using AutoMapper;
using Trasher.Domain.Common;
using Trasher.Domain.DTOs;

namespace Trasher.API.Mapping
{
    public static class Initialize<T, Tmodel>
    where T : BaseEntity
    where Tmodel : BaseEntityDTO
    {
        public static IMapper InitializeMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tmodel, T>();
            });
            return mapperConfiguration.CreateMapper();
        }
    }
}
