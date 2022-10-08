﻿namespace Library.Domain.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string PublishCompany { get; set; }
    public string Subject { get; set; }

    public Book()
    {
    }

    public Book(string title, string author, string publishCompany, string subject)
    {
        this.Author = author;
        this.Title = title;
        this.PublishCompany = publishCompany;
        this.Subject = subject;
    }
}
