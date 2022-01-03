import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommandsComponent } from './components/commands/commands.component';
import { PlatformsComponent } from './components/platforms/platforms.component';

const routes: Routes = [
  { path: '', redirectTo: '/platforms', pathMatch: 'full' },
  { path: 'platforms', component: PlatformsComponent },
  { path: 'commands', component: CommandsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
