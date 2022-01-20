using aspnet_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace aspnet_dotnet
{
    public class ScezClient
    {
        HttpClient _restApiClient;
        public ScezClient(string ApiKey, string BaseUrl)
        {
            _restApiClient = CreateRestClient(ApiKey, BaseUrl);

        }

        /// <summary>
        /// Call This Api to Create NFT in One Flow
        /// </summary>
        /// <param name="TokenName"></param>
        /// <param name="Description"></param>
        /// <param name="FilePath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public async Task<NftImage?> MintNft(string TokenName, string Description, byte[] FileContent, string FileName)
        {

            //Step #1: Get Default Wallet(Where minted NFT will be sent)
            var wallets = await GetWallets();
            if (wallets != null)
            {
                var defaultWallet = wallets.FirstOrDefault();
                //Step #2: Create NFT
                var nft = await CreateNft(TokenName, Description, defaultWallet?.Id);
                if (nft != null)
                {
                    //Step #3: Upload NFT Image
                    return await UploadNftImage(nft.OrderID, FileContent, FileName);
                }
            }
            return null;
        }

        /// <summary>
        /// Add default ADA wallet
        /// </summary>
        /// <param name="WalletAddress"></param>
        /// <param name="WalletName"></param>
        /// <returns></returns>
        public async Task<Wallet?> CreateWallet(string WalletAddress, string WalletName)
        {

            var nftData = new
            {
                wallet_address = WalletAddress,// Valid ADA address
                wallet_name = WalletName,//Friendly name
            };
            var nft = JsonSerializer.Serialize(nftData);
            var request = new HttpRequestMessage(HttpMethod.Post, "wallet");
            var requestContent = new StringContent(nft, Encoding.UTF8);
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = requestContent;
            using (var response = await _restApiClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Wallet>($"{body}");
            }

        }

        /// <summary>
        /// Step #1: Get Default Wallet
        /// </summary>
        /// <returns></returns>
        public async Task<List<Wallet>?> GetWallets()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "wallet/list");
            var requestContent = new StringContent("");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = requestContent;
            using (var response = await _restApiClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Wallet>>($"{body}");
            }
        }

        /// <summary>
        /// Step #2: Create NFT
        /// </summary>
        /// <param name="TokenName"></param>
        /// <param name="Description"></param>
        /// <param name="WalletID"></param>
        /// <returns></returns>
        public async Task<Nft?> CreateNft(string TokenName, string Description, int? WalletID)
        {

            var nftData = new
            {
                token_name = TokenName,//"MyAwesomeNFT",(don't use space or any other character)
                description = Description,//"This is my first NFT using SCEZ REST api",
                wallet_id = WalletID //Get from wallet list
            };
            var nft = JsonSerializer.Serialize(nftData);
            var request = new HttpRequestMessage(HttpMethod.Post, "nft");
            var requestContent = new StringContent(nft, Encoding.UTF8);
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = requestContent;
            using (var response = await _restApiClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Nft>($"{body}");
            }

        }

        /// <summary>
        /// Step #3: Upload NFT Image
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="FilePath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public async Task<NftImage?> UploadNftImage(string OrderID, byte[] FileContent, string FileName)
        {
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new ByteArrayContent(FileContent), "file", FileName);

            using (var response = await _restApiClient.PostAsync($"nft/asset/upload?order_id={OrderID}", multipartContent))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<NftImage>($"{body}");
            }

        }




        /// <summary>
        /// Fetch Wallet Balance and Profile Data
        /// </summary>
        /// <returns></returns>
        public async Task<WalletBalance?> GetWalletBalance()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "wallet/balance");
            var requestContent = new StringContent("");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = requestContent;
            using (var response = await _restApiClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<WalletBalance>($"{body}");
            }


        }

        /// <summary>
        /// Get all NFT orders
        /// </summary>
        /// <returns></returns>
        public async Task<List<NftOrder>?> GetNftOrders()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "nft/list");
            var requestContent = new StringContent("");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = requestContent;
            using (var response = await _restApiClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<NftOrder>>($"{body}");
            }
        }

        /// <summary>
        /// Get all transactions
        /// </summary>
        /// <returns></returns>
        public async Task<List<Transactions>?> GetTransactions()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "user/transactions/list");
            var requestContent = new StringContent("");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = requestContent;
            using (var response = await _restApiClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Transactions>>($"{body}");
            }
        }


        /// <summary>
        /// Default HttpClient Instance
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        private HttpClient CreateRestClient(string ApiKey, string BaseUrl)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseUrl);
            if (ApiKey != null)
            {
                httpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey);
            }
            return httpClient;
        }
    }

}
