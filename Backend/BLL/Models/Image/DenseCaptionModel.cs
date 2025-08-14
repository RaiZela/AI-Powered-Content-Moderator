namespace ai_powered_content_moderator_backend.BLL.Models.Image;

public class DenseCaptionModel
{
    public string Text { get; set; } // The caption text
    public string Confidence { get; set; } // Confidence score is a double, but we need it string
    public string Language { get; set; } // Language of the caption
    public string ModelVersion { get; set; } // Version of the model used for captioning
    public BoundingBoxModel BoundingBox { get; internal set; }
}
