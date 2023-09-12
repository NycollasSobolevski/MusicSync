import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StreamersConfigComponent } from './streamers-config.component';

describe('StreamersConfigComponent', () => {
  let component: StreamersConfigComponent;
  let fixture: ComponentFixture<StreamersConfigComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StreamersConfigComponent]
    });
    fixture = TestBed.createComponent(StreamersConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
