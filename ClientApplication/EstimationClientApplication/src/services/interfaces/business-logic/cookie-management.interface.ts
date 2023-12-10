export interface ICookieManagement {
    setCookie(name: string, value: string, expires?: number, path?: string): void;
    getCookie(name: string): string;
    deleteCookie(name: string): void;
}
