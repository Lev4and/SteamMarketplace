using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface ICRUDRepository<T> where T : BaseEntity
    {
        bool Contains(T entity);

        T GetById(Guid id);

        void Add(T entity);

        void Update(T entity);

        void DeleteById(Guid id);
    }
}
