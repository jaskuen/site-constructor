import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {TabsComponent} from "../../../components/tabs/tabs.component";
import {NgIf} from "@angular/common";
import {SelectComponent} from "../../../components/select/select.component";
import {RouterOutlet} from "@angular/router";
import {DesignComponent} from "./design/design.component";
import {ContentComponent} from "./content/content.component";
import {ContentPageData, DesignPageData} from "../../../../types";

@Component({
  selector: 'app-site-creator',
  standalone: true,
  imports: [
    TabsComponent,
    NgIf,
    SelectComponent,
    RouterOutlet,
    DesignComponent,
    ContentComponent
  ],
  templateUrl: './site-creator.component.html',
  styleUrl: './site-creator.component.scss'
})
export class SiteCreatorComponent {
  @Input() designPageData!: DesignPageData;
  @Input() contentPageData!: ContentPageData;
  @Output() designPageDataChange = new EventEmitter<DesignPageData>();
  @Output() contentPageDataChange = new EventEmitter<ContentPageData>();

  onDesignChange() {
    console.log('changes')
    this.designPageDataChange.emit(this.designPageData);
  }

  onContentChange() {
    console.log('changes')
    this.contentPageDataChange.emit(this.contentPageData);
  }

  currentPageNumber: number = 0;
  setCurrentPageNumber(tab: number) {
    this.currentPageNumber = tab;
  }
}
