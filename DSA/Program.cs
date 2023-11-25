using DSA;

PrimeNumbersFiller.Fill(1000);

Console.WriteLine("********** DSA - Key Generation **********");
var keyGenerator = new KeyGenerator();
keyGenerator.ChoosePrimeQ();
keyGenerator.ChoosePrimeP();
keyGenerator.ChooseRandomH();
keyGenerator.ComputeG();
keyGenerator.ChooseRandomX();
keyGenerator.ComputeY();
var privateKey = keyGenerator.GeneratePrivateKey();
var publicKey = keyGenerator.GeneratePublicKey();

Console.WriteLine();
Console.WriteLine("********** DSA - Signing **********");
var textDocumentToSign = "Acordul de studii UTM";
var signApplier = new SignApplier(textDocumentToSign, privateKey);
signApplier.ChooseRandomK();
signApplier.ComputeR();
signApplier.ComputeI();
signApplier.ComputeS();
var signature = signApplier.GenerateSignature();

Console.WriteLine();
Console.WriteLine("********** DSA - Verification **********");
var signVerifier = new SignVerifier(publicKey, signature);
if (signVerifier.InitialVerification())
{
    signVerifier.ComputeW();
    signVerifier.ComputeU1();
    signVerifier.ComputeU2();
    signVerifier.ComputeV();

    if (signVerifier.IsSignatureValid())
    {
        Console.WriteLine("Signature is valid");
    }
    else
    {
        Console.WriteLine("Signature is invalid");
    }
}
else
{
    Console.WriteLine("'0 < r < q and 0 < s < q' validation did not pass");
}