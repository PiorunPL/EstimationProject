export interface IAuthorizationAppLogic {
    login(email : string, password : string) : void;
    register(email : string, username : string, password : string) : void;
}