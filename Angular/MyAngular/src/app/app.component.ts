import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { MsalBroadcastService, MsalModule, MsalService } from '@azure/msal-angular';
import { MsalRedirectComponent } from '@azure/msal-angular';
import { InteractionStatus } from '@azure/msal-browser';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MsalModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'MyAngular';

  constructor(private broadcast: MsalBroadcastService, private router: Router, private msalService: MsalService) {
  this.broadcast.inProgress$.subscribe((status) => {
    if (status === InteractionStatus.None) {
      const account = this.msalService.instance.getActiveAccount();
      if (account) {
        this.router.navigate(['/dashboard']);
      }
    }
  });
}
}
