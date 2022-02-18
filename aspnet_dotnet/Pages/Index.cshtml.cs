using aspnet_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspnet_dotnet.Pages
{
    public class IndexModel : PageModel
    {
        string ApiKey = "a9c8ea62789e994f89aec47cde8d1080"; //Get from dashboard
        string BaseUrl = "https://api-test.smartcontractsez.com/api/v2/"; //For mainnet, use https://api.smartcontractsez.com/api/v2/

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public NftModel NftModel { get; set; }

        [BindProperty]
        public MyWallet? MyWallet { get; set; }

        public async Task OnGetAsync()
        {
            var scezClient = new ScezClient(ApiKey, BaseUrl);
            this.MyWallet = await scezClient.GetMyWallet();

        }

        public async Task OnPostAsync()
        {

            //Mainnet config
            if (NftModel.NetworkType == "mainnet")
            {
                BaseUrl = "https://api.smartcontractsez.com/api/v2/";
                ApiKey = "21140209e56752bfcdd10e5c34ce74e8";
            }
            var scezClient = new ScezClient(ApiKey, BaseUrl);

            if (NftModel.FormFile.Length > 0)
            {

                using (var ms = new MemoryStream())
                {
                    NftModel.FormFile.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    var nftImage = await scezClient.MintNft(NftModel.TokenName, NftModel.Description, fileBytes, NftModel.FormFile.FileName, NftModel.PassPhrase);
                    if (nftImage != null)
                    {
                        ModelState.Clear();
                    }

                    ViewData["JavaScript"] = "window.location = '" + Url.Page("Index") + "'";


                }
            }



        }


    }
}