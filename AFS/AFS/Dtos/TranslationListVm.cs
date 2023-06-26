using AFS.Models;

namespace AFS.Dtos
{
    public class TranslationListVm
    {
        public IQueryable<Translation> TranslationList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}
