using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace KBS.Cities.API
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult([ActionResultObjectValue] object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }

    public class NotImplementedObjectResult : ObjectResult
    {
        public NotImplementedObjectResult([ActionResultObjectValue] object value) : base(value)
        {
            StatusCode = StatusCodes.Status501NotImplemented;
        }
    }
}
