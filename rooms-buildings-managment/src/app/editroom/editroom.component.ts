import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { Rooms } from '../../models/room.model';
import { AsyncPipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-editroom',
  standalone: true,
  imports: [HttpClientModule,AsyncPipe,ReactiveFormsModule,CommonModule],
  templateUrl: './editroom.component.html',
  styleUrl: './editroom.component.css'
})
export class EditRoomComponent implements OnInit {
  roomId: number | undefined;
  room$: Observable<Rooms> | undefined;
  roomForm: FormGroup;

  constructor(private route: ActivatedRoute, private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.roomForm = this.fb.group({
      name: ['', Validators.required],
      buildingId: ['', Validators.required],
      roomTypeId: ['', Validators.required],
      capacity: [0, [Validators.required, Validators.min(1)]],
      floor: [0, [Validators.required, Validators.min(0)]],
      number: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    // Получаем roomId из параметров маршрута
    this.roomId = Number(this.route.snapshot.paramMap.get('id'));

    if (this.roomId) {
      this.room$ = this.findRoomById(this.roomId);
      this.room$.subscribe(room => {
        this.roomForm.patchValue(room);
      });
    }
  }

  private findRoomById(roomId: number): Observable<Rooms> {
    return this.http.get<Rooms>(`http://localhost:7211/api/Rooms/GetBy/${roomId}`);
  }

  public editRoom(): void {
    if (this.roomForm.valid) {
      const updatedRoom = {
        name: this.roomForm.value.name,
        buildingId: this.roomForm.value.buildingId,
        roomTypeId: this.roomForm.value.roomTypeId,
        capacity: this.roomForm.value.capacity,
        floor: this.roomForm.value.floor,
        number: this.roomForm.value.number,
      };
      this.http.put(`http://localhost:7211/api/Rooms/Update/${this.roomId}`, updatedRoom).subscribe({
        next: () => {
          console.log('Room updated successfully');
          this.router.navigate(['/rooms']); 
        },
        error: (err) => {
          console.error('Error updating room:', err);
          alert(err.error);
        }
      });
    } else {
      console.error('Form is invalid');
    }
  }
}
