import { User } from "./user";

export class UserParams {

    pageNumber = 1;
    pageSize = 5;
    knownAs: string = '';
    orderBy: string = 'lastActive';

    constructor(user: User) {

    }
}