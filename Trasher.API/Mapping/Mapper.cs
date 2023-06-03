using AutoMapper;
using Trasher.Domain.Common;
using Trasher.Domain.DTOs;

namespace Trasher.API.Mapping
{
    public class Mapper<T, Tmodel>
    where T : BaseEntity
    where Tmodel : BaseEntityDTO
    {
        public static IEnumerable<Tmodel> Map(IEnumerable<T> sourceCollection)
        {
            var mapper = Initialize<T, Tmodel>.InitializeMapper();
            return mapper.Map<IEnumerable<Tmodel>>(sourceCollection);
        }

        public static Tmodel Map(T source)
        {
            var mapper = Initialize<T, Tmodel>.InitializeMapper();
            return mapper.Map<Tmodel>(source);
        }
    }

}
