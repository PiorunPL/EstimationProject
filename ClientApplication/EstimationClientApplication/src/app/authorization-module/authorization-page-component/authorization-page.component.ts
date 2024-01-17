import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'authorization-page',
  templateUrl: './authorization-page.component.html',
  styleUrl: './authorization-page.component.scss',
})

export class AuthorizationPageComponent {
  constructor(private titleService: Title) {
    this.titleService.setTitle('EstimaPro - Auhtorization');
  }
}
