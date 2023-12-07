using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.DTO.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<ResultCategoryDto>> GetAllCategories();
        public Task SaveCategory(Category cartegory);
        public Task<ResultCategoryDto> FindCategoryById(int categoryId);
        public Task<Category> FindCategoryEntityById(int categoryId);
        public Task UpdateCategory(Category category);
        public Task DeleteCategory(Category category);
    }
}
