import { Component } from '@angular/core';
import { ArticleService } from '../../article.service';
import { SearchComponent } from '../search/search.component';

@Component({
  selector: 'app-home',
  imports: [SearchComponent],
  templateUrl: './home.component.html',
})
export class HomeComponent {
    
  constructor(private articleService : ArticleService){}
  
  defaultHeadlinesToFetch : number = 10;
  headlines : string[] = [];

  ngOnInit(): void {    
    this.articleService.getTopHeadlines(this.defaultHeadlinesToFetch).subscribe((headlines: string[]) => this.headlines = headlines);
  }


}