import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IAuthorizationAppLogic } from '../../../services/interfaces/application-logic/authorization-app-logic.interface';

@Component({
    selector: 'register-component',
    templateUrl: './register.component.html',
    styleUrl: './register.component.scss'
})

export class RegisterComponent implements OnInit {

    registerForm!: FormGroup;

    constructor(
        @Inject('IAuthorizationAppLogic') private authorizationAppLogic: IAuthorizationAppLogic,
        private formBuilder: FormBuilder
    ) { }

    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.pattern(/^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/)]],
            username: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(100)]],
            password: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(50)]],
        });
    }

    onSubmit() {
        this.authorizationAppLogic.register(this.registerForm.value.email, this.registerForm.value.username, this.registerForm.value.password);
    }
}

