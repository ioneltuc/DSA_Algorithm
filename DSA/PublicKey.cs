using System.Numerics;

namespace DSA
{
    public class PublicKey
    {
        public BigInteger P { get; set; }
        public BigInteger Q { get; set; }
        public BigInteger G { get; set; }
        public BigInteger Y { get; set; }

        public PublicKey(BigInteger p, BigInteger q, BigInteger g, BigInteger y)
        {
            P = p;
            Q = q;
            G = g;
            Y = y;
        }
    }
}