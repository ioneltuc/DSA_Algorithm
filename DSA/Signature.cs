using System.Numerics;

namespace DSA
{
    public class Signature
    {
        public BigInteger R { get; set; }
        public BigInteger S { get; set; }
        public BigInteger TextDocumentDigest { get; set; }

        public Signature(BigInteger r, BigInteger s, int textDocumentDigest)
        {
            R = r;
            S = s;
            TextDocumentDigest = textDocumentDigest;
        }
    }
}