import {Component, EventEmitter, Input, Output} from '@angular/core';
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-tabs',
  standalone: true,
  imports: [
    NgForOf
  ],
  templateUrl: './tabs.component.html',
  styleUrl: './tabs.component.scss'
})
export class TabsComponent {
  tabsArray: string[] = ['Оформление сайта', 'Контент'];
  @Input() activeTab: number = 0;

  @Output() tabChange = new EventEmitter<number>();

  changeTab(i: number): void {
    this.activeTab = i;
    this.tabChange.emit(this.activeTab);
  }
}
