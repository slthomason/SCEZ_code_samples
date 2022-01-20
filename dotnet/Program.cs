using aspnet_dotnet;
using System.Diagnostics;
using System.Text.Json;

string ApiKey = "21140209e56752bfcdd10e5c34ce74e8"; //Get from dashboard
string BaseUrl = "https://api-test.smartcontractsez.com/api/v1/"; //For mainnet, use https://api.smartcontractsez.com/api/v1/
var scezClient = new ScezClient(ApiKey, BaseUrl);

//Get wallet balance and profile data
var wallet = await scezClient.GetWalletBalance();
Console.WriteLine(JsonSerializer.Serialize(wallet));
Console.Write(Environment.NewLine);

//Get nft orders
var nftOrders = await scezClient.GetNftOrders();
//Console.WriteLine(JsonSerializer.Serialize(nftOrders));
//Console.Write(Environment.NewLine);

//Get transactions
var txn = await scezClient.GetTransactions();
//Console.WriteLine(JsonSerializer.Serialize(txn));
//Console.Write(Environment.NewLine);

//If DefaultWalletId > 0 then go ahead else add new wallet address where minted NFT will be sent 
if (wallet?.DefaultWalletId <= 0)
{
    Console.WriteLine("Enter a valid ADA address address where minted NFT will be sent");
    var WalletAddress = Console.ReadLine() ?? "";
    var WalletName = "default";
    await scezClient.CreateWallet(WalletAddress, WalletName);
}

//Mint NFT
var TokenName = "MyAwesomeNFT";
var Description = "This is my Awesome NFT";
var FileName = "logo100.png";
var FilePath = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), FileName);
var nftImage = await scezClient.MintNft(TokenName, Description, FilePath);
Console.WriteLine(JsonSerializer.Serialize(nftImage));






