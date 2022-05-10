namespace G4G.Api
{
    public class SubCategoryDto
    {
        public int IdSubcategory { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public ContentDto lastContentInSubCategory { get; set; }
        public int totalContentsInSubCategory { get; set; }
        public int totalCommentInInSubCategory { get; set; }

    }
}
