namespace G4G.Api
{
    public class ContentDto
    {
        public ContentDto()
        {
            Comment = new HashSet<CommentDto>();
        }

        public int IdContent { get; set; }
        public string Headline { get; set; }
        public string Text { get; set; }
        public int? Views { get; set; }
        public DateTime Posted { get; set; }
        public int AccountIdAccount { get; set; }
        public string AccountUsername { get; set; }
        public int SubcategoryIdSubcategory { get; set; }
        public AccountDto Account { get; set; }

        public virtual ICollection<CommentDto> Comment { get; set; }
        public int CommentsCount { get; set; }
    }
}
