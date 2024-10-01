import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Buildings } from '../../models/building.model';
import { Observable } from 'rxjs';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-buildings',
  standalone: true,
  imports: [HttpClientModule,AsyncPipe,RouterLink],
  templateUrl: './buildings.component.html',
  styleUrl: './buildings.component.css'
})
export class BuildingsComponent {
  http=inject(HttpClient);

  buildings$=this.getBuildings();

  private getBuildings(): Observable<Buildings[]>{
    return this.http.get<Buildings[]>('https://localhost:7217/api/Buildings/GetAll');
  }


  public deleteBuilding(id: number ){
    this.http.delete('https://localhost:7217/api/Buildings/Delete'+id).subscribe(
      {
        next:(value) => {
          alert('Item deleted');
          this.buildings$=this.getBuildings();
        },
      }
    )
  }
}
