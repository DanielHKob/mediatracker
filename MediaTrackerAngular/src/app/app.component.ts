import { Component } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterOutlet } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { MediaItemsComponent } from "./media-items/media-items.component";
import { CreateUserComponent } from './create-user/create-user.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, 
    RouterLink,
    LoginComponent,
    NavBarComponent,
    MediaItemsComponent,
    CreateUserComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'MediaTrackerAngular';

  constructor(private router: Router) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        console.log('Navigated to:', event.url); // Log current URL
      }
    });
  }

  ngOnInit(): void {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        window.scrollTo({ top: 0, behavior: 'smooth' });
      }
    });
  }
}
