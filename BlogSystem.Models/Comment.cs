using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class Comment:BaseEntity
    {
        [ForeignKey("User")]
        private Guid _UserId;
        private User _User;
        [Required,StringLength(800)]
        private string _Content;
        [ForeignKey("Article")]
        private Guid _ArticleId;
        private Article _Article;

        public Guid ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }

        public Article Article
        {
            get { return _Article; }
            set { _Article = value; }
        }

        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        public Guid UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public User User
        {
            get { return _User; }
            set { _User = value; }
        }

    }
}
