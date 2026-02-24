using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Authentication.Authorization;
using WebAPI.Authentication.Domain.Authorization;
using WebAPI.Authentication.Domain.Enum;
using WebAPI.Authentication.Domain.Identity;
using WebAPI.Authentication.Infrastructure.Persistence;
using WebAPI.Authentication.ViewModel;

namespace WebAPI.Authentication.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _db;

        private readonly IConfiguration _config;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;

        public AuthController(
         UserManager<ApplicationUser> userManager,
         RoleManager<ApplicationRole> roleManager,
         IConfiguration config,
         ApplicationDbContext db,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _claimsFactory = claimsFactory;
            _db = db;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Username,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);


            return Ok("User registered successfully");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var valid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!valid)
                return Unauthorized("Invalid credentials");

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!)
    };

            // ✅ Permissions already added by PermissionClaimsFactory
            // Just read them
            var principal = await HttpContext.RequestServices
                .GetRequiredService<IUserClaimsPrincipalFactory<ApplicationUser>>()
                .CreateAsync(user);

            claims.AddRange(principal.Claims.Where(c => c.Type == "permission"));

            var token = CreateToken(claims);

            return Ok(new
            {
                access_token = token
            });
        }


        private string CreateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Role name is required");

            var exists = await _roleManager.RoleExistsAsync(roleName);
            if (exists)
                return BadRequest("Role already exists");

            var role = new ApplicationRole
            {
                Name = roleName
            };

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok($"Role '{roleName}' created successfully");
        }
        [HttpPost("users/{userId}/roles/{roleName}")]
        public async Task<IActionResult> AssignRoleToUser(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return NotFound("User not found");

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
                return NotFound("Role not found");

            var alreadyInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (alreadyInRole)
                return BadRequest("User already has this role");

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok($"Role '{roleName}' assigned to user");
        }

        [HttpDelete("users/{userId}/roles/{roleName}")]
        public async Task<IActionResult> DeleteRoleFromUser(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return NotFound("User not found");

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Role removed from user");
        }

        [HttpGet("roles")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles
                .Select(r => new
                {
                    r.Id,
                    r.Name
                })
                .ToList();

            return Ok(roles);
        }

        [HttpGet("users/{userId}/roles")]
        public async Task<IActionResult> GetUserRoles(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return NotFound("User not found");

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }


        [HttpPost(nameof(CreatePermission))]
        public async Task<IActionResult> CreatePermission(CreatePermissionViewModel dto)
        {
            var exists = await _db.Permissions.AnyAsync(p =>
                p.Resource == dto.Resource &&
                p.Action == dto.Action);

            if (exists)
                return BadRequest("Permission already exists");

            var permission = new Permission
            {
                Resource = dto.Resource,
                Action = dto.Action
            };

            _db.Permissions.Add(permission);
            await _db.SaveChangesAsync();

            return Ok(permission);
        }
        [HttpGet(nameof(GetPermissions))]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _db.Permissions
                .Select(p => new
                {
                    p.Id,
                    p.Resource,
                    Action = p.Action.ToString()
                })
                .ToListAsync();

            return Ok(permissions);
        }
        [HttpPost("roles/{roleId}/permissions/{permissionId}")]
        public async Task<IActionResult> AssignPermissionToRole(int roleId, int permissionId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return NotFound("Role not found");

            var permission = await _db.Permissions.FindAsync(permissionId);
            if (permission == null)
                return NotFound("Permission not found");

            var exists = await _db.RolePermissions.AnyAsync(rp =>
                rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (exists)
                return BadRequest("Permission already assigned to role");

            _db.RolePermissions.Add(new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            });

            await _db.SaveChangesAsync();

            return Ok("Permission assigned to role");
        }
        [HttpDelete("roles/{roleId}/permissions/{permissionId}")]
        public async Task<IActionResult> RemovePermissionFromRole(int roleId, int permissionId)
        {
            var rp = await _db.RolePermissions
                .FirstOrDefaultAsync(x => x.RoleId == roleId && x.PermissionId == permissionId);

            if (rp == null)
                return NotFound("Permission not assigned to role");

            _db.RolePermissions.Remove(rp);
            await _db.SaveChangesAsync();

            return Ok("Permission removed from role");
        }
        [HttpGet("roles/{roleId}")]
        public async Task<IActionResult> GetRolePermissions(int roleId)
        {
            var permissions = await _db.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => new
                {
                    rp.Permission.Id,
                    rp.Permission.Resource,
                    Action = rp.Permission.Action.ToString()
                })
                .ToListAsync();

            return Ok(permissions);
        }




        [HasPermission(PermissionAction.View)]
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return Ok();
        }


        [HasPermission(PermissionAction.Delete)]
        [HttpGet()]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}