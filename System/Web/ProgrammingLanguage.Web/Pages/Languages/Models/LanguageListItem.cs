namespace ProgrammingLanguage.Web.Pages.Languages.Models
{
    public class LanguageListItem
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
