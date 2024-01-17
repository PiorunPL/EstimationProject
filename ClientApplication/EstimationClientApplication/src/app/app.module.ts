import { BrowserModule } from "@angular/platform-browser";
import { AuthorizationModule } from "./authorization-module/authorization.module";
import { NgModule } from "@angular/core";
import { AppComponent } from "./app-component/app.component";
import { AuthorizationInteractorService } from "../services/services/application-logic/authorization-interactor.service";
import { AuthorizationService } from "../services/services/business-logic/authorization.service";
import { HttpClientModule } from "@angular/common/http";
import {CookieManagementService} from "../services/services/business-logic/cookie-management.service";
import { SimulationComponent } from "./simulation-module/simulation-component/simulation.component";
import { SimulationModule } from "./simulation-module/simulation.module";
import { RouterModule } from "@angular/router";
import { AppRoutingModule } from "./app-routing.module";

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserModule, 
        AppRoutingModule,
        HttpClientModule, 
        AuthorizationModule, 
        SimulationModule, 
        RouterModule
    ],
    providers: [
        { provide: 'IAuthorizationAppLogic', useClass: AuthorizationInteractorService },
        { provide: 'IAuthorizationBusinessLogic', useClass: AuthorizationService },
        { provide: 'ICookieManagement', useClass: CookieManagementService }
     ],
    bootstrap: [AppComponent],
})

export class AppModule { }
