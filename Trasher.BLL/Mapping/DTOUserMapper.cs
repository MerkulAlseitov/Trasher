using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.Domain.Common;
using Trasher.Domain.DTOs;

namespace Trasher.BLL.Mapping
{
    public class DTOUserMapper<Tmodel, T>
    where Tmodel : AppUserDTO
    where T : AppUser
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
