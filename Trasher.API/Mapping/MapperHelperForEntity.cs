using AutoMapper;
using Trasher.Domain.Common;
using Trasher.Domain.DTOs;

namespace Trasher.API.Mapping
{
    public class MapperHelperForEntity<Tmodel, T>
    where Tmodel : BaseEntityDTO
    where T : BaseEntity
    {
        public static IEnumerable<T> Map(IEnumerable<Tmodel> sourceCollection)
        {

            var mapper = InitializeMapper();
            return mapper.Map<IEnumerable<T>>(sourceCollection);
        }

        public static T Map(Tmodel source)
        {

            var mapper = InitializeMapper();
            return mapper.Map<T>(source);
        }

        private static IMapper InitializeMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, Tmodel>();
            });
            return mapperConfiguration.CreateMapper();
        }
    }

}
