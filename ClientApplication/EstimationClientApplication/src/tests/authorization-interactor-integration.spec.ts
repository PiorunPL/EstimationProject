
import { TestBed } from '@angular/core/testing';
import { AuthorizationInteractorService } from '../services/services/application-logic/authorization-interactor.service';
import { IAuthorizationBusinessLogic } from '../services/interfaces/business-logic/authorization-business-logic.interface';
import {CookieManagementService} from "../services/services/business-logic/cookie-management.service";

describe('AuthorizationInteractorService - cookie management integration', () => {
    let service: AuthorizationInteractorService;
    let mockAuthorizationBusinessLogic: jasmine.SpyObj<IAuthorizationBusinessLogic>;

    beforeEach(() => {
        const spyAuthorizationBusinessLogic = jasmine.createSpyObj('IAuthorizationBusinessLogic', ['login', 'register']);

        TestBed.configureTestingModule({
            providers: [
                AuthorizationInteractorService,
                { provide: 'IAuthorizationBusinessLogic', useValue: spyAuthorizationBusinessLogic },
                { provide: 'ICookieManagement', useClass: CookieManagementService }
            ]
        });

        service = TestBed.inject(AuthorizationInteractorService);
        mockAuthorizationBusinessLogic = spyAuthorizationBusinessLogic;

        const cookies = document.cookie.split(";");

        for (let i = 0; i < cookies.length; i++) {
            const cookie = cookies[i];
            const eqPos = cookie.indexOf("=");
            const name = eqPos > -1 ? cookie.substring(0, eqPos) : cookie;
            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
        }
    });

    it('should successfully login and set cookie', async () => {
        mockAuthorizationBusinessLogic.login.and.returnValue(Promise.resolve('testToken'));

        await service.login('testEmail', 'testPassword');

        expect(mockAuthorizationBusinessLogic.login).toHaveBeenCalledWith('testEmail', 'testPassword');
        expect(document.cookie).toContain('jwt-token=testToken');
    });

    it('should handle login error gracefully', async () => {
        mockAuthorizationBusinessLogic.login.and.returnValue(Promise.reject('loginError'));

        await service.login('testEmail', 'testPassword');

        expect(mockAuthorizationBusinessLogic.login).toHaveBeenCalledWith('testEmail', 'testPassword');
        expect(document.cookie).not.toContain('jwt-token');
        expect(document.cookie).toEqual('');
    });

    it('should successfully register and set cookie', async () => {
        mockAuthorizationBusinessLogic.register.and.returnValue(Promise.resolve('testToken'));

        await service.register('testEmail', 'testUsername', 'testPassword');

        expect(mockAuthorizationBusinessLogic.register).toHaveBeenCalledWith('testEmail', 'testUsername', 'testPassword');
        expect(document.cookie).toContain('jwt-token=testToken');
    });

    it('should handle register error gracefully', async () => {
        mockAuthorizationBusinessLogic.register.and.returnValue(Promise.reject('registerError'));

        await service.register('testEmail', 'testUsername', 'testPassword');

        expect(mockAuthorizationBusinessLogic.register).toHaveBeenCalledWith('testEmail', 'testUsername', 'testPassword');
        expect(document.cookie).not.toContain('jwt-token');
        expect(document.cookie).toEqual('');
    });
});
