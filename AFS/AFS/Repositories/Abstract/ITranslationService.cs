using AFS.Dtos;
using AFS.Models;

namespace AFS.Repositories.Abstract
{
    public interface ITranslationService
    {
        bool Add(Translation model);
        bool Update(Translation model);
        Translation GetById(int id);
        bool Delete(int id);
        TranslationListVm List(string term = "", bool paging = false, int currentPage = 0);
    }
}
