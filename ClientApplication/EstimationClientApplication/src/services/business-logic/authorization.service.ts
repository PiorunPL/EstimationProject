import { Injectable } from "@angular/core";
import { IAuthorizationBusinessLogic } from "../../interfaces/business-logic/authorization-business-logic.interface";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
    providedIn: 'root',
})
export class AuthorizationService implements IAuthorizationBusinessLogic{

    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
        })
    };

    constructor(private http: HttpClient) { }

    login(email : string, password : string) : string{
        var token: string = "";
        var body = {
            email: email,
            password: password
        };

        this.http.post<string>('https://localhost:7114/api/authorize/login', body, this.httpOptions).subscribe((data) => {
            console.log(data);
            token = data;
        });
        return token;
    }
    
    register(email : string, username : string, password : string) : string {
        return 'test_token';
    }
}