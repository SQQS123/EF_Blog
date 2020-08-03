using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class Fans:BaseEntity
    {
        [ForeignKey("User")]
        private Guid _UserId;
        private User User;

        [ForeignKey("FocusUser")]
        private Guid _FocusUserId;
        private User FocusUser;

        public Guid UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        
        public User User1
        {
            get { return User; }
            set { User = value; }
        }
        
        public Guid FocusUserId
        {
            get { return _FocusUserId; }
            set { _FocusUserId = value; }
        }
        
        public User FocusUser1
        {
            get { return FocusUser; }
            set { FocusUser = value; }
        }
    }
}
