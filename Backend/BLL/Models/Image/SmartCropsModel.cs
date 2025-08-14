using Azure.AI.Vision.ImageAnalysis;

namespace ai_powered_content_moderator_backend.BLL.Models.Image;

public class SmartCropsModel
{
    public float CropRegion { get; internal set; }
    public ImageBoundingBox BoundingBox { get; internal set; }
}
