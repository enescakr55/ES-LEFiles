import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientRegistrationTokensComponent } from './client-registration-tokens.component';

describe('ClientRegistrationTokensComponent', () => {
  let component: ClientRegistrationTokensComponent;
  let fixture: ComponentFixture<ClientRegistrationTokensComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientRegistrationTokensComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientRegistrationTokensComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
