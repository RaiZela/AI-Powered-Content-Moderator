using ai_powered_content_moderator_backend.BLL.Helpers;
using ai_powered_content_moderator_backend.BLL.Models.Image;
using ai_powered_content_moderator_backend.BLL.Models.Text;
using Azure;
using Azure.AI.ContentSafety;
using Azure.AI.Vision.ImageAnalysis;

namespace ai_powered_content_moderator_backend.BLL.Services;

public interface IImageService
{
    List<ModerationResult> AnalyzeImageSeverity(string path);
    ImageAnalysis<Caption> GetImageCaption(string path);
    ImageAnalysis<List<DenseCaptionModel>> GetImageDenseCaption(string path);
    ImageAnalysis<List<Tags>> GetImageTags(string path);
    ImageAnalysis<List<Objects>> GetImageObjects(string path);
    ImageAnalysis<List<SmartCropsModel>> GetImageSmartCrops(string path);
    ImageAnalysis<List<Persons>> GetImagePeople(string path);
    ImageAnalysis<List<Text>> GetImageText(string path);
    AllFeatures GetAllImageFeatures(string path);
}
public class ImageService : IImageService
{
    public IConfiguration _configuration;
    public ImageService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public List<ModerationResult> AnalyzeImageSeverity(string path)
    {
        var endpoint = _configuration["ContentSafety:Endpoint"];
        var key = _configuration["ContentSafety:Key"];

        ContentSafetyClient contentSafetyClient = new ContentSafetyClient(new Uri(endpoint), new AzureKeyCredential(key));

        ContentSafetyImageData imageData = new ContentSafetyImageData(BinaryData.FromBytes(File.ReadAllBytes(path)));
        var request = new AnalyzeImageOptions(imageData);

        Response<AnalyzeImageResult> response = null;
        try
        {
            response = contentSafetyClient.AnalyzeImage(request);

        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine("Analyze image failed.\nStatus code: {0}, Error code: {1}, Error message: {2}", ex.Status, ex.ErrorCode, ex.Message);
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
    public ImageAnalysis<Caption> GetImageCaption(string path)
    {
        var client = Authentication.GetImageAnalysisClient(_configuration);
        using FileStream stream = new FileStream(path, FileMode.Open);
        ImageAnalysisResult result = ImageAnalysisHelper.GetImageAnalysisResult(stream, client, VisualFeatures.Caption);
        return new ImageAnalysis<Caption>
        {
            ModelVersion = result.ModelVersion,
            ImageDimensions = $"{result.Metadata.Width}x{result.Metadata.Height}",
            Feature = new Caption
            {
                Text = result.Caption.Text,
                Confidence = $"{result.Caption.Confidence:F4}"
            }
        };
    }
    public ImageAnalysis<List<DenseCaptionModel>> GetImageDenseCaption(string path)
    {
        var client = Authentication.GetImageAnalysisClient(_configuration);
        using FileStream stream = new FileStream(path, FileMode.Open);
        ImageAnalysisResult result = ImageAnalysisHelper.GetImageAnalysisResult(stream, client, VisualFeatures.DenseCaptions);
        var denseCaptions = new List<DenseCaptionModel>();

        foreach (DenseCaption denseCaption in result.DenseCaptions.Values)
        {
            denseCaptions.Add(new DenseCaptionModel
            {
                Text = denseCaption.Text,
                Confidence = $"{denseCaption.Confidence:F4}",
                BoundingBox = new BoundingBoxModel
                {
                    X = denseCaption.BoundingBox.X,
                    Y = denseCaption.BoundingBox.Y,
                    Width = denseCaption.BoundingBox.Width,
                    Height = denseCaption.BoundingBox.Height
                }
            });
        }

        return new ImageAnalysis<List<DenseCaptionModel>>
        {
            ModelVersion = result.ModelVersion,
            ImageDimensions = $"{result.Metadata.Width}x{result.Metadata.Height}",
            Feature = denseCaptions
        };
    }
    public ImageAnalysis<List<Tags>> GetImageTags(string path)
    {
        var client = Authentication.GetImageAnalysisClient(_configuration);
        using FileStream stream = new FileStream(path, FileMode.Open);
        ImageAnalysisResult result = ImageAnalysisHelper.GetImageAnalysisResult(stream, client, VisualFeatures.Tags);
        var tags = new List<Tags>();

        foreach (DetectedTag tag in result.Tags.Values)
        {
            tags.Add(new Tags
            {
                Name = tag.Name,
                Confidence = $"{tag.Confidence:F4}"
            });
        }

        return new ImageAnalysis<List<Tags>>
        {
            ModelVersion = result.ModelVersion,
            ImageDimensions = $"{result.Metadata.Width}x{result.Metadata.Height}",
            Feature = tags
        };
    }
    public ImageAnalysis<List<Objects>> GetImageObjects(string path)
    {
        var client = Authentication.GetImageAnalysisClient(_configuration);
        using FileStream stream = new FileStream(path, FileMode.Open);
        ImageAnalysisResult result = ImageAnalysisHelper.GetImageAnalysisResult(stream, client, VisualFeatures.Objects);
        var Objects = new List<Objects>();

        foreach (DetectedObject obj in result.Objects.Values)
        {
            Objects.Add(new Objects
            {
                Name = obj.Tags.First().Name,
                BoundingBox = $"{obj.BoundingBox.ToString()}"
            });
        }

        return new ImageAnalysis<List<Objects>>
        {
            ModelVersion = result.ModelVersion,
            ImageDimensions = $"{result.Metadata.Width}x{result.Metadata.Height}",
            Feature = Objects
        };
    }
    public ImageAnalysis<List<SmartCropsModel>> GetImageSmartCrops(string path)
    {
        var client = Authentication.GetImageAnalysisClient(_configuration);
        using FileStream stream = new FileStream(path, FileMode.Open);
        ImageAnalysisResult result = client.Analyze(
                                    BinaryData.FromStream(stream),
                                    VisualFeatures.SmartCrops,
                                    new ImageAnalysisOptions { SmartCropsAspectRatios = new float[] { 0.9F, 1.33F } });
        var SmartCrops = new List<SmartCropsModel>();

        foreach (CropRegion cropRegion in result.SmartCrops.Values)
        {
            SmartCrops.Add(new SmartCropsModel
            {
                CropRegion = cropRegion.AspectRatio,
                BoundingBox = cropRegion.BoundingBox
            });
        }

        return new ImageAnalysis<List<SmartCropsModel>>
        {
            ModelVersion = result.ModelVersion,
            ImageDimensions = $"{result.Metadata.Width}x{result.Metadata.Height}",
            Feature = SmartCrops
        };
    }
    public ImageAnalysis<List<Persons>> GetImagePeople(string path)
    {
        var client = Authentication.GetImageAnalysisClient(_configuration);
        using FileStream stream = new FileStream(path, FileMode.Open);
        ImageAnalysisResult result = ImageAnalysisHelper.GetImageAnalysisResult(stream, client, VisualFeatures.People);
        var persons = new List<Persons>();

        foreach (DetectedPerson person in result.People.Values)
        {
            persons.Add(new Persons
            {
                Boundingbox = person.BoundingBox.ToString(),
                Confidence = person.Confidence
            });
        }

        return new ImageAnalysis<List<Persons>>
        {
            ModelVersion = result.ModelVersion,
            ImageDimensions = $"{result.Metadata.Width}x{result.Metadata.Height}",
            Feature = persons
        };
    }
    public ImageAnalysis<List<Text>> GetImageText(string path)
    {
        var client = Authentication.GetImageAnalysisClient(_configuration);
        using FileStream stream = new FileStream(path, FileMode.Open);
        ImageAnalysisResult result = ImageAnalysisHelper.GetImageAnalysisResult(stream, client, VisualFeatures.Objects);
        var text = new List<Text>();

        foreach (var line in result.Read.Blocks.SelectMany(block => block.Lines))
        {
            List<Word> words = new List<Word>();
            foreach (DetectedTextWord word in line.Words)
            {
                words.Add(new Word
                {
                    Text = word.Text,
                    BoundingPolygon = string.Join(" ", word.BoundingPolygon),
                    Confidence = $"{word.Confidence:F4}"
                });
            }
            text.Add(new Text
            {
                Line = line.Text,
                BoundingPolygon = string.Join(" ", line.BoundingPolygon),
                Word = words
            });
        }

        return new ImageAnalysis<List<Text>>
        {
            ModelVersion = result.ModelVersion,
            ImageDimensions = $"{result.Metadata.Width}x{result.Metadata.Height}",
            Feature = text
        };
    }
    //AllFeatures

    public AllFeatures GetAllImageFeatures(string path)
    {
        var client = Authentication.GetImageAnalysisClient(_configuration);
        using FileStream stream = new FileStream(path, FileMode.Open);

        ImageAnalysisResult result = client.Analyze(
            BinaryData.FromStream(stream),
    VisualFeatures.Caption |
    VisualFeatures.DenseCaptions |
    VisualFeatures.Tags |
    VisualFeatures.Objects |
    VisualFeatures.SmartCrops |
    VisualFeatures.People |
    VisualFeatures.Read);
        var denseCaptions = new List<DenseCaptionModel>();
        foreach (DenseCaption denseCaption in result.DenseCaptions.Values)
        {
            denseCaptions.Add(new DenseCaptionModel
            {
                Text = denseCaption.Text,
                Confidence = $"{denseCaption.Confidence:F4}",
                BoundingBox = new BoundingBoxModel
                {
                    X = denseCaption.BoundingBox.X,
                    Y = denseCaption.BoundingBox.Y,
                    Width = denseCaption.BoundingBox.Width,
                    Height = denseCaption.BoundingBox.Height
                }
            });
        }
        var allFeatures = new AllFeatures
        {
            ModelVersion = result.ModelVersion,
            ImageDimensions = $"{result.Metadata.Width}x{result.Metadata.Height}",
            Caption = new Caption
            {
                Text = result.Caption.Text,
                Confidence = $"{result.Caption.Confidence:F4}"
            },
            DenseCaption = denseCaptions,
            Tags = result.Tags.Values.Select(t => new Tags
            {
                Name = t.Name,
                Confidence = $"{t.Confidence:F4}"
            }).ToList(),
            Objects = result.Objects.Values.Select(o => new Objects
            {
                Name = o.Tags.First().Name,
                BoundingBox = $"{o.BoundingBox.ToString()}"
            }).ToList(),
            SmartCrops = result.SmartCrops.Values.Select(sc => new SmartCropsModel
            {
                CropRegion = sc.AspectRatio,
                BoundingBox = sc.BoundingBox
            }).ToList(),
            People = result.People.Values.Select(p => new Persons
            {
                Boundingbox = p.BoundingBox.ToString(),
                Confidence = p.Confidence
            }).ToList(),
            Text = result.Read.Blocks.SelectMany(block => block.Lines).Select(line => new Text
            {
                Line = line.Text,
                BoundingPolygon = string.Join(" ", line.BoundingPolygon),
                Word = line.Words.Select(word => new Word
                {
                    Text = word.Text,
                    BoundingPolygon = string.Join(" ", word.BoundingPolygon),
                    Confidence = $"{word.Confidence:F4}"
                }).ToList()
            }).ToList()
        };
        return allFeatures;
    }
}
