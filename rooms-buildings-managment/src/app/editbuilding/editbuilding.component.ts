import { AsyncPipe } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Buildings } from '../../models/building.model';
import { Observable, switchMap, tap } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-editbuilding',
  standalone: true,
  imports: [HttpClientModule,AsyncPipe,ReactiveFormsModule,CommonModule],
  templateUrl: './editbuilding.component.html',
  styleUrl: './editbuilding.component.css'
})
export class EditbuildingComponent implements OnInit {
itemId:number | undefined;
$building: Observable<Buildings> | undefined;
buildingForm: FormGroup;

http=inject(HttpClient);
constructor(private route: ActivatedRoute, private fb: FormBuilder) {
   // Инициализация формы
   this.buildingForm = this.fb.group({
    name: ['', Validators.required],
    address: ['', Validators.required],
    floors: [0, [Validators.required, Validators.min(1)]]
  });
 }

ngOnInit(): void {
 // Получаем itemId из параметров маршрута
 this.itemId = Number(this.route.snapshot.paramMap.get('id'));

 // Проверяем, что itemId валиден и вызываем метод findById только после этого
 if (this.itemId) {
  // Подписка на Observable для получения данных
  this.$building = this.findById(this.itemId);
  this.$building.subscribe({
    next: (building) => {
      console.log('Fetched Building:', building);
      this.buildingForm.patchValue({
        name: building.name,
        address: building.address,
        floors: building.numberOfFloors
      });
    },
    error: (err) => console.error('Error fetching building:', err)
  });
}
}

public findById(itemId:number): Observable<Buildings>
{
return this.http.get<Buildings>('https://localhost:7217/api/Buildings/GetBy/'+itemId);
}

public editBuilding(): void {
  if (this.buildingForm.valid) {
    const addbuildingRequest = {
      name: this.buildingForm.value.name,
      address: this.buildingForm.value.address,
      numberOfFloors: this.buildingForm.value.floors
    };

    this.http.put(`https://localhost:7217/api/Buildings/Update/${this.itemId}`, addbuildingRequest).subscribe({
      next: (response) => {
        console.log('Building updated successfully:', response);
  
      },
      error: (error) => {
        console.error('Error updating building:', error);
        alert(error.error);

      }
    });
  } else {
    console.error('Form is invalid');
  }
}
}
