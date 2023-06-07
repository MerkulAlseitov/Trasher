using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.Domain.Common;
using Trasher.Domain.DTOs;

namespace Trasher.BLL.Mapping
{
    public class DTOUserInitialize<Tmodel, T>
         where Tmodel : AppUserDTO
         where T : AppUser
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
