export interface IAuthorizationBusinessLogic {
    login(email: string, password: string): Promise<string>;
    register(email: string, username: string, password: string): Promise<string>;
}