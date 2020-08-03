using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class Article:BaseEntity
    {
        [Required]
        private string _Title;

        [Required, Column(TypeName = "ntext")]
        private string _Content;

        [ForeignKey("User")]
        private Guid _UserId;

        private User _User;

        private int _GoodCount;

        public int GoodCount
        {
            get { return _GoodCount; }
            set { _GoodCount = value; }
        }
        private int _BadCount;

        public int BadCount
        {
            get { return _BadCount; }
            set { _BadCount = value; }
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

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
    }
}
