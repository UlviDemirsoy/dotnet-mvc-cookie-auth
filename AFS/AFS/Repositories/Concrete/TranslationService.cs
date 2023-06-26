using AFS.Dtos;
using AFS.Models;
using AFS.Repositories.Abstract;

namespace AFS.Repositories.Concrete
{
    public class TranslationService : ITranslationService
    {

        private readonly DatabaseContext ctx;
        public TranslationService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Translation model)
        {
            try
            {
                ctx.Translations.Add(model);
                ctx.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                ctx.Translations.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Translation GetById(int id)
        {
            return ctx.Translations.Find(id);
        }

        public TranslationListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new TranslationListVm();

            var list = ctx.Translations.ToList();


            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Text.ToLower().Contains(term)).ToList();
            }

            if (paging)
            {
                int pageSize = 10;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }
           
            data.TranslationList = list.AsQueryable();
            return data;
        }

        public bool Update(Translation model)
        {
            try
            {
                ctx.Translations.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
