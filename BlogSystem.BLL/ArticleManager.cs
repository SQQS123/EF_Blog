using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.DAL;
using BlogSystem.Dto;
using BlogSystem.IBLL;
using BlogSystem.IDAL;
using BlogSystem.Models;

namespace BlogSystem.BLL
{
    public class ArticleManager:IArticleManager
    {
        public async Task CreateArticle(string title, string content, Guid[] categoryIds, Guid userId)
        {
            using (var articleSvc=new ArticleService())
            {
                var article = new Article()
                {
                    Title = title,
                    Content = content,
                    UserId = userId
                };
                await articleSvc.CreateAsync(article);

                Guid articleId = article.Id;
                using (var articleToCategorySvc = new ArticleToCategoryService())
                {
                    foreach (var categoryId in categoryIds)
                    {
                        await articleToCategorySvc.CreateAsync(new ArticleToCategory()
                        {
                            ArticleId = articleId,
                            BlogCategoryId = categoryId
                        }, saved: false);
                    }

                    await articleToCategorySvc.Save();
                }
            }
        }

        public async Task CreateCategory(string name, Guid userId)
        {
            using (var categorySvc=new BlogCategoryService())
            {
                await categorySvc.CreateAsync(new BlogCategory()
                {
                    CategoryName = name,
                    UserId = userId,
                });
            }
        }

        public async Task<List<BlogCategoryDto>> GetAllCategories(Guid userId)
        {
            using (IDAL.IBlogCategory categoryService=new BlogCategoryService())
            {
                return await categoryService.GetAllAsync().Where(m=>m.UserId==userId).Select(m => new Dto.BlogCategoryDto()
                {
                    Id = m.Id,
                    CategoryName = m.CategoryName
                }).ToListAsync();
            }
        }

        public async Task<List<ArticleDto>> GetAllArticlesByUserId(Guid userId, int pageIndex, int pageSize)
        {
            using (IDAL.IArticleService articleService=new ArticleService())
            {
                var list = await articleService.GetAllByPageOrderAsync(pageSize,pageIndex,false).Include(m=>m.User).Where(m => m.UserId == userId)
                    .Select(m => new Dto.ArticleDto()
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Content = m.Content,
                        CreateTime = m.CreateTime,
                        GoodCount = m.GoodCount,
                        BadCount = m.BadCount,
                        Email = m.User.Email,
                        ImagePath = m.User.ImagePath
                    }).ToListAsync();
                using (IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    foreach (var articleDto in list)
                    {
                        var cates = await articleToCategoryService.GetAllAsync().Include(m=>m.BlogCategory).Where(m => m.ArticleId == articleDto.Id).ToListAsync();
                        articleDto.CategoryIds = cates.Select(m => m.BlogCategoryId).ToArray();
                        articleDto.CategoryNames = cates.Select(m => m.BlogCategory.CategoryName).ToArray();
                    }

                    return list;
                }
            }
        }

        public async Task<int> GetDataCount(Guid userId)
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                return await articleService.GetAllAsync().CountAsync(m => m.UserId == userId);
            }
        }

        public async Task<List<ArticleDto>> GetAllArticlesByUserEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ArticleDto>> GetAllArticlesByCategoryId(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveCategory(string name)
        {
            throw new NotImplementedException();
        }

        public async Task EditCategory(Guid categoryId, string newCategoryName)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveArticle(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public async Task EditArticle(Guid articleId, string title, string content, Guid[] categoryIds)
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                var article = await articleService.GetOneByIdAsync(articleId);
                article.Title = title;
                article.Content = content;
                await articleService.EditAsync(article);
                using (IDAL.IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    foreach (var categoryId in articleToCategoryService.GetAllAsync().Where(m=>m.Id==articleId))
                    {
                        await articleToCategoryService.RemoveAsync(categoryId, false);
                    }

                    foreach (var categoryId in categoryIds)
                    {
                        await articleToCategoryService.CreateAsync(new ArticleToCategory()
                        {
                            ArticleId = articleId,
                            BlogCategoryId = categoryId
                        },false);
                    }
                    await articleToCategoryService.Save();
                }
            }
        }

        public async Task<bool> ExistsArticle(Guid articleId)
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                return await articleService.GetAllAsync().AnyAsync(m => m.Id == articleId);
            }
        }

        public async Task<ArticleDto> GetOneArticleById(Guid articleId)
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                var data = await articleService.GetAllAsync()
                    .Include(m=>m.User)
                    .Where(m => m.Id == articleId)
                    .Select(m => new Dto.ArticleDto()
                    {
                        Id = m.Id,
                        BadCount = m.BadCount,
                        Title = m.Title,
                        Content = m.Content,
                        CreateTime = m.CreateTime,
                        Email = m.User.Email,
                        GoodCount = m.GoodCount,
                        ImagePath = m.User.ImagePath
                    }).FirstAsync();

                using (IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                        var cates = await articleToCategoryService.GetAllAsync().Include(m => m.BlogCategory)
                            .Where(m => m.ArticleId == data.Id).ToListAsync();
                        data.CategoryIds = cates.Select(m => m.BlogCategoryId).ToArray();
                        data.CategoryNames = cates.Select(m => m.BlogCategory.CategoryName).ToArray();
                        return data;
                }
            }
        }

        public async Task CreateComment(Guid userId, Guid articleId, string content)
        {
            using (IDAL.ICommentService commentService = new CommentService())
            {
                await commentService.CreateAsync(new Comment()
                {
                    UserId = userId,
                    ArticleId = articleId,
                    Content = content
                });
            }
        }

        public async Task<List<Dto.CommentDto>> GetCommentsByArticleId(Guid articleId)
        {
            using (IDAL.ICommentService commentService = new CommentService())
            {
                return await commentService.GetAllAsync().Where(m => m.ArticleId == articleId)
                    .Include(m=>m.User)
                    .Select(m=>new Dto.CommentDto()
                    {
                        Id =  m.Id,
                        ArticleId = m.ArticleId,
                        UserId = m.UserId,
                        Email = m.User.Email,
                        Content = m.Content,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
            }
        }
    }
}
