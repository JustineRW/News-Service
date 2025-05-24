import { Component, Input } from '@angular/core';
import { Theme } from '../../../shared/Theme';

@Component({
  selector: 'app-titlecard',
  imports: [],
  templateUrl: './titlecard.component.html',
  styleUrl: './titlecard.component.css'
})
export class TitlecardComponent {
  @Input() theme! : Theme;

}
