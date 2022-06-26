using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class PostDtoBase:UserDtoBase,IDto
    {
        public int PostId { get; set; }
        public string PostPath { get; set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }
        public int LikeCount { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
