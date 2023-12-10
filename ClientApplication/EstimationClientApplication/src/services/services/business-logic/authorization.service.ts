import {Injectable} from "@angular/core";
import {IAuthorizationBusinessLogic} from "../../interfaces/business-logic/authorization-business-logic.interface";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
    providedIn: 'root',
})
export class AuthorizationService implements IAuthorizationBusinessLogic {

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        }),
        responseType: 'text' as 'json',
    };

    constructor(private http: HttpClient) { }

    async login(email: string, password: string): Promise<string> {
        var body = {
            email: email,
            password: password
        };

        return new Promise((resolve, reject) => {
            this.http.post<string>('https://localhost:7114/api/authorize/login', body, this.httpOptions).subscribe({
                next: data => {
                    resolve(data);
                },
                error: error => {
                    error.error = error.error ? JSON.parse(error.error) : error;
                    reject(new Error(JSON.stringify(error, null, 2)));
                }
            });
        });
    }

    async register(email: string, username: string, password: string): Promise<string> {
        let body = {
            email: email,
            username: username,
            password: password
        };

        return new Promise((resolve, reject) => {
            this.http.post<string>('https://localhost:7114/api/authorize/register', body, this.httpOptions).subscribe({
                next: data => {
                    resolve(data);
                },
                error: error => {
                    error.error = error.error ? JSON.parse(error.error) : error;
                    reject(new Error(JSON.stringify(error, null, 2)));
                }
            });
        });
    }
}
