using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using Mihcelle.Hwavmvid.Data;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Shared.Constants;

namespace Mihcelle.Hwavmvid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Installationauthenticationcontroller : ControllerBase
    {

        public IHostApplicationLifetime ihostapplicationlifetime { get; set; }
        public IWebHostEnvironment iwebhostenvironment { get; set; }
        public IConfiguration configuration { get; set; }
        public UserManager<Applicationuser> usermanager { get; set; }
        public SignInManager<Applicationuser> signinmanager { get; set; }
        public RoleManager<IdentityRole> rolemanager { get; set; }
        public Applicationdbcontext context { get; set; }

        private const string frontpagename = "Mihcellehwavmvid corporatcc";

        public Installationauthenticationcontroller(IHostApplicationLifetime ihostapplicationlifetime, IWebHostEnvironment environment, IConfiguration configuration, UserManager<Applicationuser> usermanager, SignInManager<Applicationuser> signinmanager, RoleManager<IdentityRole> rolemanager, Applicationdbcontext context)
        {

            this.ihostapplicationlifetime = ihostapplicationlifetime;
            this.iwebhostenvironment = environment;
            this.configuration = configuration;
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
            this.rolemanager = rolemanager;
            this.context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task Post([FromBody] Installationmodel model)
        {

            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                await this.signinmanager.SignOutAsync();
            }

            var applicationuser = new Applicationuser();
            applicationuser.UserName = model.Hostusername;
            applicationuser.Email = model.Hostusername + "@mihcelle.hwavmvid.com";
            applicationuser.PasswordHash = model.Hostpassword;
            applicationuser.EmailConfirmed = true;
            applicationuser.TwoFactorEnabled = false;
            applicationuser.LockoutEnabled = true;

            var createuserresult = await this.usermanager.CreateAsync(applicationuser, model.Hostpassword);
            if (createuserresult.Succeeded)
            {

                if (!await this.rolemanager.RoleExistsAsync(Shared.Constants.Authentication.Hostrole))
                {
                    await this.rolemanager.CreateAsync(new IdentityRole(Shared.Constants.Authentication.Hostrole));
                }
                if (!await this.rolemanager.RoleExistsAsync(Shared.Constants.Authentication.Administratorrole))
                {
                    await this.rolemanager.CreateAsync(new IdentityRole(Shared.Constants.Authentication.Administratorrole));
                }
                if (!await this.rolemanager.RoleExistsAsync(Shared.Constants.Authentication.Userrole))
                {
                    await this.rolemanager.CreateAsync(new IdentityRole(Shared.Constants.Authentication.Userrole));
                }
                if (!await this.rolemanager.RoleExistsAsync(Shared.Constants.Authentication.Anonymousrole))
                {
                    await this.rolemanager.CreateAsync(new IdentityRole(Shared.Constants.Authentication.Anonymousrole));
                }

                var addtoroleresult = await usermanager.AddToRoleAsync(applicationuser, Shared.Constants.Authentication.Hostrole);
                if (!addtoroleresult.Succeeded)
                {
                    throw new HubException("Failed to add user to role..");
                }
            }

        }

    }
}
