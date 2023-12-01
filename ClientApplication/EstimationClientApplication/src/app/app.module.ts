import { BrowserModule } from "@angular/platform-browser";
import { AuthorizationModule } from "./authorization-module/authorization.module";
import { NgModule } from "@angular/core";
import { AppComponent } from "./app-component/app.component";

@NgModule({
    declarations: [AppComponent],
    imports: [BrowserModule, AuthorizationModule],
    providers: [],
    bootstrap: [AppComponent],
})

export class AppModule { }
