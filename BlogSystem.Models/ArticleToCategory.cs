using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class ArticleToCategory:BaseEntity
    {
        [ForeignKey("BlogCategory")]
        private Guid _BlogCategoryId;
        private BlogCategory _BlogCategory;
        [ForeignKey("Article")]
        private Guid _ArticleId;

        public Guid ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        private Article _Article;

        public Article Article
        {
            get { return _Article; }
            set { _Article = value; }
        }

        public BlogCategory BlogCategory
        {
            get { return _BlogCategory; }
            set { _BlogCategory = value; }
        }

        public Guid BlogCategoryId
        {
            get { return _BlogCategoryId; }
            set { _BlogCategoryId = value; }
        }

    }
}
