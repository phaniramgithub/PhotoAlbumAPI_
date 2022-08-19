using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoAlbumAPI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace PhotoAlbumAPI
{
    public interface IPhotoAlbumService
    {
        Task<List<PhotoAlbumModel>> GetByUser(int uid = 0);
        Task<List<PhotoAlbumModel>> GetAll();
    }
    public class PhotoAlbumService : IPhotoAlbumService
    {
        private HttpClient _httpClient = null;

        public PhotoAlbumService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Gets the details of Photo Album details. 
        /// If user enters a specific 
        /// </summary>
        /// <returns></returns>
        public async Task<List<PhotoAlbumModel>> GetAll()
        {
            IEnumerable<PhotoModel> photos = null;
            IEnumerable<AlbumModel> albums = null;
            List<PhotoAlbumModel> photodetails = null;
            try
            {
                HttpResponseMessage res = await _httpClient.GetAsync("photos");
                if (res.IsSuccessStatusCode)
                {
                    photos = await res.Content.ReadFromJsonAsync<List<PhotoModel>>();
                }
                else
                {
                    string msg = "Reponse failed!";
                    Console.WriteLine(msg);
                    throw new Exception(msg);
                }

                res = await _httpClient.GetAsync("albums");
                if (res.IsSuccessStatusCode)
                {
                    albums = await res.Content.ReadFromJsonAsync<List<AlbumModel>>();
                }
                else
                {
                    string msg = "Reponse failed!";
                    Console.WriteLine(msg);
                    throw new Exception(msg);
                }

                // Get the data set usng two endpoints and combined them as a single set mapping
                // the albumID in both photos and albums
                var allDetails = from album in albums
                                 join photo in photos
                                               on album.ID equals photo.albumId
                                 select new
                                 {
                                     album.ID,
                                     album.Title,
                                     album.userId,
                                     photo.id,
                                     photo.title,
                                     photo.url,
                                     photo.thumbnailUrl
                                 };

                photodetails = allDetails.
                    Select(m => new PhotoAlbumModel
                    {
                        AlbumId = m.ID,
                        AlbumTitle = m.Title,
                        UserId = m.userId,
                        PhotoId = m.id,
                        PhotoTitle = m.title,
                        Url = m.url,
                        ThumbnailUrl = m.thumbnailUrl
                    }).ToList();

            }
            catch (HttpRequestException) // Non success
            {
                Console.WriteLine("An error occurred.");
            }
            catch (NotSupportedException) // When content type is not valid
            {
                Console.WriteLine("The content type is not supported.");
            }
            catch (JsonException) // Invalid JSON
            {
                Console.WriteLine("Invalid JSON.");
            }

            return photodetails;
        }

        /// <summary>
        /// Gets the details of Photo Album details. when user Id is not entered 
        /// then it fetches all the records from end points.
        /// If user enters a specific 
        /// </summary>
        /// <param name="uid">user ID</param>
        /// <returns>Details of Phot album of single user</returns>
        public async Task<List<PhotoAlbumModel>> GetByUser(int uid = 0)
        {
            IEnumerable<PhotoModel> photos = null;
            IEnumerable<AlbumModel> albums = null;
            List<PhotoAlbumModel> photodetails = null;
            try
            {
                HttpResponseMessage res = await _httpClient.GetAsync("photos");
                if (res.IsSuccessStatusCode)
                {
                    photos = await res.Content.ReadFromJsonAsync<List<PhotoModel>>();
                }
                else
                {
                    string msg = "Reponse failed!";
                    Console.WriteLine(msg);
                    throw new Exception(msg);
                }

                res = await _httpClient.GetAsync("albums");
                if (res.IsSuccessStatusCode)
                {
                    albums = await res.Content.ReadFromJsonAsync<List<AlbumModel>>();
                }
                else
                {
                    string msg = "Reponse failed!";
                    Console.WriteLine(msg);
                    throw new Exception(msg);
                }

                // Get the data set usng two endpoints and combined them as a single set mapping
                // the albumID in both photos and albums
                var allDetails = from album in albums
                                 join photo in photos
                                               on album.ID equals photo.albumId
                                 select new
                                 {
                                     album.ID,
                                     album.Title,
                                     album.userId,
                                     photo.id,
                                     photo.title,
                                     photo.url,
                                     photo.thumbnailUrl
                                 };

                photodetails = allDetails.Where(user => user.userId == uid).
                    Select(m => new PhotoAlbumModel
                    {
                        AlbumId = m.ID,
                        AlbumTitle = m.Title,
                        UserId = m.userId,
                        PhotoId = m.id,
                        PhotoTitle = m.title,
                        Url = m.url,
                        ThumbnailUrl = m.thumbnailUrl
                    }).ToList();

            }
            catch (HttpRequestException) // Non success
            {
                Console.WriteLine("An error occurred.");
            }
            catch (NotSupportedException) // When content type is not valid
            {
                Console.WriteLine("The content type is not supported.");
            }
            catch (JsonException) // Invalid JSON
            {
                Console.WriteLine("Invalid JSON.");
            }

            return photodetails;
        }        
    }
    
}
