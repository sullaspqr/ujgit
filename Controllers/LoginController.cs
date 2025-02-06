using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektNeveBackend.DTOs;
using ProjektNeveBackend.Models;

namespace ProjektNeveBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("GetSalt/{felhasznaloNev}")]
        public async Task<IActionResult> GetSalt(string felhasznaloNev)
        {
            string password = "a";
            string SALT=Program.GenerateSalt();
            string tHASH=Program.CreateSHA256(password+SALT);
            string HASH=Program.CreateSHA256(tHASH);
            using (var cx = new TurbodriveContext())
            {
                try
                {
                    User response = await cx.Users.FirstOrDefaultAsync(f => f.FelhasznaloNev == felhasznaloNev);
                    return response == null ? BadRequest("Hiba") : Ok(response.Salt);
                }
                catch
                (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            using (var cx = new TurbodriveContext())
            {
                try
                {
                    string Hash = Program.CreateSHA256(loginDTO.TmpHash);
                    User loggedUser = await cx.Users.FirstOrDefaultAsync(f => f.FelhasznaloNev == loginDTO.LoginName && f.Hash == Hash);
                    if (loggedUser != null && loggedUser.Aktiv==1)
                    {
                        string token = Guid.NewGuid().ToString();
                        lock (Program.LoggedInUsers)
                        {
                            Program.LoggedInUsers.Add(token, loggedUser);
                        }
                        return Ok(new LoggedUser { Name = loggedUser.TeljesNev, Email = loggedUser.Email, Permission = loggedUser.Jogosultsag, ProfilePicturePath = loggedUser.FenykepUtvonal, Token = token });
                    }
                    else
                    {
                        return BadRequest("Hibás név vagy jelszó/inaktív felhasználó!");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new LoggedUser { Permission = -1, Name = ex.Message, ProfilePicturePath = "", Email = "" });
                }
            }
        }
    }
}
