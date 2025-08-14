using Azure.AI.Vision.ImageAnalysis;

namespace ai_powered_content_moderator_backend.BLL.Helpers;

public static class ImageAnalysisHelper
{
    public static ImageAnalysisResult GetImageAnalysisResult(FileStream stream, ImageAnalysisClient client, VisualFeatures features)
    {
        return client.Analyze(
                        BinaryData.FromStream(stream),
                        features,
                        new ImageAnalysisOptions { GenderNeutralCaption = true });
    }
}
