namespace G4G.Api
{
    public class CategoryDto
    {
        public CategoryDto()
        {
            SubCategory = new HashSet<SubCategoryDto>();
        }

        public int IdCategory { get; set; }
        public string Name { get; set; }

        public ICollection<SubCategoryDto> SubCategory { get; set; }

    }
}

