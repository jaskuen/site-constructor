import {AfterViewInit, Component, Input} from '@angular/core';
import {SiteConstructorData} from "../../../types";

@Component({
  selector: 'app-site-preview',
  standalone: true,
  imports: [],
  templateUrl: './site-preview.component.html',
  styleUrl: './site-preview.component.scss'
})
export class SitePreviewComponent implements AfterViewInit {
  @Input() data!: SiteConstructorData
  ngAfterViewInit() {
    if (this.data.logoSrc) {
      (document.getElementById('site-logo') as HTMLImageElement).src = this.data.logoSrc[0].imageFileBase64String
    }
  }
}
