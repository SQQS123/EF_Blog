using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Dto
{
    public class UserInformationDto
    {
        private Guid _Id;
        private string _Email;
        private string _ImagePath;
        private string _SiteName;
        private int _FansCount;
        private int _FocusCount;

        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
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

    }
}
