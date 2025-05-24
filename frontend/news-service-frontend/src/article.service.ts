import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Theme } from '../shared/Theme';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  constructor(private http: HttpClient) { }
  private baseUrl = new URL("https://localhost:7201/api/");
  // private articleUrlNorway = new URL("");//"articles?country=no"
  // private articleUrl = new URL(this.baseUrl +"articles");//""

  getTopHeadlines(numberOfHeadlinesToFetch: number, currentNewsTheme : Theme) : Observable<string[]> {

    const numberOfHeadlinesToFetchAsString : string = numberOfHeadlinesToFetch.toString();
    this.articleUrl.searchParams.append("pagesize", numberOfHeadlinesToFetchAsString)
    this.articleUrl.searchParams.append("country", currentNewsTheme.countryCode)
    this.articleUrl.searchParams.append("language", currentNewsTheme.language)
    this.articleUrl.searchParams.append("category", "nation")
    console.log(this.articleUrl.toString())
    return this.http.get<string[]>(this.articleUrl.toString());
  }

  getSearchResults(query: string, currentNewsTheme : Theme) : Observable<string[]> {
    this.articleUrl.searchParams.append("search",query)
    this.articleUrl.searchParams.append("country", currentNewsTheme.countryCode)
    this.articleUrl.searchParams.append("language", currentNewsTheme.language)
    console.log(this.articleUrl.toString())
    return this.http.get<string[]>(this.articleUrl.toString());
  }

  getTopHeadlinesByCategory(query: string, currentNewsTheme : Theme, category : string) : Observable<string[]> {
    const numberOfHeadlinesToFetchAsString : string = numberOfHeadlinesToFetch.toString();
    this.articleUrl.searchParams.append("pagesize", numberOfHeadlinesToFetchAsString)
    this.articleUrl.searchParams.append("country", currentNewsTheme.countryCode)
    this.articleUrl.searchParams.append("language", currentNewsTheme.language)
    this.articleUrl.searchParams.append("category", category)
    console.log(this.articleUrl.toString())
    return this.http.get<string[]>(this.articleUrl.toString());
  }

}
