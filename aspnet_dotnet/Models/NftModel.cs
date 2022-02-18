namespace aspnet_dotnet.Models
{
    public class NftModel
    {
        public string PassPhrase { get; set; }
        public string NetworkType { get; set; }

        public string TokenName { get; set; }

        public string Description { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
