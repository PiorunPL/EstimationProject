import { Inject, Injectable } from '@angular/core';
import { IAuthorizationAppLogic } from '../../interfaces/application-logic/authorization-app-logic.interface';
import { IAuthorizationBusinessLogic } from '../../interfaces/business-logic/authorization-business-logic.interface';
import {ICookieManagement} from "../../interfaces/business-logic/cookie-management.interface";

@Injectable({
    providedIn: 'root',
})
export class AuthorizationInteractorService implements IAuthorizationAppLogic {

    constructor(
        @Inject('IAuthorizationBusinessLogic') private authorizationBusinessLogic: IAuthorizationBusinessLogic,
        @Inject('ICookieManagement') private cookieManagement: ICookieManagement
    ) { }

    async login(email: string, password: string): Promise<void> {

        try {
            let token = await this.authorizationBusinessLogic.login(email, password)
            console.log("Token from login: " + token);
            this.cookieManagement.setCookie("jwt-token", token);
        } catch (err) {
            console.error("Error: Login interactor error\n", err);
        }
    }

    async register(email: string, username: string, password: string): Promise<void> {
        try {
            let token = await this.authorizationBusinessLogic.register(email, username, password);
            console.log("Token from register: " + token);
            this.cookieManagement.setCookie("jwt-token", token);
        } catch (err) {
            console.error("Error: Register interactor error\n", err);
        }
    }

}
