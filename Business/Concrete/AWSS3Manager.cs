using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AWSS3Manager : IAWSS3Service
    {
        AmazonS3Client client = new AmazonS3Client(AWSConfigKeys.accessKey, AWSConfigKeys.secretKey,
            AWSConfigKeys.bucketRegion);
        public IDataResult<string> PostToAWS(FileStream image,Post post)
        {
            var today = DateTime.Now.ToString("yyyyMMddHHmmss");


            PutObjectRequest request = new PutObjectRequest()
            {
                InputStream = image,
                BucketName = AWSConfigKeys.bucketName,
                CannedACL = S3CannedACL.PublicRead,
                Key = $"images/{post.UserId}/{today}"
            };
            try
            {

                var response = client.PutObjectAsync(request).GetAwaiter().GetResult();
                //Console.WriteLine("Uploaded Succesfully");
                return new SuccessDataResult<string>("https://mtm-bitirme.s3.amazonaws.com/" + request.Key, "Gönderi paylaşıldı.");
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                return new ErrorDataResult<string>(null,amazonS3Exception.Message);
            }
        }
        public IDataResult<string> PostToAWS(FileStream image, User user)
        {
            PutObjectRequest request = new PutObjectRequest()
            {
                InputStream = image,
                BucketName = AWSConfigKeys.bucketName,
                CannedACL = S3CannedACL.PublicRead,
                Key = $"images/{user.UserId}/ProfilePhoto"
            };
            try
            {

                var response = client.PutObjectAsync(request).GetAwaiter().GetResult();
                //Console.WriteLine("Uploaded Succesfully");
                return new SuccessDataResult<string>("https://mtm-bitirme.s3.amazonaws.com/" + request.Key, "Profil Fotoğrafı paylaşıldı.");
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                return new ErrorDataResult<string>(null, amazonS3Exception.Message);
            }
        }
    }
}
