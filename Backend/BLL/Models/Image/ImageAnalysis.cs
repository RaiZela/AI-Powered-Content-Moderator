namespace ai_powered_content_moderator_backend.BLL.Models.Image;

public class ImageAnalysis<T> where T : class
{
    public string ModelVersion { get; set; }
    public string ImageDimensions { get; set; }
    public T Feature { get; set; }
}
