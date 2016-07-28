using TaskManager.DB;
using TaskManager.DBEntity.ORG;
using TaskManager.LogicEntity.Entities;
using TaskManager.LogicEntity.Entities.Org;
using TaskManager.Repository.Converter.Org;
using TaskManager.Repository.Interfaces.Org;

namespace TaskManager.Repository.Repositories.Base.Org
{
    internal abstract class BaseUserRepository : BaseRepository<User, T_ORG_USER>, IUserRepository
    {
        protected BaseUserRepository(ITaskManagerDb db)
            : base(db)
        {
        }

        protected override User FromT(T_ORG_USER t)
        {
            return t.FromT();
        }

        protected override T_ORG_USER ToT(User l)
        {
            return l.ToT();
        }

        public bool Exists(string userId)
        {
            return base.BaseQuery.Equal("OU_Id", userId).Equal(IsActive, true).QueryCount() > 0;
        }

        public bool Create(User user)
        {
            return base.Add(user.ToT());
        }

        public bool Update(User user)
        {
            return base.Update(user.ToT());
        }

        public bool Delete(string userId)
        {
            var sql = new Sql();
            sql.Where("OU_Id = @0", userId);
            return base.Db.Delete<T_ORG_USER>(sql) >= 0;
        }

        public User Get(string userId)
        {
            return base.BaseQuery.Equal("OU_Id", userId).Equal(IsActive, true).SingleOrDefault().FromT();
        }

        public PagedList<User> GetByCondition(int pageIndex, int pageSize)
        {
            return base.ConvertToPagedList(
                base.BaseQuery.Equal(IsActive, true)
                    .OrderBy("OU_Name")
                    .QueryByPage(pageIndex, pageSize));
        }
    }
}
