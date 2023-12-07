using BusinessObjects.DataModels;
using BusinessObjects.DTO.CategoryDTO;
using BusinessObjects.Enum;
using Repositories.Implementations;

namespace Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<List<ResultCategoryDto>> GetAllCategories()
        {
            List<ResultCategoryDto> categoryResults = await this._categoryRepository.GetAllCategories();
            return categoryResults;
        }

        public async Task<Category> FindCategoryEntityById(int categoryId)
        {
            return await _categoryRepository.FindCategoryEntityById(categoryId);
        }

        public async Task<ResultCategoryDto> FindCategoryById(int categoryId)
        {
            return await _categoryRepository.FindCategoryById(categoryId);
        }

        public async Task CreateCategory(CreateCategoryDto dataInvo)
        {
            Category categoryCreate = new Category();
            // create data
            categoryCreate.CategoryName = dataInvo.CategoryName;
            categoryCreate.Status = EnumCategoryStatus.ACTIVE.ToString();
            categoryCreate.CreatedAt = DateTime.Now;
            categoryCreate.UpdatedAt = DateTime.Now;
            // save data
            await _categoryRepository.SaveCategory(categoryCreate);
        }

        public async Task UpdateCategory(CreateCategoryDto dataInvo, int categoryId)
        {
            Category categoryUpdate = await FindCategoryEntityById(categoryId);

            if (categoryUpdate == null)
            {
                throw new Exception($"Cannot find category with id: {categoryId}");

            }

            // set data
            categoryUpdate.CategoryName = dataInvo.CategoryName;
            categoryUpdate.UpdatedAt = DateTime.Now;
            // update data
            await _categoryRepository.UpdateCategory(categoryUpdate);
        }

        public async Task DeleteCategoryById(int categoryId)
        {
            Category category = await FindCategoryEntityById(categoryId);

            if (category == null)
            {
                throw new Exception($"Cannot find category with id: {categoryId}");
            }

            await _categoryRepository.DeleteCategory(category);
        }
    }
}

