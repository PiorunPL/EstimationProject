import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app-component/app.component';
import { SimulationComponent } from './simulation-module/simulation-component/simulation.component';
import { AuthorizationPageComponent } from './authorization-module/authorization-page-component/authorization-page.component';

const routes: Routes = [
    { path: 'home', component: AuthorizationPageComponent },
    { path: 'simulation', component: SimulationComponent},
    { path: '', redirectTo: 'home', pathMatch: 'full' }, // redirect to `home` path by default
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }