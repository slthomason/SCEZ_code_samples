using aspnet_dotnet;
using System.Diagnostics;

string ApiKey = "21140209e56752bfcdd10e5c34ce74e8"; //Get from dashboard
string BaseUrl = "https://api-test.smartcontractsez.com/api/v1/"; //For mainnet, use https://api.smartcontractsez.com/api/v1/
var scezClient = new ScezClient(ApiKey, BaseUrl);
var TokenName = "MyAwesomeNFT";
var Description = "This is my Awesome NFT";
var FileName = "logo100.png";
var FilePath = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), FileName);
var nftImage = await scezClient.MintNft(TokenName, Description, FilePath);
Console.Write(nftImage);


