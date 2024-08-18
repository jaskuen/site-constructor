import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteCreatorComponent } from './site-creator.component';

describe('SiteCreatorComponent', () => {
  let component: SiteCreatorComponent;
  let fixture: ComponentFixture<SiteCreatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SiteCreatorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SiteCreatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
