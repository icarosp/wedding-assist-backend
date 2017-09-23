using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingAssist.Api.Models
{
    public class Result
    {
        public object Data { get; private set; }
        public bool IsSuccess { get; private set; }
        public List<string> Errors { get; private set; }

        public Result(object data, params string[] errors)
        {
            Data = data;
            Errors = new List<string>();
            Errors.AddRange(errors);

            if (Errors.Count == 0)
                IsSuccess = true;
        }
    }
}
