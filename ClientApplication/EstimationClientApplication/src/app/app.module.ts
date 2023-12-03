import { BrowserModule } from "@angular/platform-browser";
import { AuthorizationModule } from "./authorization-module/authorization.module";
import { NgModule } from "@angular/core";
import { AppComponent } from "./app-component/app.component";
import { AuthorizationInteractorService } from "../services/application-logic/authorization-interactor.service";
import { AuthorizationService } from "../services/business-logic/authorization.service";

@NgModule({
    declarations: [AppComponent],
    imports: [BrowserModule, AuthorizationModule],
    providers: [
        { provide: 'IAuthorizationAppLogic', useClass: AuthorizationInteractorService },
        { provide: 'IAuthorizationBusinessLogic', useClass: AuthorizationService }
     ],
    bootstrap: [AppComponent],
})

export class AppModule { }
