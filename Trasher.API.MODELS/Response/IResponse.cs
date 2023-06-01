using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trasher.API.MODELS.Response
{
    public interface IResponse<T>
    {
        public T Data { get; set; }
        public string ErrorMassage { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
    }
}
