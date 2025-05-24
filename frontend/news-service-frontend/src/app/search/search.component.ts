import { Component, Output, EventEmitter } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import { ArticleService } from '../../article.service';

@Component({
  selector: 'app-search',
  imports: [FontAwesomeModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent {
  @Output() searchResultEvent = new EventEmitter<string[]>();

  faMagnifyingGlass = faMagnifyingGlass;
  searchResults: string[] = [];

  constructor(private articleService : ArticleService){}

  search(input : string) : void {    
    let searchTerms = this.cleanUserInput(input);
    this.articleService.getSearchResults(searchTerms).subscribe((results: string[]) => this.searchResults = results);
    this.searchResultEvent.emit(this.searchResults);
  }

  private cleanUserInput(input: string) {
    return input.trim().split(" ").join("+");
  }
}
