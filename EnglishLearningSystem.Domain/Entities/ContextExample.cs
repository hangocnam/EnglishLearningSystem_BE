using EnglishLearningSystem.Domain.Common;

namespace EnglishLearningSystem.Domain.Entities
{
    public class ContextExample : BaseEntity
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public string EnglishSentence { get; set; } = default!;
        public string VietnameseTranslation { get; set; } = default!;
    }
}