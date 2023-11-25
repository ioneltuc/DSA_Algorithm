using System.Numerics;

namespace DSA
{
    public class SignVerifier
    {
        private PublicKey _publicKey;
        private Signature _signature;
        private BigInteger _w;
        private BigInteger _u1;
        private BigInteger _u2;
        private BigInteger _v;

        public SignVerifier(PublicKey publicKey, Signature signature)
        {
            _publicKey = publicKey;
            _signature = signature;
        }

        public bool InitialVerification()
        {
            Console.WriteLine("Verify 0 < r < q and 0 < s < q");
            Console.WriteLine($"\t0 < {_signature.R} < {_publicKey.Q} and 0 < {_signature.S} < {_publicKey.Q}");
            if (_signature.R > 0 &&
                _signature.R < _publicKey.Q &&
                _signature.S > 0 &&
                _signature.S < _publicKey.Q)
            {
                return true;
            }

            return false;
        }

        public BigInteger ComputeW()
        {
            int w = 1;
            while (true)
            {
                if (_signature.S * w % _publicKey.Q == 1)
                {
                    _w = w;
                    break;
                }

                w++;
            }

            Console.WriteLine("Choose a number 'w' in such a way that s * w mod q = 1");
            Console.WriteLine($"\t{_signature.S} * {_w} mod {_publicKey.Q} = 1 =>");
            Console.WriteLine($"\tw = {_w}");

            return _w;
        }

        public BigInteger ComputeU1()
        {
            _u1 = _signature.TextDocumentDigest * _w % _publicKey.Q;

            Console.WriteLine("Compute u1 = H(m) * w mod q");
            Console.WriteLine($"\tu1 = {_signature.TextDocumentDigest} * {_w} mod {_publicKey.Q}");
            Console.WriteLine($"\tu1 = {_u1}");

            return _u1;
        }

        public BigInteger ComputeU2()
        {
            _u2 = _signature.R * _w % _publicKey.Q;

            Console.WriteLine("Compute u2 = r * w mod q");
            Console.WriteLine($"\tu2 = {_signature.R} * {_w} mod {_publicKey.Q}");
            Console.WriteLine($"\tu2 = {_u2}");

            return _u2;
        }

        public BigInteger ComputeV()
        {
            _v = (BigInteger.Pow(_publicKey.G, (int)_u1) * BigInteger.Pow(_publicKey.Y, (int)_u2) % _publicKey.P) % _publicKey.Q;

            Console.WriteLine("Compute v = (g ^ u1 * y ^ u2 mod p) mod q");
            Console.WriteLine($"\tv = ({_publicKey.G} ^ {_u1} * {_publicKey.Y} ^ {_u2} mod {_publicKey.P}) mod {_publicKey.Q}");
            Console.WriteLine($"\tv = {_v}");

            return _v;
        }

        public bool IsSignatureValid()
        {
            Console.WriteLine("Signature is valid if v = r");
            Console.WriteLine($"v = {_v}");
            Console.WriteLine($"r = {_signature.R}");
            if (_v == _signature.R)
            {
                return true;
            }

            return false;
        }
    }
}