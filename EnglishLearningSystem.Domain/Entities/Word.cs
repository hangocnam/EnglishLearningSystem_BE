using EnglishLearningSystem.Domain.Common;
using EnglishLearningSystem.Domain.Enums;

namespace EnglishLearningSystem.Domain.Entities
{
    public class Word : BaseEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = default!;
        public PartOfSpeech PartOfSpeech { get; set; } = default!;
        public string Ipa { get; set; } = default!;
        public string EnglistDefinition { get; set; } = default!;
        public string VietnameseMeaning { get; set; } = default!;
        public int WordFamilyId { get; set; }
        public bool IsRoot { get; set; } = false;
    }
}
