import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ProgressbarService } from 'src/app/shared/services/progressbar.service';
import { AlertService } from 'ngx-alerts';
import { AuthService } from '../resources/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  roleOptions: string[] = ['Administrator', 'Manager'];
  developerType: string[] = ['Developer', 'Designer'];

  model: any = {
    username: null,
    email: null,
    password: null,
    role: 'Administrator',
    claim: 'Developer',
  };
  constructor(
    private progressService: ProgressbarService,
    private alertService: AlertService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {}

  onSubmit() {
    this.alertService.info('Creating new user');
    this.progressService.startLoading();

    setTimeout(() => {
      this.progressService.setSuccess();
      this.progressService.completeLoading();
      this.alertService.success('Welcome New User');
    }, 3000);
  }

  roleChange(value) {
    this.model.role = value;
  }

  claimChange(value) {
    this.model.claim = value;
  }
}
