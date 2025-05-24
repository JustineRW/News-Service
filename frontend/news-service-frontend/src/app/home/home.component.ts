import { Component } from '@angular/core';
import { ArticleService } from '../../article.service';
import { SearchComponent } from '../search/search.component';
import { HeadlinesComponent } from '../headlines/headlines.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {
  faSun,
  faSnowflake,
  IconDefinition,
} from '@fortawesome/free-solid-svg-icons';
import { TitlecardComponent } from '../titlecard/titlecard.component';
import { CommonModule } from '@angular/common';
import { Theme } from '../../../shared/Theme';
import { NewsCategory } from '../../../shared/NewsCategory';
import { Article } from '../../../shared/Article';

@Component({
  selector: 'app-home',
  imports: [
    SearchComponent,
    HeadlinesComponent,
    FontAwesomeModule,
    TitlecardComponent,
    CommonModule,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  faSun: IconDefinition = faSun;
  faSnowflake: IconDefinition = faSnowflake;
  isEnglandTheme: boolean = false;
  isAustraliaTheme: boolean = true;
  englandTheme!: Theme;
  australiaTheme!: Theme;
  currentNewsTheme!: Theme;

  numberHeadlinesToFetch: number = 10;
  headlineInput: Article[] = [];
  searchResults: Article[] = [];
  currentCategory: NewsCategory = NewsCategory.Nation;
  categories = Object.values(NewsCategory);

  constructor(private articleService: ArticleService) {}

  ngOnInit(): void {
    this.englandTheme = new Theme(
      'The Robin Read',
      'News from snowy England',
      'uk',
      'England',
      'en'
    );
    this.australiaTheme = new Theme(
      'The Wallaby Word',
      'News from sunny Australia',
      'au',
      'Australia',
      'en'
    );
    this.currentNewsTheme = this.isEnglandTheme
      ? this.englandTheme
      : this.australiaTheme;

    // this.getHeadlinesByCategory(NewsCategory.Nation);
  }

  private getHeadlinesByCategory(category: string) {
    this.articleService
      .getTopHeadlinesByCategory(
        this.numberHeadlinesToFetch,
        this.currentNewsTheme,
        category
      )
      .subscribe((headlines: Article[]) => (this.headlineInput = headlines));
  }

  onCategoryButtonClick(category: NewsCategory) {
    this.currentCategory = category;
    this.getHeadlinesByCategory(category);
  }

  acceptSearchResults(eventSearchResults: Article[]) {
    this.searchResults = eventSearchResults;
  }

  changeTheme(theme: string){
    if(theme === 'australia'){
      this.isAustraliaTheme = true;
      this.isEnglandTheme = false;
      this.currentNewsTheme = this.australiaTheme;
    }
    if(theme === 'england'){
      this.isEnglandTheme = true;
      this.isAustraliaTheme = false;
      this.currentNewsTheme = this.englandTheme;
    }
  }
}

