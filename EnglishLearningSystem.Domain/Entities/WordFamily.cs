using EnglishLearningSystem.Domain.Common;

namespace EnglishLearningSystem.Domain.Entities
{
    public class WordFamily : BaseEntity
    {
        public int Id { get; set; }
        public string FamilyName { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
