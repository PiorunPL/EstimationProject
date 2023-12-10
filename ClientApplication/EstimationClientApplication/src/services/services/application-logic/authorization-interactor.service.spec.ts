import { TestBed } from '@angular/core/testing';
import { AuthorizationInteractorService } from './authorization-interactor.service';
import { IAuthorizationBusinessLogic } from '../../interfaces/business-logic/authorization-business-logic.interface';
import { ICookieManagement } from "../../interfaces/business-logic/cookie-management.interface";

describe('AuthorizationInteractorService', () => {
    let service: AuthorizationInteractorService;
    let mockAuthorizationBusinessLogic: jasmine.SpyObj<IAuthorizationBusinessLogic>;
    let mockCookieManagement: jasmine.SpyObj<ICookieManagement>;

    beforeEach(() => {
        const spyAuthorizationBusinessLogic = jasmine.createSpyObj('IAuthorizationBusinessLogic', ['login', 'register']);
        const spyCookieManagement = jasmine.createSpyObj('ICookieManagement', ['setCookie', 'getCookie', 'deleteCookie']);

        TestBed.configureTestingModule({
            providers: [
                AuthorizationInteractorService,
                { provide: 'IAuthorizationBusinessLogic', useValue: spyAuthorizationBusinessLogic },
                { provide: 'ICookieManagement', useValue: spyCookieManagement }
            ]
        });

        service = TestBed.inject(AuthorizationInteractorService);
        mockAuthorizationBusinessLogic = spyAuthorizationBusinessLogic;
        mockCookieManagement = spyCookieManagement;
    });

    it('should successfully login and set cookie', async () => {
        mockAuthorizationBusinessLogic.login.and.returnValue(Promise.resolve('testToken'));

        await service.login('testEmail', 'testPassword');

        expect(mockAuthorizationBusinessLogic.login).toHaveBeenCalledWith('testEmail', 'testPassword');
        expect(mockCookieManagement.setCookie).toHaveBeenCalledWith('jwt-token', 'testToken');
    });

    it('should handle login error gracefully', async () => {
        mockAuthorizationBusinessLogic.login.and.returnValue(Promise.reject('loginError'));

        await service.login('testEmail', 'testPassword');

        expect(mockAuthorizationBusinessLogic.login).toHaveBeenCalledWith('testEmail', 'testPassword');
        expect(mockCookieManagement.setCookie).not.toHaveBeenCalled();
    });

    it('should successfully register and set cookie', async () => {
        mockAuthorizationBusinessLogic.register.and.returnValue(Promise.resolve('testToken'));

        await service.register('testEmail', 'testUsername', 'testPassword');

        expect(mockAuthorizationBusinessLogic.register).toHaveBeenCalledWith('testEmail', 'testUsername', 'testPassword');
        expect(mockCookieManagement.setCookie).toHaveBeenCalledWith('jwt-token', 'testToken');
    });

    it('should handle register error gracefully', async () => {
        mockAuthorizationBusinessLogic.register.and.returnValue(Promise.reject('registerError'));

        await service.register('testEmail', 'testUsername', 'testPassword');

        expect(mockAuthorizationBusinessLogic.register).toHaveBeenCalledWith('testEmail', 'testUsername', 'testPassword');
        expect(mockCookieManagement.setCookie).not.toHaveBeenCalled();
    });
});
