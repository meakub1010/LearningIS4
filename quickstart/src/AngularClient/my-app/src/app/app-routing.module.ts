import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FreeDataComponent } from './free-data/free-data.component';
import { PremiumDataComponent } from './premium-data/premium-data.component';


const routes: Routes = [
  { path: 'free', component: FreeDataComponent },
  { path: 'premium', component: PremiumDataComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
