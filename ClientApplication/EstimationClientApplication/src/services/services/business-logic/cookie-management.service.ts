import {ICookieManagement} from "../../interfaces/business-logic/cookie-management.interface";
import {Injectable} from "@angular/core";

@Injectable({
    providedIn: 'root',
})
export class CookieManagementService implements ICookieManagement {

    constructor() { }

    setCookie(name: string, value: string, expires?: number, path?: string): void {
        let cookieString: string = name + "=" + value + ";";
        if (expires) {
            let date: Date = new Date();
            date.setTime(date.getTime() + expires * 60 * 60 * 1000); //expires in hours
            cookieString += "expires=" + date.toUTCString() + ";";
        }
        if (path) {
            cookieString += "path=" + path + ";";
        }
        document.cookie = cookieString;
    }

    getCookie(name: string): string {
        let cookieName: string = name + "=";
        let cookies: string[] = document.cookie.split(';');

        for (let i = 0; i < cookies.length; i++) {
            let cookie: string = cookies[i].trim();
            if (cookie.indexOf(cookieName) == 0) {
                return cookie.substring(cookieName.length, cookie.length);
            }
        }
        return '';
    }

    deleteCookie(name: string): void {
        this.setCookie(name, "", -1);
    }
}
