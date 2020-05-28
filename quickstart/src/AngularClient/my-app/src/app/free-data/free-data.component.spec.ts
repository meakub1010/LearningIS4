import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FreeDataComponent } from './free-data.component';

describe('FreeDataComponent', () => {
  let component: FreeDataComponent;
  let fixture: ComponentFixture<FreeDataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FreeDataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FreeDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
