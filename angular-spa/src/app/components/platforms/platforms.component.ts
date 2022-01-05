import { Component, OnInit } from '@angular/core';
import { Platform } from 'src/app/models/platform';
import { PlatformService } from 'src/app/services/platform.service';

@Component({
  selector: 'app-platforms',
  templateUrl: './platforms.component.html',
  styleUrls: ['./platforms.component.css'],
})
export class PlatformsComponent implements OnInit {
  platforms: Platform[] = [];

  constructor(private platformService: PlatformService) {}

  ngOnInit(): void {
    this.getPlatforms();
  }

  getPlatforms(): void {
    this.platformService
      .getPlatforms()
      .subscribe((platforms) => (this.platforms = platforms));
  }

  name: string = '';
  publisher: string = '';
  cost: string = '';

  addPlatform(): void {
    this.platformService
      .addPlatform({
        name: this.name,
        publisher: this.publisher,
        cost: this.cost,
      } as Platform)
      .subscribe((platform) => {
        if (!platform.id) return;
        this.platforms.push(platform);
      });
  }

  deletePlatform(id: number): void {
    this.platforms = this.platforms.filter((p) => p.id !== id);
    this.platformService.deletePlatform(id).subscribe();
  }
}
