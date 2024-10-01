import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BuildingsComponent } from './buildings/buildings.component'; // Adjust the import path
//import { RoomsComponent } from './path-to-rooms/rooms.component'; // Adjust the import path

const routes: Routes = [
  { path: 'app-buildings', component: BuildingsComponent },
 // { path: 'app-rooms', component: RoomsComponent }, // Ensure RoomsComponent is imported
  { path: '', redirectTo: '/app-buildings', pathMatch: 'full' }, // Default route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}