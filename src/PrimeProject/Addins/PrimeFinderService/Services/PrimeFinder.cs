using System;
using System.Collections;
using System.Numerics;

namespace PrimeFinderService.Services
{
    public class PrimeFinder
    {
        private static object @lock = new object();
        public const int MAX_NUM_TO_CALC_DIRECTLY = (int.MaxValue / 2);
        public static bool _ready = false;
        private readonly IServiceProvider _services;
        private static BitArray _isPrime;

        public static BitArray IsPrimeBitArray => _isPrime;
        public static bool Ready => _ready;

        public PrimeFinder(IServiceProvider services)
        {
            _services = services;
        }

        public bool IsPrime(int number)
        {
            if (number < 1)
            {
                return false;
            }

            return number <= 3 || IsPrimeBitArray[number];
        }

        private (bool IsPrime, double Accuracy) IsPrime(BigInteger number, int recurring = 25)
        {
            var isPrime = number.IsPrime(recurring, out var accuracy);
            return (isPrime, accuracy);
        }

    public (BigInteger NearestLeftPrime, double Accouracy) NearestLeftPrime(BigInteger number)
        {
            var maxSmallNum = MAX_NUM_TO_CALC_DIRECTLY;
            
            //Use cache
            if (number < maxSmallNum)
            {
                var intNum = (int) number;

                if (intNum < 1)
                {
                    return (0, 1);
                }

                if (intNum <=3)
                {
                    return (number, 1);
                }

                for (var i = intNum; i > 3; i--)
                {
                    if (IsPrime(i))
                    {
                        return (i, 1);
                    }
                }

                return (3, 1);
            }

            //Use Miller-Rabin test
            for (var i = number; i >= 3; i--)
            {
                var (isPrime, accuracy) = IsPrime(i);
                if (isPrime)
                {
                    return (i, accuracy);
                }
            }

            return (0, 1);
        }

        public static int CacheSmallPrime()
        {
            return CachePrimesUsingSieveOfAtkins();
        }

        private static int CachePrimesUsingSieveOfAtkins(int topCandidate = MAX_NUM_TO_CALC_DIRECTLY)
        {
            //Only do once
            lock (@lock)
            {
                if (_isPrime != null)
                {
                    return 0;
                }

                _isPrime = new BitArray(MAX_NUM_TO_CALC_DIRECTLY + 1);
            }

            Console.WriteLine($"Begin cache, calc to {MAX_NUM_TO_CALC_DIRECTLY}");
            var totalCount = 0;

            var squareRoot = (int) Math.Sqrt(topCandidate);

            int xSquare = 1, xStepsize = 3;

            for (var x = 1; x <= squareRoot; x++)
            {
                var ySquare = 1;
                var yStepsize = 3;
                for (int y = 1; y <= squareRoot; y++)
                {
                    var computedVal = (xSquare << 2) + ySquare;

                    if ((computedVal <= topCandidate) && (computedVal % 12 == 1 || computedVal % 12 == 5))
                        IsPrimeBitArray[computedVal] = !IsPrimeBitArray[computedVal];

                    computedVal -= xSquare;
                    if ((computedVal <= topCandidate) && (computedVal % 12 == 7))
                        IsPrimeBitArray[computedVal] = !IsPrimeBitArray[computedVal];

                    if (x > y)
                    {
                        computedVal -= ySquare << 1;
                        if ((computedVal <= topCandidate) && (computedVal % 12 == 11))
                            IsPrimeBitArray[computedVal] = !IsPrimeBitArray[computedVal];
                    }

                    ySquare += yStepsize;
                    yStepsize += 2;
                }

                xSquare += xStepsize;
                xStepsize += 2;
            }

            for (var n = 5; n <= squareRoot; n++)
            {
                if (IsPrimeBitArray[n] != true) continue;
                var k = n * n;
                for (var z = k; z <= topCandidate; z += k)
                    IsPrimeBitArray[z] = false;
            }

            for (var i = 1; i < topCandidate; i++)
            {
                if (!IsPrimeBitArray[i]) continue;
                totalCount++;
            }
            Console.WriteLine("Cache Small Primes done");
            _ready = true;
            return (totalCount + 2); // 2 and 3 will be missed in Sieve Of Atkins
        }
    }
}
