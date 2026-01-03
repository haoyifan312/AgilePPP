using GeneratePrimes;

namespace TestPrimeGenerator
{
    public class TestGeneratePrimes
    {
        [Fact]
        public void testPrimes()
        {
            int[] nullArray = GeneratePrimes.PrimeGenerator.generatePrimes(0);
            Assert.Empty(nullArray);

            int[] minArray = GeneratePrimes.PrimeGenerator.generatePrimes(2);
            Assert.Single(minArray);
            Assert.Equal(2, minArray[0]);

            int[] threeArray = GeneratePrimes.PrimeGenerator.generatePrimes(3);
            Assert.Equal(2, threeArray.Length);
            Assert.Equal(2, threeArray[0]);
            Assert.Equal(3, threeArray[1]);

            int[] centArray = GeneratePrimes.PrimeGenerator.generatePrimes(100);
            Assert.Equal(25, centArray.Length);
            Assert.Equal(97, centArray[24]);
        }

        [Fact]
        public void testExhaustive()
        {
            for (int i = 2; i < 500; i++)
                verifyPrimeList(GeneratePrimes.PrimeGenerator.generatePrimes(i));
        }

        private void verifyPrimeList(int[] list)
        {
            for (int i = 0; i < list.Length; i++)
                verifyPrime(list[i]);
        }

        private void verifyPrime(int n)
        {
            for (int factor = 2; factor < n; factor++)
                Assert.True(n % factor != 0);
        }
    }
}
