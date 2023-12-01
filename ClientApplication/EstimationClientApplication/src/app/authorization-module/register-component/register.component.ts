import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'register-component',
    templateUrl: './register.component.html',
    styleUrl: './register.component.scss'
})

export class RegisterComponent {
    username: any;
    password: any;
    email: any;

    constructor() { }

    onSubmit(form: NgForm) {
        console.log(form);
    }
}

