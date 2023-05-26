
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
        Task<Boolean> ExistsProduct(int productId, string size, int amount);
        Task<IEnumerable<StorageDto>>  GetStorageDtos();
    }
}