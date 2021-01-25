using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using C5;
using PrimeFinderService.Services;

namespace BruteForcePrimeFinder
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            int[] PRIMES_UNDER = {1000000, 10000000, 30000000, 60000000, 70000000, 80000000, 90000000, 100000000};
            Console.WriteLine(
                $"{"--------------------",-20}{"--------------------",-20}{"--------------------",20}{"--------------------",20}");
            Console.WriteLine($"{"Primes Under",-20}{"Total Primes",-20}{"Time Taken",20}{"Algorithm",20}");
            Console.WriteLine(
                $"{"--------------------",-20}{"--------------------",-20}{"--------------------",20}{"--------------------",20}");

            var primeDictionary = new TreeDictionary<long, object>();
            var topCandidates = int.MaxValue / 2;

            DateTime startTime;
            int totalPrimes;
            DateTime stopTime;
            TimeSpan duration;

            startTime = DateTime.Now;
            totalPrimes = FindPrimeUsingSieveOfAtkins(topCandidates);
            stopTime = DateTime.Now;
            duration = stopTime - startTime;
            string findPrimeUsingSieveOfAtkins2 = String.Format("{0,-20}{1,-20}{2,20}{3,20}", topCandidates,
                totalPrimes, duration, "Atkins");
            Console.WriteLine(findPrimeUsingSieveOfAtkins2);
            

            
            var primeFinder = new PrimeFinder(null);
            startTime = DateTime.Now;
            totalPrimes = PrimeFinder.CacheSmallPrime();
            stopTime = DateTime.Now;
            duration = stopTime - startTime;
            string findPrimeUsingSieveOfAtkins3 = String.Format("{0,-20}{1,-20}{2,20}{3,20}", topCandidates,
                totalPrimes, duration, "PrimeFinderLibs");
            Console.WriteLine(findPrimeUsingSieveOfAtkins3);

            startTime = DateTime.Now;
            totalPrimes = 0;
            for (int i = 0; i <= topCandidates; i++)
            {
                if (primeFinder.IsPrime(i))
                    totalPrimes++;
            }
            stopTime = DateTime.Now;
            duration = stopTime - startTime;
            string findPrimeUsingSieveOfAtkins4 = String.Format("{0,-20}{1,-20}{2,20}{3,20}", topCandidates,
                totalPrimes, duration, "PrimeFinderLibsx2");
            Console.WriteLine(findPrimeUsingSieveOfAtkins4);
            return;
            
            startTime = DateTime.Now;
            totalPrimes = FindPrimeUsingBruteForce(PRIMES_UNDER[2], primeDictionary);
            stopTime = DateTime.Now;
            duration = stopTime - startTime;

            string findPrimeUsingBruteForce = $"{PRIMES_UNDER[2],-20}{totalPrimes,-20}{duration,20}{"Brute Force",20}";
            Console.WriteLine(findPrimeUsingBruteForce);

            startTime = DateTime.Now;
            totalPrimes = FindPrimeUsingBruteForce(PRIMES_UNDER[2], primeDictionary);
            stopTime = DateTime.Now;
            duration = stopTime - startTime;
            findPrimeUsingBruteForce =
                $"{PRIMES_UNDER[2],-20}{totalPrimes,-20}{duration,20}{"Brute Force With Cache",20}";
            Console.WriteLine(findPrimeUsingBruteForce);

            startTime = DateTime.Now;
            totalPrimes = FindPrimeUsingSieveOfAtkins(PRIMES_UNDER[2]);
            stopTime = DateTime.Now;
            duration = stopTime - startTime;
            string findPrimeUsingSieveOfAtkins = String.Format("{0,-20}{1,-20}{2,20}{3,20}", PRIMES_UNDER[2],
                totalPrimes, duration, "Atkins");
            Console.WriteLine(findPrimeUsingSieveOfAtkins);

            startTime = DateTime.Now;
            totalPrimes = await FindPrimeUsingRobinMillerTest((ulong) PRIMES_UNDER[2]);
            stopTime = DateTime.Now;
            duration = stopTime - startTime;
            string findPrimeUsingRobinMillerTest = String.Format("{0,-20}{1,-20}{2,20}{3,20}", PRIMES_UNDER[2],
                totalPrimes, duration, "RobinMiller");
            Console.WriteLine(findPrimeUsingRobinMillerTest);

            startTime = DateTime.Now;
            totalPrimes = await FindPrimeUsingRobinMillerTest2((ulong) PRIMES_UNDER[2]);
            stopTime = DateTime.Now;
            duration = stopTime - startTime;
            string findPrimeUsingRobinMillerTest2 = String.Format("{0,-20}{1,-20}{2,20}{3,20}", PRIMES_UNDER[2],
                totalPrimes, duration, "RobinMiller2");
            Console.WriteLine(findPrimeUsingRobinMillerTest2);
        }

        private static async Task<int> FindPrimeUsingRobinMillerTest(ulong topCandidate)
        {
            var totalCount = 1;
            for (ulong i = 3; i < topCandidate; i += 2)
            {
                var isPrime = PrimeTests.IsPrime(i);
                if (isPrime)
                {
                    totalCount++;
                }
            }

            return totalCount;
        }

        private static async Task<int> FindPrimeUsingRobinMillerTest2(ulong topCandidate)
        {
            var totalCount = 1;
            BigInteger top = new BigInteger(topCandidate);
            for (BigInteger i = 3; i < top; i += 2)
            {
                var isPrime = i.IsPrime(25, out _);
                if (isPrime)
                {
                    totalCount++;
                }
            }

            return totalCount;
        }

        private static int FindPrimeUsingBruteForce(long topCandidate, TreeDictionary<long, object> primeDictionary)
        {
            int totalCount = 1;
            for (long i = 3; i < topCandidate; i += 2)
            {
                var isPrime = IsPrime(i, primeDictionary);
                if (isPrime)
                {
                    totalCount++;
                }
            }

            return totalCount;
        }

        private static bool IsPrime(long i, TreeDictionary<long, object> primeDictionary)
        {
            // var number = new BigInteger(i);
            if (primeDictionary.Find(ref i, out _))
            {
                return true;
            }

            var isPrime = true;

            for (var j = 3; j * j <= i; j += 2)
            {
                if ((i % j) != 0) continue;
                isPrime = false;
                break;
            }

            if (isPrime)
            {
                primeDictionary.Add(i, null);
            }

            return isPrime;
        }

        private static int FindPrimeUsingSieveOfAtkins(int topCandidate = 1000000)
        {
            int totalCount = 0;

            BitArray isPrime = new BitArray(topCandidate + 1);

            int squareRoot = (int) Math.Sqrt(topCandidate);

            int xSquare = 1, xStepsize = 3;

            int ySquare = 1, yStepsize = 3;

            int computedVal = 0;

            for (int x = 1; x <= squareRoot; x++)
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

            for (int n = 5; n <= squareRoot; n++)
            {
                if (isPrime[n] == true)
                {
                    int k = n * n;
                    for (int z = k; z <= topCandidate; z += k)
                        isPrime[z] = false;
                }
            }

            for (int i = 1; i < topCandidate; i++)
            {
                if (isPrime[i]) totalCount++;
            }

            return (totalCount + 2); // 2 and 3 will be missed in Sieve Of Atkins
        }

    }
}
