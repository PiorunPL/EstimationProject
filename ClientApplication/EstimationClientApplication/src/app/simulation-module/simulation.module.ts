import { BrowserModule } from "@angular/platform-browser";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { NgModule } from "@angular/core";
import { SimulationComponent } from "./simulation-component/simulation.component";


@NgModule({
    declarations: [SimulationComponent],
    imports: [BrowserModule, ReactiveFormsModule, FormsModule],
    exports: [SimulationComponent],
    providers: [
        //TODO: Think about providing authorization services here instead of app.module.ts (see src/app/app.module.ts)
    ],
    bootstrap: [SimulationComponent],
})

export class SimulationModule { }
