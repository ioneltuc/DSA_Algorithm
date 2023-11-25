namespace DSA
{
    public class PrimeNumbersFiller
    {
        public static List<int> PrimeNumbers { get; private set; } = new List<int>();

        public static void Fill(int n)
        {
            bool[] sieveOfEratosthenes = Enumerable.Repeat(true, n + 1).ToArray();
            sieveOfEratosthenes[0] = false;
            sieveOfEratosthenes[1] = false;

            int i = 2;

            while (i * i <= n)
            {
                if (sieveOfEratosthenes[i])
                {
                    int k = i * i;
                    while (k <= n)
                    {
                        sieveOfEratosthenes[k] = false;
                        k += i;
                    }
                }

                i += 1;
            }

            for (int j = 0; j < sieveOfEratosthenes.Length; j++)
            {
                if (sieveOfEratosthenes[j])
                {
                    PrimeNumbers.Add(j);
                }
            }
        }
    }
}