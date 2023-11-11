import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountConfigComponent } from './account-config.component';

describe('AccountConfigComponent', () => {
  let component: AccountConfigComponent;
  let fixture: ComponentFixture<AccountConfigComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccountConfigComponent]
    });
    fixture = TestBed.createComponent(AccountConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
