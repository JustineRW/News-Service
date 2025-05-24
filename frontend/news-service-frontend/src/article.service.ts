import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  constructor(private http: HttpClient) { }
  private topHeadlinesUrl = "";//"http://localhost:5056/api/articles?search=orchard"

  getTopHeadlines(defaultHeadlinesToFetch: number) : Observable<string[]> {
    return this.http.get<string[]>(this.topHeadlinesUrl)
  }

}
