import { Component } from '@angular/core';
import { ArticleService } from '../../article.service';
import { SearchComponent } from '../search/search.component';
import { HeadlinesComponent } from '../headlines/headlines.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSun, faSnowflake, IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { TitlecardComponent } from '../titlecard/titlecard.component';
import { CommonModule } from '@angular/common';
import { Theme } from '../../../shared/Theme';

@Component({
  selector: 'app-home',
  imports: [SearchComponent, HeadlinesComponent, FontAwesomeModule, TitlecardComponent, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  
  faSun : IconDefinition = faSun;
  faSnowflake : IconDefinition = faSnowflake;
  isNorwayTheme : boolean = false;
  isAustraliaTheme : boolean = true;  
 
  defaultHeadlinesToFetch : number = 10;
  headlineInput : string[] = [];
  searchResults : string[] = [];
  norwayTheme: Theme | undefined;
  australiaTheme: Theme | undefined;

  constructor(private articleService : ArticleService){}

  ngOnInit(): void {    
    this.norwayTheme = new Theme("The Norway News","News from snowy Norway","no","no")
    this.australiaTheme = new Theme("The Sydney Star","News from sunny Australia","au","en")
    let currentNewsTheme = this.isNorwayTheme ? this.norwayTheme : this.australiaTheme;
    
    this.articleService.getTopHeadlines(this.defaultHeadlinesToFetch, currentNewsTheme).subscribe((headlines: string[]) => this.headlineInput = headlines);
    // this.headlineInput = ["Wow such headline","Amazing placeholder","So interesting"]
  }

  acceptSearchResults(searchResults : string[]){
    this.searchResults = searchResults;
  }
}

