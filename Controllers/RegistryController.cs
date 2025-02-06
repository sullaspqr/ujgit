using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektNeveBackend.Models;
using System.Configuration;

namespace ProjektNeveBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistryController : ControllerBase
    {
        [HttpPost]

        public async Task<IActionResult> Registry(User user)
        {
            using (var cx=new TurbodriveContext())
            {
                try
                {
                    if (cx.Users.FirstOrDefault(f=>f.FelhasznaloNev==user.FelhasznaloNev) != null) 
                    {
                        return Ok("Már létezik ilyen felhasználónév!");
                    }
                    if (cx.Users.FirstOrDefault(f => f.Email == user.Email) != null)
                    {
                        return Ok("Ezzel az e-mail címmel már regisztráltak!");
                    }
                    user.Jogosultsag = 1;
                    user.Aktiv = 0;
                    user.Hash = Program.CreateSHA256(user.Hash);
                    await cx.Users.AddAsync(user);
                    await cx.SaveChangesAsync();

                    Program.SendEmail(user.Email, "Regisztráció", $"http://localhost:5000/api/Registry?felhasznaloNev={user.FelhasznaloNev}&email={user.Email}");
                    
                    return Ok("Sikeres regisztráció. Fejezze be a regisztrációját az e-mail címére küldött link segítségével!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet]

        public async Task<IActionResult> EndOfTheRegistry(string felhasznaloNev,string email)
        {
            using (var cx=new TurbodriveContext())
            {
                User user=await cx.Users.FirstOrDefaultAsync(f=>f.FelhasznaloNev==felhasznaloNev&&f.Email==email);
                if (user == null)
                {
                    return Ok("Sikertelen a regisztráció befejezése!");
                }
                else
                {
                    user.Aktiv = 1;
                    cx.Users.Update(user);
                    await cx.SaveChangesAsync();
                    return Ok("A regisztráció befejezése sikeresen megtörtént.");
                }
            }
            return null;
        }

    }
}
