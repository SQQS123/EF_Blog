using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class BlogCategory:BaseEntity
    {

        private string _CategoryName;
        //用户外键
        [ForeignKey("User")]
        private Guid _UserId;
        private User _User;

        public User User
        {
            get { return _User; }
            set { _User = value; }
        }

        public Guid UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
    }
}
