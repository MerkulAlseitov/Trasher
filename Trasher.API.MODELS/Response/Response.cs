using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Trasher.API.MODELS.Response
{
    public class Response<T> : IResponse<T>
    {
        public T Data { get; set; }
        public string ErrorMassage { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }

        public Response(int statusCode, string errorMassage, bool issuccess, T data) 
        {
            StatusCode = statusCode;
            ErrorMassage = errorMassage;
            IsSuccess = issuccess;
            Data = data;
        }


    public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
