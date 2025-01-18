import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../services/user.service';
import { MediaItems } from '../model/media-items';
@Component({
  selector: 'app-media-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './media-list.component.html',
  styleUrl: './media-list.component.css'
})
export class MediaListComponent {
constructor(private router: Router, 
  private userService: UserService
){

}

allMedia: MediaItems[] = []
  ngOninit(): void {
    const headerValue = localStorage.getItem('headerValue');
    if (!headerValue) {
      this.router.navigate(['login']);
      return;
    }
  }


}
