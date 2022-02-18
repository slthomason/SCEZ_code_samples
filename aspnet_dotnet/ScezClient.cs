﻿using aspnet_dotnet.Models;
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
        /// <param name="PassPhrase"></param>
        /// <returns></returns>
        public async Task<NftImage?> MintNft(string TokenName, string Description, byte[] FileContent, string FileName, string PassPhrase)
        {
            //Step #1: Create NFT
            var nft = await CreateNft(TokenName, Description, PassPhrase);
            if (nft != null)
            {
                //Step #2: Upload NFT Image
                return await UploadNftImage(nft.OrderID, FileContent, FileName);
            }

            return null;
        }




        /// <summary>
        /// Step #2: Create NFT
        /// </summary>
        /// <param name="TokenName"></param>
        /// <param name="Description"></param>
        /// <param name="PassPhrase"></param>
        /// <returns></returns>
        public async Task<Nft?> CreateNft(string TokenName, string Description, string PassPhrase)
        {

            var nftData = new
            {
                token_name = TokenName,//"MyAwesomeNFT",(don't use space or any other character)
                description = Description,//"This is my first NFT using SCEZ REST api",
                wallet_passphrase = PassPhrase //Secret passphrase
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
        /// Get my-wallet
        /// </summary>
        /// <returns></returns>
        public async Task<MyWallet?> GetMyWallet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "wallet/my-wallet");
            var requestContent = new StringContent("");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = requestContent;
            using (var response = await _restApiClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<MyWallet>($"{body}");
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
