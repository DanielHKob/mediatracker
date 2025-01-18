import { Component } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterOutlet } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, LoginComponent, NavBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'MediaTrackerAngular';

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        window.scrollTo({ top: 0, behavior: 'smooth' });
      }
    });
  }
}
