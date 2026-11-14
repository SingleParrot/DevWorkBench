using DevWorkBench;
using Microsoft.EntityFrameworkCore;

public class AuthRoleService
{
    private RoleContext _roleContext;

    public AuthRoleService(RoleContext context)
    {
        this._roleContext = context;
    }

    public async Task<Role?> GetRoleAsync (int id)
    {
        return await this._roleContext.Roles.FindAsync(id);
    }

    public async Task<List<Role>> GetAllRolesAsync ()
    {
        return await this._roleContext.Roles.ToListAsync();
    }
}