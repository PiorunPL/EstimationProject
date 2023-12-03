import { Injectable } from "@angular/core";
import { IAuthorizationBusinessLogic } from "../../interfaces/business-logic/authorization-business-logic.interface";

@Injectable({
    providedIn: 'root',
})
export class AuthorizationService implements IAuthorizationBusinessLogic{

    constructor() { }

    login(email : string, password : string) : string{
        return 'test_token';
    }
    
    register(email : string, username : string, password : string) : string {
        return 'test_token';
    }
}