using AutoMapper;
using Trasher.Domain.Common;
using Trasher.Domain.DTOs;

namespace Trasher.BLL.Mapping
{
    public class AutoMapperConfig<T, Tmodel>

    {
        public static IMapper Initialize()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, Tmodel>();
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}