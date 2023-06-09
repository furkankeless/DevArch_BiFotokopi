
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IStorageRepository : IEntityRepository<Storage>
    {
        Task<Boolean> StorageReadyControll(int productId, bool status);
        Task<IEnumerable<StorageDto>>  GetStorageDtos();
        Task<Boolean> AmountControll(int productId, int amount);
    }
}