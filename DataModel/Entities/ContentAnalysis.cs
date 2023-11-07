namespace DataModel.Entities
{
    public class ContentAnalysis
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();  
        public string? CrawledId { get; set; }
        public string? MetaTagKeywords { get; set; }
        public string? KeywordFrequency { get; set; }
        public string? Headings { get; set;}
        public string? Title { get; set; }
        public string? MetaDescription { get; set; }
        public string? ImageDetail { get; set; }
        public string? InternalLinks { get; set; }
        public string? ExternalLinks { get; set; }
        public string? URLStructure { get; set; }
        public virtual Crawled? Crawled { get; set; }
    }
}
