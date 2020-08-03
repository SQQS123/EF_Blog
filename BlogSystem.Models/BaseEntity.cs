using System;

namespace BlogSystem.Models
{
    public class BaseEntity
    {
        /// <summary>
        /// 编号：_Id
        /// 创建时间:_CreateTime
        /// 是否被删除:_IsRemoved(伪删除)
        /// </summary>
        private Guid _Id = Guid.NewGuid();
        private DateTime _CreateTime = DateTime.Now;
        private bool _IsRemoved;

        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }

        public bool IsRemoved
        {
            get { return _IsRemoved; }
            set { _IsRemoved = value; }
        }
        
    }
}
