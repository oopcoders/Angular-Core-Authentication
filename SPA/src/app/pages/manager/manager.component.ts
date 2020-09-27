import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IResponse } from 'src/app/modules/auth/resources/IResponse';
import { SecretService } from 'src/app/shared/services/secret.service';

@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrls: ['./manager.component.scss'],
})
export class ManagerComponent implements OnInit {
  secrets$: Observable<IResponse>;
  constructor(private secretService: SecretService) {}

  ngOnInit(): void {
    this.getManagerDeveloperSecrets();
  }

  getManagerDeveloperSecrets() {
    this.secrets$ = this.secretService.managerDeveloperSecrets();
  }
}
