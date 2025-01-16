namespace CinemaTix.Interfaces
{
    public interface IUserRoleService
    {
        Task<bool> IsUserAdmin(Guid userId);
    }
}
