import { provideRouter,RouterModule,Routes } from '@angular/router';
import {BuildingsComponent} from './buildings/buildings.component';
import {RoomsComponent} from './rooms/rooms.component';
import { ApplicationConfig, NgModule } from '@angular/core';
import { EditbuildingComponent } from './editbuilding/editbuilding.component';
import { EditRoomComponent } from './editroom/editroom.component';
import { AddBuildingComponent } from './addbuilding/addbuilding.component';
import { AddRoomComponent } from './addroom/addroom.component';

export const routes: Routes = [
    { path: 'buildings', component: BuildingsComponent },
  { path: 'rooms', component: RoomsComponent },
  { path: 'buildings/edit/:id', component: EditbuildingComponent },
  { path: 'rooms/edit/:id', component: EditRoomComponent },
  { path: 'buildings/add', component: AddBuildingComponent },
  { path: 'rooms/add', component: AddRoomComponent },
];

@NgModule({
    imports: [ RouterModule.forRoot(routes) ],
    exports: [ RouterModule ]
  })
  export class AppRoutingModule {}