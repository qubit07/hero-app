﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using IcqApp.DTOs;
using IcqApp.Entities;
using IcqApp.Helpers;
using IcqApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IcqApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                .Where(u => u.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.Users
                .AsQueryable();

            query = query.Where(u => u.UserName != userParams.CurrentUsername);

            if (!string.IsNullOrEmpty(userParams.KnownAs))
            {
                query = query.Where(u => u.KnownAs.Contains(userParams.KnownAs));
            }

            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                _ => query.OrderByDescending(u => u.LastActive)
            };

            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .AsNoTracking(), userParams.PageNumber, userParams.PageSize);
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<PagedList<AppUser>> GetUsersAsync(UserParams userParams)
        {
            var query = _context.Users
                .Include(u => u.Photos)
                .AsNoTracking();

            return await PagedList<AppUser>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);

        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

    }
}
