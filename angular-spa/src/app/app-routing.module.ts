import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommandsPlatformsComponent } from './components/commands-platforms/commands-platforms.component';
import { CommandsComponent } from './components/commands/commands.component';
import { PlatformsComponent } from './components/platforms/platforms.component';

const routes: Routes = [
  { path: '', redirectTo: '/platforms', pathMatch: 'full' },
  { path: 'platforms', component: PlatformsComponent },
  { path: 'commands', component: CommandsPlatformsComponent },
  { path: 'commands/:id', component: CommandsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
