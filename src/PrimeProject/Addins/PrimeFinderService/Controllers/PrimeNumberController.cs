using System;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeFinderService.Services;
using PrimeService.Data;

namespace PrimeFinderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrimeNumberController : ControllerBase
    {
        private readonly PrimeFinder _primeFinder;
        private readonly IServiceProvider _services;
        private readonly IDbContextFactory<PrimeServiceContext> _contextFactory;

        public PrimeNumberController(PrimeFinder primeFinder, IServiceProvider services, IDbContextFactory<PrimeServiceContext> contextFactory)
        {
            _primeFinder = primeFinder;
            _services = services;
            _contextFactory = contextFactory;
        }

        public IActionResult Index()
        {
            return Ok("This is my default action...");
        }

        [Route("NearestLeftPrime/{number}")]
        public IActionResult NearestLeftPrime(string number)
        {
            var response = "";
            var num = BigInteger.Parse(number);
            var (nearest, accuracy) = _primeFinder.NearestLeftPrime(num);
            response = $"{{nearest:{nearest}, accuracy:{accuracy}}}";
            return Ok(response);
        }
    }
}
