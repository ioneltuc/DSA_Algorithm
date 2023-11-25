using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace DSA
{
    public class SignApplier
    {
        private Random _random;
        private string _textDocument;
        private int _textDocumentDigest;
        private PrivateKey _privateKey;
        private BigInteger _k;
        private BigInteger _r;
        private BigInteger _i;
        private BigInteger _s;

        public SignApplier(string textDocument, PrivateKey privateKey)
        {
            _textDocument = textDocument;
            _privateKey = privateKey;
            _random = new Random();
        }

        private int GetTextDocumentDigest()
        {
            using (HashAlgorithm sha = SHA256.Create())
            {
                var textDocumentHash = sha.ComputeHash(Encoding.UTF8.GetBytes(_textDocument));
                _textDocumentDigest = textDocumentHash.GetHashCode();

                Console.WriteLine($"Text document hash: {Encoding.UTF8.GetString(textDocumentHash)}");
                Console.WriteLine($"Text document digest: {_textDocumentDigest}");

                return _textDocumentDigest;
            }
        }

        public BigInteger ChooseRandomK()
        {
            _k = _random.Next(2, (int)_privateKey.Q - 1);

            Console.WriteLine("Choose a random 'k' in such a way that 1 < k < q - 1");
            Console.WriteLine($"\tk = {_k}");

            return _k;
        }

        public BigInteger ComputeR()
        {
            _r = BigInteger.ModPow(_privateKey.G, _k, _privateKey.P) % _privateKey.Q;

            Console.WriteLine("Compute r = (g ^ k mod p) mod q");
            Console.WriteLine($"\tr = ({_privateKey.G} ^ {_k} mod {_privateKey.P}) mod {_privateKey.Q}");
            Console.WriteLine($"\tr = {_r}");

            return _r;
        }

        public BigInteger ComputeI()
        {
            int i = 1;

            while (true)
            {
                if (_k * i % _privateKey.Q == 1)
                {
                    _i = i;
                    break;
                }

                i++;
            }

            Console.WriteLine("Choose a number 'i' in such a way that k * i mod q = 1");
            Console.WriteLine($"\t{_k} * {_i} mod {_privateKey.Q} = 1");
            Console.WriteLine($"\ti = {_i}");

            return _i;
        }

        public BigInteger ComputeS()
        {
            _s = (_i * (GetTextDocumentDigest() + _privateKey.X * _r)) % _privateKey.Q;

            Console.WriteLine("Compute s = (i * (H(m) + x * r)) mod q");
            Console.WriteLine($"\ts = ({_i} * ({_textDocumentDigest} + {_privateKey.X} * {_r})) mod {_privateKey.Q}");
            Console.WriteLine($"\ts = {_s}");

            return _s;
        }

        public Signature GenerateSignature()
        {
            return new Signature(_r, _s, _textDocumentDigest);
        }
    }
}