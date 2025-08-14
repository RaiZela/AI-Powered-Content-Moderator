namespace ai_powered_content_moderator_backend.BLL.Models.Image;

public class Word
{
    public string Text { get; set; }
    public string BoundingPolygon { get; set; }
    public string Confidence { get; set; }
}
