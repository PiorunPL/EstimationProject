import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'login-component',
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss'
})

export class LoginComponent {
    username: any;
    password: any;
    constructor() { }

    onSubmit(form: NgForm) {
        console.log(form);
    }
}

