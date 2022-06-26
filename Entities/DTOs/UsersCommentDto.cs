using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UsersCommentDto: UserDtoBase,IDto
    {
        public int PostId { get; set; }
        public string CommentMessage { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
