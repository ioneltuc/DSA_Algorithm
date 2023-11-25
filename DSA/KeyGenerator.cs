using System.Numerics;

namespace DSA
{
    public class KeyGenerator
    {
        private BigInteger _q;
        private BigInteger _p;
        private BigInteger _h;
        private BigInteger _g;
        private BigInteger _x;
        private BigInteger _y;
        private Random _random;

        public KeyGenerator()
        {
            _random = new Random();
        }

        public BigInteger ChoosePrimeQ()
        {
            int qIndex = _random.Next(PrimeNumbersFiller.PrimeNumbers.Count / 5);
            _q = PrimeNumbersFiller.PrimeNumbers[qIndex];

            Console.WriteLine("Choose a random prime 'q':");
            Console.WriteLine($"\tq = {_q}");

            return _q;
        }

        public BigInteger ChoosePrimeP()
        {
            int pIndex = 1;
            while (true)
            {
                if ((PrimeNumbersFiller.PrimeNumbers[pIndex] - 1) % _q == 0)
                {
                    _p = PrimeNumbersFiller.PrimeNumbers[pIndex];
                    break;
                }

                pIndex++;
            }

            Console.WriteLine("Choose a prime 'p' in such a way that (p - 1) mod q = 0");
            Console.WriteLine($"\t({_p} - 1) mod {_q} = 0 =>");
            Console.WriteLine($"\tp = {_p}");

            return _p;
        }

        public BigInteger ChooseRandomH()
        {
            _h = _random.Next(3, (int)_p - 2);

            Console.WriteLine("Choose a random 'h' in such a way that 2 < h < p - 2");
            Console.WriteLine($"\th = {_h}");

            return _h;
        }

        public BigInteger ComputeG()
        {
            var hPower = (_p - 1) / _q;
            _g = BigInteger.ModPow(_h, hPower, _p);

            Console.WriteLine("Compute g = h ^ ((p - 1) / q) mod p");
            Console.WriteLine($"\tg = {_h} ^ (({_p} - 1) / {_q}) mod {_p} =>");
            Console.WriteLine($"\tg = {_g}");

            return _g;
        }

        public BigInteger ChooseRandomX()
        {
            _x = _random.Next(1, (int)_q);

            Console.WriteLine("Choose a random 'x' in such a way that 0 < x < q");
            Console.WriteLine($"\tx = {_x}");

            return _x;
        }

        public BigInteger ComputeY()
        {
            _y = BigInteger.ModPow(_g, _x, _p);

            Console.WriteLine("Compute y = g ^ x mod p");
            Console.WriteLine($"\ty = {_g} ^ {_x} mod {_p}");
            Console.WriteLine($"\ty = {_y}");

            return _y;
        }

        public PrivateKey GeneratePrivateKey()
        {
            Console.WriteLine($"Private key is {{p, q, g, x}} => {{{_p}, {_q}, {_g}, {_x}}}");

            return new PrivateKey(_p, _q, _g, _x);
        }

        public PublicKey GeneratePublicKey()
        {
            Console.WriteLine($"Private key is {{p, q, g, y}} => {{{_p}, {_q}, {_g}, {_y}}}");

            return new PublicKey(_p, _q, _g, _y);
        }
    }
}