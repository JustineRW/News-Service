import { Component } from '@angular/core';
import { ArticleService } from '../../article.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  constructor(private articleService : ArticleService){}


  

}