using System;
using System.Data.Linq;
using System.Linq;
using Application.Domain;
using Application.Domain.Repository;
using Application.Domain.UnitOfWork;
using Infrastructure.Database.LinqMapper;

namespace Infrastructure.Database.Repositories
{
    public abstract class BaseRepository<Tm, TDb> : IRepository<Tm>
        where Tm : BaseDomainEntity 
        where TDb : class, IDbEntity, new()
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public bool SaveOnCommit(Tm domainEntity)
        {
            if (domainEntity == null)
                throw new ArgumentNullException("domainEntity");

            TDb dbEntity;

            if(domainEntity.IsNew())
            {
                dbEntity = new TDb();
            }
            else
            {
                dbEntity = FindDbEntity(domainEntity);
                if(dbEntity == null)
                    return false;
            }

            MapEntry(dbEntity, domainEntity);

            if(domainEntity.IsNew())
            {
                DbTable.InsertOnSubmit(dbEntity);
            }

            domainEntity.ID = dbEntity.ID;
            return true;
        }

        public bool DeleteOnCommit(Tm domainEntity)
        {
            var dbEntity = FindDbEntity(domainEntity);

            if (dbEntity == null)
                return false;

            DbTable.DeleteOnSubmit(dbEntity);
            return true;
        }

        protected virtual TDb FindDbEntity(Tm domainEntity)
        {
            return DbTable.Where(x => x.ID.Equals(domainEntity.ID)).SingleOrDefault();
        }

        protected abstract Table<TDb> DbTable { get; }
        protected abstract void MapEntry(TDb dbEntity, Tm domainEntity);
    }
}
