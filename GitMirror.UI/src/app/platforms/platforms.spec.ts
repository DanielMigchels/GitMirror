import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Platforms } from './platforms';

describe('Platforms', () => {
  let component: Platforms;
  let fixture: ComponentFixture<Platforms>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Platforms]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Platforms);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
