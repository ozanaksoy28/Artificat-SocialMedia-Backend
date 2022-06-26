using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserProfileDto: UserDtoBase,IDto
    {
        public string Description { get; set; }
        public int PostCount { get; set; }
        public int FollowingCount { get; set; }
        public int FollowerCount { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
    }
}
