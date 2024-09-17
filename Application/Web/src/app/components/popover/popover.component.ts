import {Component, EventEmitter, Input, Output} from '@angular/core';
import {TextInputComponent} from "../text-input/text-input.component";
import {ButtonComponent} from "../button/button.component";
import {NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {DownloadSiteRequest} from "../../../types";

@Component({
  selector: 'app-popover',
  standalone: true,
  imports: [
    TextInputComponent,
    ButtonComponent,
    NgIf,
    FormsModule
  ],
  templateUrl: './popover.component.html',
  styleUrl: './popover.component.scss'
})
export class PopoverComponent {
  @Input() generateSite(downloadSiteRequest: DownloadSiteRequest) {};
  @Input() siteName: string = "";
  @Input() isOpened: boolean = false;
  @Output() isOpenedChange = new EventEmitter<boolean>();
  @Input() siteDownloadUrl: string = "";

  closePopover = () => {
    this.isOpened = false;
    this.isOpenedChange.emit(false);
  }
  handleClick = () => {
    console.log(this.siteName);
    const downloadSiteRequest: DownloadSiteRequest = {
      userId: localStorage.getItem("userId")!,
      fileName: this.siteName,
    }
    this.generateSite(downloadSiteRequest);
  }
  handleDownloadClick = () => {
    console.log(this.siteDownloadUrl);
  };
}
