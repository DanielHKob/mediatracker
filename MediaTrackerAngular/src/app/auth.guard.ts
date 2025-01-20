import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

@Injectable({
    providedIn: 'root'
})
export class AuthGuard {
    constructor(private router: Router) {}

    canActivate(): boolean {
      const isAuthenticated = localStorage.getItem('headerValue') !== null;
  
      if (!isAuthenticated) {
        // Redirect only if the route is protected
        this.router.navigate(['/login']);
        return false;
      }
      return true;
    }
}
