namespace e_handelsystem.Models
{
    public class CategoriesCreateModel
    {
        public string Name { get; set; }

        public CategoriesCreateModel(string name)
        {
            Name = name;
        }
    }
}
