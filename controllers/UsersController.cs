using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using DF.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.Encodings.Web;


namespace DF.Controller
{
    public class UsersController : ControllerBase
    {

        [HttpGet("/getallusers/{type}")]
        public async Task<IActionResult> GetAllUsers(string type)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://demo6957850.mockable.io");
                    var response = await client.GetAsync($"/people");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    List<User> rawUsers = JsonConvert.DeserializeObject<User[]>(stringResult).ToList();
                    List<User> sortedUsers = rawUsers;
                    if(type == "first-name")
                    {
                        sortedUsers = rawUsers.OrderBy(o => o.FirstName).ToList();
                    }
                    else if(type == "score")
                    {
                        sortedUsers = rawUsers.OrderBy(o => o.Score).ToList();
                    }

                    return Ok(sortedUsers);
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting users: {httpRequestException.Message}");
                }
            }
        }
    }
}