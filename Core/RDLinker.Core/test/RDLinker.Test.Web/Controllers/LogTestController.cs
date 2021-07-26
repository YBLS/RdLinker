using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDLinker.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDLinker.Test.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogTestController : ControllerBase
    {
        public string GetHello(string msg)
        {
            LogHelper.Info($"info: {msg}");
            return "Hello test log";
        }
    }
}
