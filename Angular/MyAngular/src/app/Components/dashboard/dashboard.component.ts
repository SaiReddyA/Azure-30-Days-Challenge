import { Component } from '@angular/core';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

username = '';

  constructor(private msalService: MsalService) {
    const account = this.msalService.instance.getActiveAccount();
    if (account) {
      this.username = account.name || account.username || 'User';
    }
  }
  logout() {
    this.msalService.logoutRedirect();
  }
}
