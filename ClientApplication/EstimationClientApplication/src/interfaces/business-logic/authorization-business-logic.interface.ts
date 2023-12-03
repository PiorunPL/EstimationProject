export interface IAuthorizationBusinessLogic {
    login(email: string, password: string): string;
    register(email: string, username: string, password: string): string;
}