import { TestBed } from '@angular/core/testing';
import { CookieManagementService } from './cookie-management.service';

describe('CookieManagementService', () => {
    let service: CookieManagementService;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(CookieManagementService);

        const cookies = document.cookie.split(";");

        for (let i = 0; i < cookies.length; i++) {
            const cookie = cookies[i];
            const eqPos = cookie.indexOf("=");
            const name = eqPos > -1 ? cookie.substring(0, eqPos) : cookie;
            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
        }
    });

    it('should set a cookie with a given name and value', () => {
        service.setCookie('test', 'value');
        expect(document.cookie).toContain('test=value');
    });

    it('should get a cookie with a given name', () => {
        service.setCookie('test', 'value');
        expect(service.getCookie('test')).toEqual('value');
    });

    it('should return an empty string when getting a non-existent cookie', () => {
        expect(service.getCookie('nonExistent')).toEqual('');
    });

    it('should delete a cookie with a given name', () => {
        service.setCookie('test', 'value');
        service.deleteCookie('test');
        expect(service.getCookie('test')).toEqual('');
    });

    it('should set a cookie with a given name, value, and expiration', () => {
        service.setCookie('test', 'value', 1);
        expect(document.cookie).toContain('test=value');
    });

    it('should set a cookie with a given name, value, expiration, and path', () => {
        service.setCookie('test', 'value', 1, '/');
        expect(document.cookie).toContain('test=value');
    });

    it('should handle cookies with leading spaces', () => {
        document.cookie = ' test=value';
        expect(service.getCookie('test')).toEqual('value');
    });
});
