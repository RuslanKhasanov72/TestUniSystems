import { HttpClient, HttpClientJsonpModule, HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Rooms } from '../../models/room.model';
import { AsyncPipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ShowRooms } from '../../models/showroom.model';

@Component({
  selector: 'app-rooms',
  standalone: true,
  imports: [HttpClientModule,AsyncPipe,RouterLink],
  templateUrl: './rooms.component.html',
  styleUrl: './rooms.component.css'
})
export class RoomsComponent {
  http=inject(HttpClient);

  rooms$=this.getRooms();

  private getRooms(): Observable<ShowRooms[]>{
    return this.http.get<ShowRooms[]>('http://localhost:7211/api/Rooms/GetAll');
  }

  public deleteRoom(id: number ){
    this.http.delete('http://localhost:7211/api/Rooms/Delete'+id).subscribe(
      {
        next:(value) => {
          alert('Item deleted');
          this.rooms$=this.getRooms();
        },
      }
    )
  }
}
