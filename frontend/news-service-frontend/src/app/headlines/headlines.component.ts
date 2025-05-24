import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Article } from '../../../shared/Article';

@Component({
  selector: 'app-headlines',
  imports: [CommonModule],
  templateUrl: './headlines.component.html',
  styleUrl: './headlines.component.css',
})
export class HeadlinesComponent {
  @Input() articles: Article[] = [];
}
