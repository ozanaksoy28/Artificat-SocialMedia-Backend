using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User:IEntity
    {
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string ProfilePhotoPath { get; set; }
        public string Description { get; set; }
        public string PasswordHashed { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }

        //public int id { get => Id; set => Id = value; }
        //public string nickName { get => NickName; set => NickName = value; }
        //public string email { get => Email; set => Email = value; }
        //public string profilePhotoPath { get => ProfilePhotoPath; set => ProfilePhotoPath = value; }
        //public string passwordHash { get => PasswordHash; set => PasswordHash = value; }
    }
}
