import { Component, Input, OnInit } from '@angular/core';
import { MediaItems } from '../model/media-items';
import { MediaitemsService } from '../services/mediaitems.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Title } from '@angular/platform-browser';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-media-items',
  standalone: true,
  imports: [MediaItemsComponent,
    FormsModule,
    DatePipe,
    RouterLink
  ],
  templateUrl: './media-items.component.html',
  styleUrl: './media-items.component.css'
})
export class MediaItemsComponent  implements OnInit {
@Input() mediaitems!: MediaItems;
mediaform: FormGroup;
  constructor(private mediaService: MediaitemsService,
    private fb: FormBuilder,
    private datePipe: DatePipe,
    private snackbar: MatSnackBar
  ) {
    this.mediaform = this.fb.group({
      id: [null, Validators.required],
      Title: [null],
      Type: [null],
      ReleaseYear: [null],
      Rating: [null],
      Comments: [null],
      StreamingService: [null],
      CreateDate: [null],
      UserId: [null]

    })
  }

  ngOnInit(): void{
    this.fetchMedia();
  }


  fetchMedia(): void {
    this.mediaService.getMedia(this.mediaitems.Id).subscribe((data: MediaItems) => {
      this.mediaitems = data;
      const formattedDate = this.datePipe.transform(this.mediaitems.CreateDate, 'yyyy-MM-dd');
      this.mediaform.patchValue({
        id: this.mediaitems.Id,
        Title: this.mediaitems.Title,
        Type: this.mediaitems.Type,
        ReleaseYear: this.mediaitems.ReleaseYear,
        Rating: this.mediaitems.Rating,
        Comments: this.mediaitems.Comments,
        StreamingService: this.mediaitems.StreamingService,
        CreateDate: this.mediaitems.CreateDate,
        UserId: this.mediaitems.UserId
      })

    })

    }  
}

