namespace ai_powered_content_moderator_backend.BLL.Models.Image;

public class AllFeatures
{
    public string ModelVersion { get; set; }
    public string ImageDimensions { get; set; }
    public Caption Caption { get; set; }
    public List<DenseCaptionModel> DenseCaption { get; set; }
    public List<Tags> Tags { get; set; }
    public List<Objects> Objects { get; set; }
    public List<SmartCropsModel> SmartCrops { get; set; }
    public List<Persons> People { get; set; }
    public List<Text> Text { get; set; }
}
