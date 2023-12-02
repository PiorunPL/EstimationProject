import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PatternValidator } from '@angular/forms';

@Component({
    selector: 'register-component',
    templateUrl: './register.component.html',
    styleUrl: './register.component.scss'
})

export class RegisterComponent {
    username: any;
    password: any;
    email: any;

    constructor() { 
        this.username = '';
        this.password = '';
        this.email = '';
    }

    onSubmit(form: NgForm) {
        console.log(form);
        form.valid ? console.log('valid') : console.log('invalid');
    }
}

