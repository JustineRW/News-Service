import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Theme } from '../shared/Theme';
import { Article } from '../shared/Article';

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  constructor(private http: HttpClient) {}
  private baseUrl = 'https://localhost:7201/api/articles';

  getTopHeadlines(
    numberOfHeadlinesToFetch: number,
    currentNewsTheme: Theme
  ): Observable<Article[]> {
    let articleUrl = new URL(this.baseUrl);

    articleUrl.searchParams.append(
      'pagesize',
      numberOfHeadlinesToFetch.toString()
    );
    articleUrl.searchParams.append('country', currentNewsTheme.countryCode);
    articleUrl.searchParams.append('language', currentNewsTheme.language);
    return this.http.get<Article[]>(articleUrl.toString());
  }

  getSearchResults(
    numberOfHeadlinesToFetch: number,
    query: string,
    currentNewsTheme: Theme
  ): Observable<Article[]> {
    let articleUrl = new URL(this.baseUrl);
    articleUrl.searchParams.append(
      'pagesize',
      numberOfHeadlinesToFetch.toString()
    );
    articleUrl.searchParams.append('search', encodeURIComponent(query));
    articleUrl.searchParams.append('country', currentNewsTheme.countryCode);
    articleUrl.searchParams.append('language', currentNewsTheme.language);
    return this.http.get<Article[]>(articleUrl.toString());
  }

  getTopHeadlinesByCategory(
    numberOfHeadlinesToFetch: number,
    currentNewsTheme: Theme,
    category: string
  ): Observable<Article[]> {
    let articleUrl = new URL(this.baseUrl);
    articleUrl.searchParams.append(
      'pagesize',
      numberOfHeadlinesToFetch.toString()
    );
    articleUrl.searchParams.append('country', currentNewsTheme.countryCode);
    articleUrl.searchParams.append('language', currentNewsTheme.language);
    articleUrl.searchParams.append('category', category);
    return this.http.get<Article[]>(articleUrl.toString());
    return new Observable<Article[]>();
  }
}
