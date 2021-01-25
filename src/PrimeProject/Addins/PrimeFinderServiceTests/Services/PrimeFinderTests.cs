using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeFinderService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PrimeFinderService.Services.Tests
{
    [TestClass()]
    public class PrimeFinderTests
    {
        [TestMethod()]
        public void NearestLeftPrimeTest()
        {
            PrimeFinder.CacheSmallPrime();
            var primeFinder = new PrimeFinder(null);
            //1
            var num = new BigInteger(1);
            var (nearest, accuracy) = primeFinder.NearestLeftPrime(num);
            Assert.AreEqual(1, nearest);
            Assert.AreEqual(1, accuracy);

            //2
            num = new BigInteger(2);
            (nearest, accuracy) = primeFinder.NearestLeftPrime(num);
            Assert.AreEqual(2, nearest);
            Assert.AreEqual(1, accuracy);

            //3
            num = new BigInteger(3);
            (nearest, accuracy) = primeFinder.NearestLeftPrime(num);
            Assert.AreEqual(3, nearest);
            Assert.AreEqual(1, accuracy);

            //4
            num = new BigInteger(4);
            (nearest, accuracy) = primeFinder.NearestLeftPrime(num);
            Assert.AreEqual(3, nearest);
            Assert.AreEqual(1, accuracy);

            //5
            num = new BigInteger(5);
            (nearest, accuracy) = primeFinder.NearestLeftPrime(num);
            Assert.AreEqual(5, nearest);
            Assert.AreEqual(1, accuracy);

            //int.MaxValue/2
            num = new BigInteger(1073741822);
            (nearest, accuracy) = primeFinder.NearestLeftPrime(num);
            Assert.AreEqual(1073741789, nearest);
            Assert.AreEqual(1, accuracy);

            num = new BigInteger(1073741823);
            (nearest, accuracy) = primeFinder.NearestLeftPrime(num);
            Assert.AreEqual(1073741789, nearest);
            Assert.IsTrue(accuracy < 1);

            //Big number
            num = BigInteger.Parse("21474836472147483647214748364721474836472147483647214748364721474836472147483647");
            (nearest, accuracy) = primeFinder.NearestLeftPrime(num);
            Assert.AreEqual(BigInteger.Parse("21474836472147483647214748364721474836472147483647214748364721474836472147483289"), nearest);
            Assert.IsTrue(accuracy < 1);
        }
    }
}