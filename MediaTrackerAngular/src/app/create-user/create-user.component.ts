import { Component, Input, input } from '@angular/core';
import { User } from '../model/user';
import { UserService } from '../services/user.service';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-create-user',
  standalone: true,
  imports: [RouterLink,
     ReactiveFormsModule,
      MatFormFieldModule,
       MatInputModule,
        MatButtonModule,
      MatNativeDateModule],
  templateUrl: './create-user.component.html',
  styleUrl: './create-user.component.css'
})
export class CreateUserComponent {
registerForm = FormGroup;
email: any;
userName: any; 
createuser: number = 0;
@Input() user!: User;

constructor( private router: Router,
  private userService: UserService,
  private fb: FormBuilder
 ){}

}
