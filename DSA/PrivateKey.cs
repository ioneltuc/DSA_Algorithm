using System.Numerics;

namespace DSA
{
    public class PrivateKey
    {
        public BigInteger P { get; set; }
        public BigInteger Q { get; set; }
        public BigInteger G { get; set; }
        public BigInteger X { get; set; }

        public PrivateKey(BigInteger p, BigInteger q, BigInteger g, BigInteger x)
        {
            P = p;
            Q = q;
            G = g;
            X = x;
        }
    }
}