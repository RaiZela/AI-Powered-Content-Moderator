namespace ai_powered_content_moderator_backend.BLL.Models.Image;

public class Text
{
    public string Line { get; set; }
    public string BoundingPolygon { get; set; }
    public List<Word> Word { get; set; }
}

