namespace Library.Domain.Repositories.BookRepo.Dtos;

public class BookUpdateRequestDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string PublishCompany { get; set; }
    public string Subject { get; set; }
}
