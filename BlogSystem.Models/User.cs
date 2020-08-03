using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class User:BaseEntity
    {
        [Required,StringLength(40),Column(TypeName="varchar")]
        private string _Email;
        [Required,StringLength(30),Column(TypeName="varchar")]
        private string _Password;
        [Required,StringLength(200),Column(TypeName = "varchar")]
        private string _ImagePath;
        private int _FansCount;
        private int _FocusCount;
        private string _SiteName;

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }

        public int FansCount
        {
            get { return _FansCount; }
            set { _FansCount = value; }
        }
        

        public int FocusCount
        {
            get { return _FocusCount; }
            set { _FocusCount = value; }
        }



        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

      
    }
}
