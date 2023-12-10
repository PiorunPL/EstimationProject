import { BrowserModule } from "@angular/platform-browser";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { NgModule } from "@angular/core";
import { LoginComponent } from "./login-component/login.component";
import { RegisterComponent } from "./register-component/register.component";

@NgModule({
    declarations: [LoginComponent, RegisterComponent],
    imports: [BrowserModule, ReactiveFormsModule, FormsModule],
    exports: [LoginComponent, RegisterComponent],
    providers: [
        //TODO: Think about providing authorization services here instead of app.module.ts
    ],
    bootstrap: [LoginComponent, RegisterComponent],
})

export class AuthorizationModule { }
