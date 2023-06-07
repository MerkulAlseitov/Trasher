using AutoMapper;
using Trasher.Domain.Common;
using Trasher.Domain.DTOs;

namespace Trasher.BLL.Mapping
{
    public class Mapper<Tmodel, T>
        where Tmodel : BaseEntityDTO
        where T : BaseEntity
    {
        public static IEnumerable<T> Map(IEnumerable<Tmodel> sourceCollection)
        {

            var mapper = AutoMapperConfig<Tmodel, T>.Initialize();
            return mapper.Map<IEnumerable<T>>(sourceCollection);
        }

        public static T Map(Tmodel source)
        {
            var mapper = AutoMapperConfig<Tmodel, T>.Initialize();
            return mapper.Map<T>(source);
        }
    }
}
