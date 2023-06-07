using Trasher.Domain.Common;
using Trasher.Domain.DTOs;

namespace Trasher.BLL.Mapping
{
    public class DTOMapper<T, Tmodel>
        where T : BaseEntity
        where Tmodel : BaseEntityDTO
    {
        public static IEnumerable<Tmodel> Map(IEnumerable<T> sourceCollection)
        {

            var mapper = DTOInitialize<T, Tmodel>.InitializeMapper();
            return mapper.Map<IEnumerable<Tmodel>>(sourceCollection);
        }

        public static T Map(Tmodel source)
        {
            var mapper = DTOInitialize<T, Tmodel>.InitializeMapper();
            return mapper.Map<T>(source);
        }
    }
}