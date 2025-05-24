import { Component, Output, EventEmitter, Input } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import { ArticleService } from '../../article.service';
import { Theme } from '../../../shared/Theme';
import { Article } from '../../../shared/Article';

@Component({
  selector: 'app-search',
  imports: [FontAwesomeModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css',
})
export class SearchComponent {
  @Input() theme!: Theme;
  @Input() numberToFetch: number = 10;
  @Output() searchResultEvent = new EventEmitter<Article[]>();

  faMagnifyingGlass = faMagnifyingGlass;

  constructor(private articleService: ArticleService) {}

  search(input: string): void {
    const searchTerms = this.cleanUserInput(input);

    this.articleService
      .getSearchResults(this.numberToFetch, searchTerms, this.theme)
      .subscribe({
        next: (results: Article[]) => {
          this.searchResultEvent.emit(results);
        },
        error: (error) => {
          console.error('Search failed:', error);
          this.searchResultEvent.emit([]);
        },
      });
  }

  private cleanUserInput(input: string) {
    return input.trim();
  }
}
