using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjektNeveBackend.Models;
using ProjektNeveBackend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ProjektNeveBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("UserEmailName/{token}")]

        public async Task<IActionResult> GetUserEmailName(string token)
        {
            using(var cx = new TurbodriveContext())
            {
                try
                {
                    if (Program.LoggedInUsers.ContainsKey(token) && Program.LoggedInUsers[token].Jogosultsag==9)
                    {
                        return Ok(await cx.Users.Select(f => (new UserEmailNameDTO { Email = f.Email, TeljesNev = f.TeljesNev })).ToListAsync());
                    }
                    else {
                        return BadRequest("Nincs jogod hozzá!");
                    }
                }
                catch (Exception ex)
                {
                    //return BadRequest(ex.Message);
                    return BadRequest(ex.InnerException?.Message);
                }
            }
        }

        [HttpGet("{token}")]

        public async Task<IActionResult> Get(string token)
        {
            using (var cx = new TurbodriveContext())
            {
                try
                {
                    if (Program.LoggedInUsers.ContainsKey(token) && Program.LoggedInUsers[token].Jogosultsag == 9)
                    {
                        return Ok(await cx.Users.ToListAsync());
                    }
                    else
                    {
                        return BadRequest("Nincs jogod hozzá!");
                    }
                }
                catch (Exception ex)
                {
                    //return BadRequest(ex.Message);
                    return BadRequest(ex.InnerException?.Message);
                }
            }
        }

        [HttpGet("{token},{id}")]

        public async Task<IActionResult> Get(string token,int id)
        {
            using (var cx = new TurbodriveContext())
            {
                try
                {
                    if (Program.LoggedInUsers.ContainsKey(token) && Program.LoggedInUsers[token].Jogosultsag == 9)
                    {
                        return Ok(await cx.Users.FirstOrDefaultAsync(f=>f.Id==id));
                    }
                    else
                    {
                        return BadRequest("Nincs jogod hozzá!");
                    }
                }
                catch (Exception ex)
                {
                    //return BadRequest(ex.Message);
                    return BadRequest(ex.InnerException?.Message);
                }
            }
        }

        [HttpPost("{token}")]

        public async Task<IActionResult> Post(string token,User user)
        {
            using (var cx = new TurbodriveContext())
            {
                try
                {
                    if (Program.LoggedInUsers.ContainsKey(token) && Program.LoggedInUsers[token].Jogosultsag == 9)
                    {
                        await cx.Users.AddAsync(user);
                        await cx.SaveChangesAsync();
                        return Ok("Új felhasználó felvéve.");
                    }
                    else
                    {
                        return BadRequest("Nincs jogod hozzá!");
                    }
                }
                catch (Exception ex)
                {
                    //return BadRequest(ex.Message);
                    return BadRequest(ex.InnerException?.Message);
                }
            }
        }

        [HttpPut("{token}")]

        public IActionResult Put(string token, User user)
        {
            using (var cx = new TurbodriveContext())
            {
                try
                {
                    if (Program.LoggedInUsers.ContainsKey(token) && Program.LoggedInUsers[token].Jogosultsag == 9)
                    {
                        cx.Users.Update(user);
                        cx.SaveChanges();
                        return Ok("A felhasználó adatai módosítva.");
                    }
                    else
                    {
                        return BadRequest("Nincs jogod hozzá!");
                    }
                }
                catch (Exception ex)
                {
                    //return BadRequest(ex.Message);
                    return BadRequest(ex.InnerException?.Message);
                }
            }
        }

        [HttpDelete("{token},{id}")]

        public IActionResult Delete(string token, int id)
        {
            using (var cx = new TurbodriveContext())
            {
                try
                {
                    if (Program.LoggedInUsers.ContainsKey(token) && Program.LoggedInUsers[token].Jogosultsag == 9)
                    {
                        cx.Users.Remove(new User { Id=id});
                        cx.SaveChanges();
                        return Ok("A felhasználó adatai törölve.");
                    }
                    else
                    {
                        return BadRequest("Nincs jogod hozzá!");
                    }
                }
                catch (Exception ex)
                {
                    //return BadRequest(ex.Message);
                    return BadRequest(ex.InnerException?.Message);
                }
            }
        }
    }
}
