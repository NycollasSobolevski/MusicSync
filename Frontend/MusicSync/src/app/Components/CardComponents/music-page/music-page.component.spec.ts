import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicPageComponent } from './music-page.component';

describe('MusicPageComponent', () => {
  let component: MusicPageComponent;
  let fixture: ComponentFixture<MusicPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MusicPageComponent]
    });
    fixture = TestBed.createComponent(MusicPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
