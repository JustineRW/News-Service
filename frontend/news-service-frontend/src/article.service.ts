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
  private baseUrl = 'http://localhost:5056/api/articles'; // Move this into a config file

  // Gets top headlines, regardless of country or category. Not currently used
  getTopHeadlines(
    numberOfHeadlines: number,
    currentNewsTheme: Theme
  ): Observable<Article[]> {
    let articleUrl = new URL(this.baseUrl);

    articleUrl.searchParams.append('pagesize', numberOfHeadlines.toString());
    articleUrl.searchParams.append('language', currentNewsTheme.language);

    return this.http.get<Article[]>(articleUrl.toString());
  }

  getSearchResults(
    numberOfHeadlines: number,
    query: string,
    currentNewsTheme: Theme
  ): Observable<Article[]> {
    let articleUrl = new URL(this.baseUrl);

    articleUrl.searchParams.append('pagesize', numberOfHeadlines.toString());
    articleUrl.searchParams.append('search', query);
    articleUrl.searchParams.append('country', currentNewsTheme.countryCode);
    articleUrl.searchParams.append('language', currentNewsTheme.language);

    return this.http.get<Article[]>(articleUrl.toString());
  }

  getTopHeadlinesByCategory(
    numberOfHeadlines: number,
    currentNewsTheme: Theme,
    category: string
  ): Observable<Article[]> {
    let articleUrl = new URL(this.baseUrl);

    articleUrl.searchParams.append('pagesize', numberOfHeadlines.toString());
    articleUrl.searchParams.append('country', currentNewsTheme.countryCode);
    articleUrl.searchParams.append('language', currentNewsTheme.language);
    articleUrl.searchParams.append('category', category);

    return this.http.get<Article[]>(articleUrl.toString());
  }
}
