using Azure;
using Azure.AI.ContentSafety;
using Azure.AI.Vision.ImageAnalysis;

namespace ai_powered_content_moderator_backend.BLL.Helpers;

public static class Authentication
{
    public static ContentSafetyClient GetSafetyClient(IConfiguration configuration)
    {
        var endpoint = configuration["ContentSafety:Endpoint"];
        var key = configuration["ContentSafety:Key"];

        return new ContentSafetyClient(new Uri(endpoint), new AzureKeyCredential(key));
    }

    public static BlocklistClient GetBlocklistClient(IConfiguration configuration)
    {
        var endpoint = configuration["ContentSafety:Endpoint"];
        var key = configuration["ContentSafety:Key"];
        return new BlocklistClient(
               new Uri(endpoint),
               new AzureKeyCredential(key));
    }

    public static ImageAnalysisClient GetImageAnalysisClient(IConfiguration configuration)
    {
        var endpoint = configuration["Vision:Endpoint"];
        var key = configuration["Vision:Key"];
        return new ImageAnalysisClient(
               new Uri(endpoint),
               new AzureKeyCredential(key));
    }
}
