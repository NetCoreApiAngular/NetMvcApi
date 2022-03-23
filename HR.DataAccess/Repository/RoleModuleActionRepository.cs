

using HR.DataAccess.Entity;

namespace HR.DataAccess.Repository
{
    public interface IRoleModuleActionRepository : IBaseRepository<RoleModuleAction>
    {

    }
    public class RoleModuleActionRepository : BaseRepository<RoleModuleAction>, IRoleModuleActionRepository
    {
        public RoleModuleActionRepository(HrManagementContext context)
            : base(context)
        {
        }

    }
}
