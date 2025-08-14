namespace ai_powered_content_moderator_backend.BLL.Models.Image;

public class Caption
{
    public string Text { get; set; }
    public string Confidence { get; set; } //incoming value is in double, but we store it as string for simplicity
}
