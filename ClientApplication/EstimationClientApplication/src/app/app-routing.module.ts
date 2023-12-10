import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app-component/app.component';

const routes: Routes = [
    { path: 'home', component: AppComponent },
    { path: '', redirectTo: 'home', pathMatch: 'full' }, // redirect to `home` path by default
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }