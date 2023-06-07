using Trasher.Domain.Common;
using Trasher.Domain.DTOs;

namespace Trasher.BLL.Mapping
{
    public class DTOMapper<T, Tmodel>
where T : BaseEntityDTO
where Tmodel : BaseEntity
    {
        public static IEnumerable<Tmodel> Map(IEnumerable<T> sourceCollection)
        {
            var mapper = Initialize<Tmodel, T>.DTOInitializeMapper();
            return mapper.Map<IEnumerable<Tmodel>>(sourceCollection);
        }

        public static Tmodel Map(T source)
        {
            var mapper = Initialize<Tmodel, T>.DTOInitializeMapper();
            return mapper.Map<Tmodel>(source);
        }

    }
}