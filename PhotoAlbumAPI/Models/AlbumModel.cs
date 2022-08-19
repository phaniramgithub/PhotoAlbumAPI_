using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoAlbumAPI.Models
{
    public class AlbumModel
    {

        public int ID { get; set; }
        public int userId { get; set; }
        public string Title { get; set; }
    }
}
