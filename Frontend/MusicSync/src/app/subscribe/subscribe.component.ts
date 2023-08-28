import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-subscribe',
  templateUrl: './subscribe.component.html',
  styleUrls: ['./subscribe.component.css']
})
export class SubscribeComponent {
  @Output() loginClickEvent = new EventEmitter();
  loginClicked ( ) {
    this.loginClickEvent.emit();
  }
}
