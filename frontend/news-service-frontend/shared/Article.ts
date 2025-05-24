export class Article {
  constructor(
    public title: string,
    public url: string,
    public description?: string,
    public image?: string,
    public publishedAt?: Date,
    public source?: NewsSource
  ) {}
}

export class NewsSource {
  constructor(public name: string, public url: string) {}
}
