import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommandsPlatformsComponent } from './commands-platforms.component';

describe('CommandsPlatformsComponent', () => {
  let component: CommandsPlatformsComponent;
  let fixture: ComponentFixture<CommandsPlatformsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CommandsPlatformsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CommandsPlatformsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
