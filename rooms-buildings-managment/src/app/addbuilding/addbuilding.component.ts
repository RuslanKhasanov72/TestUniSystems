import { Component, OnInit, inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { AsyncPipe } from '@angular/common';


@Component({
  selector: 'app-addbuilding',
  standalone: true,
  imports: [HttpClientModule,AsyncPipe,ReactiveFormsModule],
  templateUrl: './addbuilding.component.html',
  styleUrl: './addbuilding.component.css'
})
export class AddBuildingComponent implements OnInit {
  buildingForm: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {
    this.buildingForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      floors: [0, [Validators.required, Validators.min(1)]]
    });
  }

  ngOnInit(): void {}

  addBuilding(): void {
    if (this.buildingForm.valid) {
      const newBuilding = {
        name: this.buildingForm.value.name,
        address: this.buildingForm.value.address,
        numberOfFloors: this.buildingForm.value.floors
      };

      this.http.post('https://localhost:7217/api/Buildings/Add', newBuilding).subscribe({
        next: () => {
          console.log('Building added successfully');
          this.router.navigate(['/buildings']); 
        },
        error: (err) => {
          console.error('Error adding building:', err);
          alert(err.error);
        }
      });
    }
  }
}