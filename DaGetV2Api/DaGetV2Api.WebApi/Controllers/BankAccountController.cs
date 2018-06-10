using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaGetV2Api.WebApi.Controllers
{
    public class BankAccountController : ApiController
    {
        [Authorize]
        public IHttpActionResult Create()
        {
            return Ok("hello world");
        }
    }
}
