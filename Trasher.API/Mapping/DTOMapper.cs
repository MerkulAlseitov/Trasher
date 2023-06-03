using Trasher.Domain.Common;
using Trasher.Domain.DTOs;

namespace Trasher.API.Mapping
{
    public class DTOMapper<Tmodel, T>
where Tmodel : BaseEntityDTO
where T : BaseEntity
    {
        public static IEnumerable<T> Map(IEnumerable<Tmodel> sourceCollection)
        {
            var mapper = Initialize<T, Tmodel>.InitializeMapper();
            return mapper.Map<IEnumerable<T>>(sourceCollection);
        }

        public static T Map(Tmodel source)
        {
            var mapper = Initialize<T, Tmodel>.InitializeMapper();
            return mapper.Map<T>(source);
        }

    }
}