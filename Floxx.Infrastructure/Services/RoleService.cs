using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Floxx.Core.DTO.Identity;
using Floxx.Core.Entities;
using Floxx.Core.Helpers;
using Floxx.Core.Interfaces;
using Floxx.Shared.Constants.Permission;
using Floxx.Shared.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Floxx.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private IMapper mapper;

        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<Result<string>> DeleteAsync(string id)
        {
            var existingRole = await roleManager.FindByIdAsync(id);
            if (existingRole.Name != "Administrator" && existingRole.Name != "Basic")
            {
                //TODO Check if Any Users already uses this Role
                bool roleIsNotUsed = true;
                var allUsers = await userManager.Users.ToListAsync();
                foreach (var user in allUsers)
                {
                    if (await userManager.IsInRoleAsync(user, existingRole.Name))
                    {
                        roleIsNotUsed = false;
                    }
                }
                if (roleIsNotUsed)
                {
                    await roleManager.DeleteAsync(existingRole);
                    return Result<string>.Success($"Role {existingRole.Name} deleted.");
                }
                else
                {
                    return Result<string>.Fail($"Not allowed to delete {existingRole.Name} Role as it is being used.");
                }
            }
            else
            {
                return Result<string>.Fail($"Not allowed to delete {existingRole.Name} Role.");
            }
        }

        public async Task<Result<List<RoleResponse>>> GetAllAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();
            var rolesResponse = mapper.Map<List<RoleResponse>>(roles);
            return Result<List<RoleResponse>>.Success(rolesResponse);
        }

        public async Task<Result<PermissionResponse>> GetAllPermissionsAsync(string roleId)
        {
            var model = new PermissionResponse();
            var allPermissions = new List<RoleClaimsResponse>();

            #region GetPermissions

            allPermissions.GetPermissions(typeof(Permissions.Users), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Roles), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Workflows), roleId);

            #endregion GetPermissions

            var role = await roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                model.RoleId = role.Id;
                model.RoleName = role.Name;
                var claims = await roleManager.GetClaimsAsync(role);
                var allClaimValues = allPermissions.Select(a => a.Value).ToList();
                var roleClaimValues = claims.Select(a => a.Value).ToList();
                var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
                foreach (var permission in allPermissions)
                {
                    if (authorizedClaims.Any(a => a == permission.Value))
                    {
                        permission.Selected = true;
                    }
                }
            }
            model.RoleClaims = allPermissions;
            return Result<PermissionResponse>.Success(model);
        }

        public async Task<Result<RoleResponse>> GetByIdAsync(string id)
        {
            var roles = await roleManager.Roles.SingleOrDefaultAsync(x => x.Id == id);
            var rolesResponse = mapper.Map<RoleResponse>(roles);
            return Result<RoleResponse>.Success(rolesResponse);
        }

        public async Task<Result<string>> SaveAsync(RoleRequest request)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                var existingRole = await roleManager.FindByNameAsync(request.Name);
                if (existingRole != null) return Result<string>.Fail($"Similar Role already exists.");
                var response = await roleManager.CreateAsync(new IdentityRole(request.Name));
                return Result<string>.Success("Role Created");
            }
            else
            {
                var existingRole = await roleManager.FindByIdAsync(request.Id);
                if (existingRole.Name == "Administrator" || existingRole.Name == "Basic")
                {
                    return Result<string>.Fail($"Not allowed to modify {existingRole.Name} Role.");
                }
                existingRole.Name = request.Name;
                existingRole.NormalizedName = request.Name.ToUpper();
                await roleManager.UpdateAsync(existingRole);
                return Result<string>.Success("Role Updated.");
            }
        }

        public async Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(request.RoleId);
                if (role.Name == "Administrator")
                {
                    return Result<string>.Fail($"Not allowed to modify Permissions for this Role.");
                }
                var claims = await roleManager.GetClaimsAsync(role);
                foreach (var claim in claims)
                {
                    await roleManager.RemoveClaimAsync(role, claim);
                }
                var selectedClaims = request.RoleClaims.Where(a => a.Selected).ToList();
                foreach (var claim in selectedClaims)
                {
                    await roleManager.AddPermissionClaim(role, claim.Value);
                }
                return Result<string>.Success("Permission Updated.");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }

        public async Task<int> GetCountAsync()
        {
            var count = await roleManager.Roles.CountAsync();
            return count;
        }
    }
}
