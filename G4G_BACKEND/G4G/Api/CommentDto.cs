namespace G4G.Api
{
    public class CommentDto
    {
        public int IdComment { get; set; }
        public string Text { get; set; }
        public DateTime Posted { get; set; }
        public int AccountIdAccount { get; set; }
        public string AccountUsername { get; set; }
        public int ContentIdContent { get; set; }
        public AccountDto Account { get; set; }

    }
}
