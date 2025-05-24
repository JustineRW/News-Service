import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-titlecard',
  imports: [],
  templateUrl: './titlecard.component.html',
  styleUrl: './titlecard.component.css'
})
export class TitlecardComponent {
  @Input() title : string = "";
  @Input() subheader : string  = "";

}
