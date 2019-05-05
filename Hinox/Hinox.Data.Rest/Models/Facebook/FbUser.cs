using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Data.Rest.Models.Facebook
{
    public class FbUser
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public FbUserPicture Picture { get; set; }
        public DateTime? Birthday { get; set; }
    }
    public class FbUserPicture
    {
        public FbUserPictureData Data { get; set; }
    }
    public class FbUserPictureData
    {
        public int? Height { get; set; }
        public string Url { get; set; }
        public int? Width { get; set; }
    }
}
