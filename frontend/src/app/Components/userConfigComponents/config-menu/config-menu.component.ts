import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-config-menu',
  templateUrl: './config-menu.component.html',
  styleUrls: ['./config-menu.component.css']
})
export class ConfigMenuComponent {
  @Output() outputTab = new EventEmitter<string>();
  streamers = false;

  toggle (page : string) {
    this.outputTab.emit(page);
  }
}
