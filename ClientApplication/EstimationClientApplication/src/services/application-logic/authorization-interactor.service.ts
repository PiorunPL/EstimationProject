import { Inject, Injectable } from '@angular/core';
import { IAuthorizationAppLogic } from '../../interfaces/application-logic/authorization-app-logic.interface';
import { IAuthorizationBusinessLogic } from '../../interfaces/business-logic/authorization-business-logic.interface';

@Injectable({
    providedIn: 'root',
})
export class AuthorizationInteractorService implements IAuthorizationAppLogic {

    constructor(
        @Inject('IAuthorizationBusinessLogic')
        private authorizationBusinessLogic: IAuthorizationBusinessLogic,
    ) { }

    token: string = '';

    login(email: string, password: string): void {
        try {
            this.token = this.authorizationBusinessLogic.login(email, password);
        } catch (error) {
            console.log(error);
        }

        console.log("Token: " + this.token);
    }

    register(email: string, username: string, password: string): void {
        try {
            this.token = this.authorizationBusinessLogic.register(email, username, password);
        } catch (error) {
            console.log(error);
        }

        console.log("Token: " + this.token);
    }

}
