import { Component, OnInit, inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { AsyncPipe, CommonModule } from '@angular/common';
@Component({
  selector: 'app-addroom',
  standalone: true,
  imports: [HttpClientModule,AsyncPipe,ReactiveFormsModule,CommonModule],
  templateUrl: './addroom.component.html',
  styleUrl: './addroom.component.css'
})
export class AddRoomComponent implements OnInit {
  roomForm: FormGroup;
  buildings: any[] = [];  // Массив для хранения списка зданий
  roomTypes: any[] = [];  // Массив для хранения списка типов комнат

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {
    this.roomForm = this.fb.group({
      name: ['', Validators.required],
      buildingId: [null, Validators.required],  // Используем null, чтобы упростить валидацию
      roomTypeId: [null, Validators.required],
      capacity: [0, [Validators.required, Validators.min(1)]],
      floor: [0, [Validators.required, Validators.min(0)]],
      number: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadBuildings();   // Загрузка данных о зданиях
    this.loadRoomTypes();   // Загрузка данных о типах комнат
  }

  // Загрузка списка зданий с сервера
  loadBuildings(): void {
    this.http.get<any[]>('http://localhost:7211/api/Buildings/GetAll').subscribe({
      next: (data) => this.buildings = data,
      error: (err) => console.error('Error loading buildings:', err)
    });
  }

  // Загрузка списка типов комнат с сервера
  loadRoomTypes(): void {
    this.http.get<any[]>('http://localhost:7211/api/RoomTypes/GetAll').subscribe({
      next: (data) => this.roomTypes = data,
      error: (err) => console.error('Error loading room types:', err)
    });
  }

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