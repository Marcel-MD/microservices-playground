import { Component, OnInit } from '@angular/core';
import { Platform } from 'src/app/models/platform';
import { CommandService } from 'src/app/services/command.service';

@Component({
  selector: 'app-commands-platforms',
  templateUrl: './commands-platforms.component.html',
  styleUrls: ['./commands-platforms.component.css'],
})
export class CommandsPlatformsComponent implements OnInit {
  platforms: Platform[] = [];

  constructor(private commandService: CommandService) {}

  ngOnInit(): void {
    this.getPlatforms();
  }

  getPlatforms(): void {
    this.commandService
      .getPlatforms()
      .subscribe((platforms) => (this.platforms = platforms));
  }
}
