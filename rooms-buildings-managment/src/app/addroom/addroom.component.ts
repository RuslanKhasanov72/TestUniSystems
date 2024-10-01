import { Component, OnInit, inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { AsyncPipe } from '@angular/common';
@Component({
  selector: 'app-addroom',
  standalone: true,
  imports: [HttpClientModule,AsyncPipe,ReactiveFormsModule],
  templateUrl: './addroom.component.html',
  styleUrl: './addroom.component.css'
})
export class AddRoomComponent implements OnInit {
  roomForm: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {
    this.roomForm = this.fb.group({
      name: ['', Validators.required],
      buildingId: [0, [Validators.required, Validators.min(1)]],
      roomTypeId: ['', Validators.required],
      capacity: [0, [Validators.required, Validators.min(1)]],
      floor: [0, [Validators.required, Validators.min(0)]],
      number: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  addRoom(): void {
    if (this.roomForm.valid) {
      const newRoom = {
        name: this.roomForm.value.name,
        buildingId: this.roomForm.value.buildingId,
        roomTypeId: this.roomForm.value.roomTypeId,
        capacity: this.roomForm.value.capacity,
        floor: this.roomForm.value.floor,
        number: this.roomForm.value.number
      };

      this.http.post('http://localhost:7211/api/Rooms/Add', newRoom).subscribe({
        next: () => {
          console.log('Room added successfully');
          this.router.navigate(['/rooms']); 
        },
        error: (err) => {
          console.error('Error adding room:', err);
          alert(err.error);
        }
      });
    }
  }
}