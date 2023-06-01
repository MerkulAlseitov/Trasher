using AutoMapper;
using Trasher.Domain.Common;
using Trasher.Domain.DTOs;
using Trasher.Domain.Users;

namespace Trasher.API.Mapping
{
    public class MappingProfile<T, TModel>
        where T : BaseEntity
        where TModel : BaseEntityDTO
    {
        public static void Main()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<OperatorDTO, Operator>();
            });

            IMapper mapper = config.CreateMapper();

            OperatorDTO source = new OperatorDTO {};
            Operator destination = mapper.Map<OperatorDTO, Operator>(source);
        }
    }
}
