import {Component, EventEmitter, Input, Output} from '@angular/core';
import {NgForOf} from "@angular/common";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-tabs',
  standalone: true,
  imports: [
    NgForOf,
    RouterLink
  ],
  templateUrl: './tabs.component.html',
  styleUrl: './tabs.component.scss'
})
export class TabsComponent {
  @Input() activeTab: number = 0;
  @Output() tabChange = new EventEmitter<number>();

  tabsArray: string[] = ['Оформление сайта', 'Контент']

  changeTab(i: number): void {
    this.activeTab = i;
    this.tabChange.emit(this.activeTab);
  }
}
