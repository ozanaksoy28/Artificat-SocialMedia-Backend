using Amazon.S3;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAWSS3Service
    {
        public IDataResult<string> PostToAWS(FileStream image,Post post);
        public IDataResult<string> PostToAWS(FileStream image, User user);
    }
}
