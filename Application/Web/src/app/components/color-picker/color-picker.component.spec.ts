import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ColorPickerComponent } from './color-picker.component';

describe('ColorPickerComponent', () => {
  let component: ColorPickerComponent;
  let fixture: ComponentFixture<ColorPickerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ColorPickerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ColorPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should change its text correctly', () => {
    component.text = 'text';
    fixture.detectChanges();
    expect(component.text).toBe('text');
  });
  it('should change its color correctly', () => {
    component.color = "#000000";
    fixture.detectChanges();
    expect(component.color).toBe("#000000");
  });
});
