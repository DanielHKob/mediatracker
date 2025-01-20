import { Component, Input, input } from '@angular/core';
import { User } from '../model/user';
import { UserService } from '../services/user.service';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { catchError, throwError } from 'rxjs';


@Component({
  selector: 'app-create-user',
  standalone: true,
  imports: [RouterLink,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatNativeDateModule,
    MatDatepickerModule],
  templateUrl: './create-user.component.html',
  styleUrl: './create-user.component.css'
})
export class CreateUserComponent {
registerForm: FormGroup;
email: any;
userName: any; 
createuser: number = 0;
@Input() user!: User;
  minDate: Date = new Date(new Date().getFullYear() - 50, 0, 1); // Default to 50 years ago
  maxDate: Date = new Date(); // Default to today
constructor( private router: Router,
  private userService: UserService,
  private fb: FormBuilder
 )
 {
  this.registerForm = this.fb.group({
    email: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    dateofbirth: ['', Validators.required],
    password: ['', Validators.required]

  });

 }

 ngOninit(){
  console.log("Create user component has been called!");
 }
  onSubmit(): void {
    console.log("onsubmit has been called!")
    if (this.registerForm.valid){
      this.getUserEmail();
      this.getUserByEmail(this.email);
    }
    else{
      console.log(this.registerForm);
    }
  }

  getUserEmail(){
    console.log("getuserEmail has been called!");
    this.email = this.registerForm.value.email;
  }

  // getUserByEmail(email: string){
  //   this.userService.getUserByEmail(email).subscribe((data: User) => {
  //     this.user = data;
  //     if ( this.email === this.user.Email) {
  //       alert("Email already exists, please provide antoher one");
  //     }
  //   }, (error) => {
  //     if (error.status === 404 ){

  //     }
  //   }
  // )
  // }

  getUserByEmail(email: string) {
    this.userService.getUserByEmail(email).subscribe(
      (data: User) => {
        this.user = data;
        if (this.email === this.user.Email) {
          alert("Email already exists, please provide another one");
        }
      },
      (error) => {
        console.error('Error in getUserByEmail:', error); // Log full error
        if (error.status === 404) {
          console.log(error.status);
          console.log('Email not found, proceeding to create user');
          this.createuser = 1;
          this.createUser();
        } else {
          console.log(error.status);
          alert('An unexpected error occurred. Please try again.');
        }
      }
    );
  }

  createUser(){
    if (this.createuser === 1){
      this.userService.getLatestUserId().subscribe((latestId: number) => {
        const newUserId = latestId + 1;
        const formValue = this.registerForm.value;

        const payload: User = {
          id: newUserId,
          Email: formValue.email,
          Password:formValue.password,
          FirstName: formValue.firstName,
          LastName: formValue.lastName,
          dateofbirth: formValue.dateofbirth
        }
        console.log(payload);
        this.userService.createUser(payload).subscribe((response) => {
          console.log("User create sucessfully:", response);
          alert('Account created sucessfully!');
          this.router.navigate(['/login']);
        },
        (error) => {
          console.error('Error creating the user:', error);
          alert('failed to create account. Please try again');
          }
        );
        }
      )
    }
    }

}
