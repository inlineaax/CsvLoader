using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CsvLoader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvLoaderController : ControllerBase
    {
        private readonly ICsvLoaderService _csvLoaderService;

        public CsvLoaderController(ICsvLoaderService csvLoaderService)
        {
            _csvLoaderService = csvLoaderService;
        }

        /// <summary>
        /// Upload CSV Files and saves in data base
        /// </summary>
        /// <remarks>
        /// The CSV file must contain a name, city, country and favorite sport following the example: \
        /// \
        /// name, city, country, favorite_sport \
        /// John Doe,New York, USA, Basketball \
        /// Jane Smith, London, United Kingdom, Football \
        /// Mike Johnson,Paris,France,Tennis \
        /// Karen Lee,Tokyo,Japan,Swimming \
        /// Tom Brown,Sydney,Australia,Racing \
        /// Emma Wilson,Berlin,Germany,Basketball \
        /// \
        /// If there is any repeated data, it will not be saved in the database (a message will be returned with the corresponding duplicated users)
        /// </remarks>
        [HttpPost("Files")]
        public IActionResult UploadCsv(IFormFile file)
        {
            try
            {
                var (uniqueUsers, duplicateUsers) = _csvLoaderService.ReadCsvFile(file.OpenReadStream());
                if (uniqueUsers.Count > 0)
                {
                    _csvLoaderService.SaveUsers(uniqueUsers);
                }
                if (duplicateUsers.Count > 0)
                {
                    return Ok(new
                    {
                        message = "Success, some users were not added as they already exist in the database",
                        duplicateUsers
                    });
                }
                else
                {
                    return Ok("Success, all users were added");
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "Error processing CSV upload" });
            }
        }

        /// <summary>
        /// Search the uploaded CSV, filtering by a query parameter 
        /// </summary>
        /// <remarks>
        /// The search will be done using case insensitive, bringing all results that contain the sent parameter \
        /// If the search does not find results, it will bring up the message "Users not found"</remarks>
        [HttpGet("Users")]
        public IActionResult SearchUsers([FromQuery] string q)
        {
            var results = _csvLoaderService.SearchUsers(q);

            if (results.Count == 0)
            {
                return NotFound("Users not found");
            }

            return Ok(results);
        }
    }
}
