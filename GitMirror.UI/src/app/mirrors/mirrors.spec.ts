import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Mirrors } from './mirrors';

describe('Mirrors', () => {
  let component: Mirrors;
  let fixture: ComponentFixture<Mirrors>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Mirrors]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Mirrors);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
