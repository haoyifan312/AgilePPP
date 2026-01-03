namespace GeneratePrimes
{
    public class PrimeGenerator
    {
        private static bool[] crossedOut;

        public static int[] generatePrimes(int maxValue)
        {
            if (maxValue < 2)         
                return new int[0];
            else
            {
                uncrossIntegersUoTo(maxValue);
                crossOutMultiples();
                return putUncrossedIntegersIntoResult();
            }

        }

        private static void uncrossIntegersUoTo(int maxValue)
        {
            crossedOut = new bool[maxValue + 1];
            for (int i = 2; i < crossedOut.Length; i++)
                crossedOut[i] = false;
        }

        private static void crossOutMultiples()
        {
            int j;
            int maxPrimeFactor = calcMaxPrimeFactor();
            for (int i = 2; i < maxPrimeFactor; i++)
            {
                if (!crossedOut[i])   //if i is uncrossed, cross its multiples
                    crossOutMultiplesOf(i);
            }
        }

        private static void crossOutMultiplesOf(int i)
        {
            for (int multiple = 2 * i; multiple < crossedOut.Length; multiple += i)
                crossedOut[multiple] = true;   //multiple is not prime
        }

        private static int calcMaxPrimeFactor()
        {
            double maxPrimeFactor = Math.Sqrt(crossedOut.Length) + 1;
            return (int)maxPrimeFactor;
        }

        private static int[] putUncrossedIntegersIntoResult()
        {
            int[] primes = new int[numberOfUncrossedIntegers()];

            // move the primes into the result
            for (int i = 2, j = 0; i < crossedOut.Length; i++)
            {
                if (!crossedOut[i])
                    primes[j++] = i;
            }

            return primes;
        }

        private static int numberOfUncrossedIntegers()
        {
            // how many primes are there?
            int count = 0;
            for (int i = 2; i < crossedOut.Length; i++)
            {
                if (!crossedOut[i])
                    count++;
            }
            return count;
        }
    }
}
