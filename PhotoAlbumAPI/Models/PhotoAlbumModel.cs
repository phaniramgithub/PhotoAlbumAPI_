using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoAlbumAPI.Models
{
    public class PhotoAlbumModel
    {
        public int AlbumId { get; set; }
        public int UserId { get; set; }       
        public string AlbumTitle { get; set; }
        public int PhotoId { get; set; }
        public string PhotoTitle { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
