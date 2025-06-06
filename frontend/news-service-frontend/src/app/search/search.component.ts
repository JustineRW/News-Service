import { Component, Output, EventEmitter, Input } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import { ArticleService } from '../../article.service';
import { Theme } from '../../../shared/Theme';
import { Article } from '../../../shared/Article';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search',
  imports: [FontAwesomeModule, FormsModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css',
})
export class SearchComponent {
  @Input() theme!: Theme;
  @Input() numberToFetch: number = 10;
  @Output() searchResultEvent = new EventEmitter<Article[]>();

  searchQuery: string = '';
  faMagnifyingGlass = faMagnifyingGlass;
  MAX_QUERY_LENGTH = 100;

  constructor(private articleService: ArticleService) {}

  search(): void {
    if (this.searchQuery.trim()) {
      const searchTerms = this.cleanUserInput(this.searchQuery);

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

    this.searchQuery = '';
  }

  private cleanUserInput(input: string) {
    let searchQuery = input
      .trim()
      .replace(/[^\w\s\-'"]/g, '')
      .replace(/\s+/g, ' ');

    if (searchQuery.length > this.MAX_QUERY_LENGTH) {
      searchQuery = searchQuery.substring(0, this.MAX_QUERY_LENGTH);
    }
    return searchQuery;
  }
}
