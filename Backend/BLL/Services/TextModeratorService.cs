using ai_powered_content_moderator_backend.BLL.Helpers;
using ai_powered_content_moderator_backend.BLL.Models.Text;
using Azure;
using Azure.AI.ContentSafety;

namespace BLL.Services;
public interface ITextModeratorService
{
    List<ModerationResult> ModerateText(string input);
    List<ModerationResult> ModerateTextWithBlocklist(string input, string blocklistName);
}

public class TextModeratorService : ITextModeratorService
{
    private readonly IConfiguration _configuration;
    public TextModeratorService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<ModerationResult> ModerateText(string input)
    {
        var contentSafetyClient = Authentication.GetSafetyClient(_configuration);
        Response<AnalyzeTextResult> response = null;
        input = string.IsNullOrEmpty(input) ? "test" : input;
        var request = new AnalyzeTextOptions(text: input);
        response = contentSafetyClient.AnalyzeText(request);
        List<ModerationResult> results = new List<ModerationResult>();
        if (response.Value != null && response.Value.CategoriesAnalysis != null)
        {
            foreach (var category in response.Value.CategoriesAnalysis)
            {
                results.Add(new ModerationResult
                {
                    Category = category.Category.ToString(),
                    Severity = (int)category.Severity
                });
            }
        }

        return results;
    }

    public List<ModerationResult> ModerateTextWithBlocklist(string input, string blocklistName)
    {
        var endpoint = _configuration["ContentSafety:Endpoint"];
        var key = _configuration["ContentSafety:Key"];
        ContentSafetyClient contentSafetyClient = new ContentSafetyClient(new Uri(endpoint), new AzureKeyCredential(key));

        var request = new AnalyzeTextOptions(input);
        request.BlocklistNames.Add(blocklistName);
        request.HaltOnBlocklistHit = true;

        Response<AnalyzeTextResult> response;

        try
        {
            response = contentSafetyClient.AnalyzeText(request);
        }
        catch (Exception ex)
        {
            throw;
        }

        List<ModerationResult> results = new List<ModerationResult>();
        if (response.Value != null && response.Value.CategoriesAnalysis != null)
        {
            foreach (var category in response.Value.CategoriesAnalysis)
            {
                results.Add(new ModerationResult
                {
                    Category = category.Category.ToString(),
                    Severity = (int)category.Severity
                });
            }
        }

        return results;
    }
}

