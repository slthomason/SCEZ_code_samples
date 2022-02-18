using aspnet_dotnet;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

string ApiKey = "a9c8ea62789e994f89aec47cde8d1080"; //Get from dashboard
string BaseUrl = "https://api-test.smartcontractsez.com/api/v2/"; //For mainnet, use https://api.smartcontractsez.com/api/v2/
var scezClient = new ScezClient(ApiKey, BaseUrl);

//Get my-wallet data
//Find out if wallet is created and passphrase is set
var wallet = await scezClient.GetMyWallet();
Console.Write(Environment.NewLine);

if (wallet?.IsPassPhrase == false)
{
    Console.WriteLine("Wallet passphrase is not set. Not allowed to make transactions!");
    Console.WriteLine("Please login to dashbord to set your passphrase");
    Console.ReadKey();

}
else
{

    string nftImagesPath = @"..\..\..\NftImages\"; //Set full path if any issue

    //Mint NFT
    var TokenName = "MyAwesomeNFT";
    var Description = "This is my Awesome NFT";
    var PassPhrase = "123456"; //Keep it safe and secure
    var FileName = "logo100.png";
    var FilePath = nftImagesPath + FileName;
    var nftImage = await scezClient.MintNft(TokenName, Description, FilePath, PassPhrase);
    Console.WriteLine(JsonSerializer.Serialize(nftImage));

}










