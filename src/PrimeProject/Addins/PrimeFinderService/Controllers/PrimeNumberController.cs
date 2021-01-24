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

        [Route("IsPrime/{number}")]
        public IActionResult IsPrime(string number)
        {
            var response = "";
            var num = BigInteger.Parse(number);
            using (var context = _contextFactory.CreateDbContext())
            {
                var (isPrime, accuracy) = _primeFinder.IsPrime(num, context);
                response = $"isPrime:{isPrime}, accuracy:{accuracy}";
            }

            return Ok(response);
        }
    }
}
