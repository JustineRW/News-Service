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

  headlineInput: Article[] = [];
  searchResults: Article[] = [];
  currentCategory: NewsCategory = NewsCategory.Nation;
  categories = Object.values(NewsCategory);
  pageSizes: number[] = [5, 10, 15];
  currentPageSize: number = 5;

  constructor(private articleService: ArticleService) {}

  ngOnInit(): void {
    this.englandTheme = new Theme(
      'The Robin Read',
      'News from snowy England',
      'uk',
      'England',
      'English',
      'en',
      'Robin.png'
    );
    this.australiaTheme = new Theme(
      'The Wallaby Word',
      'News from sunny Australia',
      'au',
      'Australia',
      'Australian',
      'en',
      'Wallaby.png'
    );
    this.currentNewsTheme = this.isEnglandTheme
      ? this.englandTheme
      : this.australiaTheme;

    this.getHeadlinesByCategory(this.currentPageSize, NewsCategory.Nation);
  }

  getHeadlinesByCategory(pagesize: number, category: string) {
    this.articleService
      .getTopHeadlinesByCategory(pagesize, this.currentNewsTheme, category)
      .subscribe((headlines: Article[]) => (this.headlineInput = headlines));
  }

  onCategoryButtonClick(category: NewsCategory) {
    this.currentCategory = category;
    this.getHeadlinesByCategory(this.currentPageSize, category);
  }

  onPagesizeButtonClick(pagesize: number) {
    this.currentPageSize = pagesize;
    this.getHeadlinesByCategory(pagesize, this.currentCategory);
  }

  acceptSearchResults(eventSearchResults: Article[]) {
    this.searchResults = eventSearchResults;
  }

  changeTheme(theme: string) {
    if (theme === 'australia') {
      this.isAustraliaTheme = true;
      this.isEnglandTheme = false;
      this.searchResults = [];
      this.currentNewsTheme = this.australiaTheme;
      this.getHeadlinesByCategory(this.currentPageSize, this.currentCategory);
    }
    if (theme === 'england') {
      this.isEnglandTheme = true;
      this.isAustraliaTheme = false;
      this.searchResults = [];
      this.currentNewsTheme = this.englandTheme;
      this.getHeadlinesByCategory(this.currentPageSize, this.currentCategory);
    }
  }
}
