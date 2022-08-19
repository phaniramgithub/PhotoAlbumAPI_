using PhotoAlbumAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using PhotoAlbumAPI;

namespace FirstWebAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoAlbumController : ControllerBase    
    {
        public IPhotoAlbumService _photoAlbumService;

        public PhotoAlbumController(IPhotoAlbumService photoAlbumService)
        {
            _photoAlbumService = photoAlbumService;
        }

        /// <summary>
        /// Get all photo album details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<PhotoAlbumModel>> GetAll()
        {
            try
            {
                return await _photoAlbumService.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Get photo album details by User ID.
        /// </summary>
        /// <param name="id"> user id</param>
        /// <returns>All the photo album details related to a specific user</returns>
        [HttpGet("{id}")]
        public async Task<List<PhotoAlbumModel>> GetByUser(int id)
        {
            try
            {
                return await _photoAlbumService.GetByUser(id);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
