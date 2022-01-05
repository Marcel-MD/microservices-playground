import { Platform } from 'src/app/models/platform';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Command } from 'src/app/models/command';
import { CommandService } from 'src/app/services/command.service';

@Component({
  selector: 'app-commands',
  templateUrl: './commands.component.html',
  styleUrls: ['./commands.component.css'],
})
export class CommandsComponent implements OnInit {
  commands: Command[] = [];

  constructor(
    private route: ActivatedRoute,
    private commandService: CommandService
  ) {}

  ngOnInit(): void {
    this.platformId = Number(this.route.snapshot.paramMap.get('id'));

    this.commandService.getPlatform(this.platformId).subscribe((platform) => {
      this.platform = platform;
    });
    this.commandService
      .getCommands(this.platformId)
      .subscribe((commands) => (this.commands = commands));
  }

  platform?: Platform;
  platformId?: number;
  howTo: string = '';
  commandLine: string = '';

  addCommand(): void {
    if (!this.platformId) return;

    this.commandService
      .addCommand(
        {
          howTo: this.howTo,
          commandLine: this.commandLine,
        } as Command,
        this.platformId
      )
      .subscribe((command) => {
        if (!command.id) return;
        this.commands.push(command);
      });
  }

  deletePlatform(id: number): void {
    if (!this.platformId) return;

    this.commands = this.commands.filter((c) => c.id !== id);
    this.commandService.deleteCommand(this.platformId, id).subscribe();
  }
}
