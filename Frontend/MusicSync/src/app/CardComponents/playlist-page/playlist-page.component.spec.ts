import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaylistPageComponent } from './playlist-page.component';

describe('PlaylistPageComponent', () => {
  let component: PlaylistPageComponent;
  let fixture: ComponentFixture<PlaylistPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PlaylistPageComponent]
    });
    fixture = TestBed.createComponent(PlaylistPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
