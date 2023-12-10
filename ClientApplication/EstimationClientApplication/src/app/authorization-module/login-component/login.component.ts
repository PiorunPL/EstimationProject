import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IAuthorizationAppLogic } from '../../../interfaces/application-logic/authorization-app-logic.interface';

@Component({
    selector: 'login-component',
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss'
})

export class LoginComponent implements OnInit {
    email: any;
    password: any;

    loginForm!: FormGroup<any>;

    constructor(
        @Inject('IAuthorizationAppLogic') private authorizationAppLogic: IAuthorizationAppLogic,
        private formBuilder: FormBuilder
    ) { }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.pattern(/^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/)]],
            password: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(50)]],
        });
    }

    onSubmit() {
        this.authorizationAppLogic.login(this.loginForm.value.email, this.loginForm.value.password);
    }
}

