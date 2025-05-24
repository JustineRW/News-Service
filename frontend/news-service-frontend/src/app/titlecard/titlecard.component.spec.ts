import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TitlecardComponent } from './titlecard.component';

describe('TitlecardComponent', () => {
  let component: TitlecardComponent;
  let fixture: ComponentFixture<TitlecardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TitlecardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TitlecardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
