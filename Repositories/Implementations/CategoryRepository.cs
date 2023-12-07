using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.DTO.CategoryDTO;
using BusinessObjects.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly Menu_minder_dbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(Menu_minder_dbContext context, IMapper mapper, ILogger<CategoryRepository> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task DeleteCategory(Category category)
        {
            try
            {
                category.Status = EnumCategoryStatus.DELETED.ToString();
                await UpdateCategory(category);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<ResultCategoryDto> FindCategoryById(int categoryId)
        {
            try
            {
                Category category = await FindCategoryEntityById(categoryId);

                if (category != null)
                {
                    ResultCategoryDto data = _mapper.Map<ResultCategoryDto>(category);
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<Category> FindCategoryEntityById(int categoryId)
        {
            try
            {
                return await _context.Categories.FirstOrDefaultAsync(category => category.CategoryId == categoryId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<List<ResultCategoryDto>> GetAllCategories()
        {
            List<ResultCategoryDto> data = new List<ResultCategoryDto>();
            try
            {
                data = await _context.Categories
                    .Where(category => category.Status != EnumCategoryStatus.DELETED.ToString())
                    .Select(category => _mapper.Map<ResultCategoryDto>(category)).ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return data;
        }

        public async Task SaveCategory(Category category)
        {
            try
            {
                this._context.Categories.Add(category);
                await this._context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task UpdateCategory(Category category)
        {
            try
            {
                this._context.Categories.Update(category);
                await this._context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
    }
}
