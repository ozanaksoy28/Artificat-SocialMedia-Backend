using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserDtoBase:IDto
    {
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string ProfilePhotoPath { get; set; }
    }
}
