using AutoMapper;
using Trasher.Domain.Common;
using Trasher.Domain.DTOs;

namespace Trasher.BLL.Mapping
{
    public static class DTOInitialize<Tmodel, T>
   where Tmodel : BaseEntityDTO
   where T : BaseEntity
    {
        public static IMapper InitializeMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, Tmodel>();
            });
            return mapperConfiguration.CreateMapper();
        }
    }
}
