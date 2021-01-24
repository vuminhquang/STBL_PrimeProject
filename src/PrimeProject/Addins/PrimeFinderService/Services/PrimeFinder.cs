using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PrimeService.Data;
using PrimeService.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace PrimeFinderService.Services
{
    public class PrimeFinder
    {
        public const long MAX_NUM_TO_CALC_DIRECTLY = 1000000;
        private readonly IServiceProvider _services;

        public PrimeFinder(IServiceProvider services)
        {
            _services = services;
        }

        public (bool IsPrime, double Accuracy) IsPrime(BigInteger number, PrimeServiceContext context, int recurring = 25)
        {
            var summary = context.Summaries.FirstOrDefault();
            if (summary == null)
            {
                summary = new Summary {MaximumNumberReached = 3};
                context.Summaries.Add(summary);
                context.SaveChanges();
            }
            
            var maxValue = summary.MaximumNumberReached;
            // if (number < long.MaxValue)

            if (number <= maxValue)
            {
                var longNum = (long) number;
                var t = context.PrimeNumbers.FirstOrDefault(primeNumber => primeNumber.Number == longNum);
                if (t==null)
                {
                    return (false, 1);
                }
                // var ret = 
            }

            var isPrime = number.IsPrime(recurring, out var accuracy);
            return (isPrime, accuracy);
        }

        public (BigInteger NearestLeftPrime, double Accouracy) NearestLeftPrime(BigInteger number, PrimeServiceContext context)
        {
            var longNum = (long) number;
            var nearestLeftNumber = context.PrimeNumbers.Where(primeNumber => primeNumber.Number <= longNum).Max(num => num.Number);

            return (0, 1);
        }

        //Calc and put into DB for number in [from, to]
        public void SeedData(long to = long.MaxValue)
        {
            //if (to <= int.MaxValue) return;
            var intTo = (int)to;
            using var context = _services.GetService<IDbContextFactory<PrimeServiceContext>>().CreateDbContext();
            FindPrimeUsingSieveOfAtkins(context, intTo);
        }

        private static int FindPrimeUsingSieveOfAtkins(PrimeServiceContext context, int topCandidate = 1000000)
        {
            Console.WriteLine("Begin calc");
            var totalCount = 0;

            var isPrime = new BitArray(topCandidate + 1);

            var squareRoot = (int) Math.Sqrt(topCandidate);

            int xSquare = 1, xStepsize = 3;

            int ySquare = 1, yStepsize = 3;

            var computedVal = 0;

            for (var x = 1; x <= squareRoot; x++)
            {
                ySquare = 1;
                yStepsize = 3;
                for (int y = 1; y <= squareRoot; y++)
                {
                    computedVal = (xSquare << 2) + ySquare;

                    if ((computedVal <= topCandidate) && (computedVal % 12 == 1 || computedVal % 12 == 5))
                        isPrime[computedVal] = !isPrime[computedVal];

                    computedVal -= xSquare;
                    if ((computedVal <= topCandidate) && (computedVal % 12 == 7))
                        isPrime[computedVal] = !isPrime[computedVal];

                    if (x > y)
                    {
                        computedVal -= ySquare << 1;
                        if ((computedVal <= topCandidate) && (computedVal % 12 == 11))
                            isPrime[computedVal] = !isPrime[computedVal];
                    }

                    ySquare += yStepsize;
                    yStepsize += 2;
                }

                xSquare += xStepsize;
                xStepsize += 2;
            }

            for (var n = 5; n <= squareRoot; n++)
            {
                if (isPrime[n] != true) continue;
                var k = n * n;
                for (var z = k; z <= topCandidate; z += k)
                    isPrime[z] = false;
            }

            for (var i = 1; i < topCandidate; i++)
            {
                if (!isPrime[i]) continue;
                Console.WriteLine($"Prime number: {i}");
                totalCount++;
                //Update to DB
                var primeNumber = new PrimeNumber
                {
                    Number = i
                };
                context.PrimeNumbers.Add(primeNumber);
            }
            //Update current top number calculated to DB
            context.Summaries.First().MaximumNumberReached = topCandidate;
            context.SaveChanges();

            return (totalCount + 2); // 2 and 3 will be missed in Sieve Of Atkins
        }
    }
}
