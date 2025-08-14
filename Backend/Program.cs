using ai_powered_content_moderator_backend.BLL.Services;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITextModeratorService, TextModeratorService>();
builder.Services.AddScoped<ITextBlocklistService, TextBloclistService>();
builder.Services.AddScoped<IImageService, ImageService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/textmoderator", ([FromBody] string input, ITextModeratorService textModeratorService) =>
{
    var result = textModeratorService.ModerateText(input);
    return Results.Ok(result);
});

app.MapGet("/textmoderator/blocklist", ([FromBody] string input, string blocklistName, ITextModeratorService textModeratorService) =>
{
    var result = textModeratorService.ModerateTextWithBlocklist(input, blocklistName);
    return Results.Ok(result);
});

app.MapPost("/textblocklist", ([FromBody] string blocklistName, string description, ITextBlocklistService textBloclistService) =>
{
    var result = textBloclistService.CreateOrUpdateTextBlockList(blocklistName, description);
    return result ? Results.Ok() : Results.BadRequest("Failed to create or update blocklist.");
});

app.MapPost("/textblocklist/items", ([FromBody] List<string> items, string blocklistName, ITextBlocklistService textBloclistService) =>
{
    var result = textBloclistService.AddBlocklistitems(items, blocklistName);
    return result ? Results.Ok() : Results.BadRequest("Failed to add items to blocklist.");
});

app.MapGet("/textblocklist", (ITextBlocklistService textBloclistService) =>
{
    var blocklists = textBloclistService.GetBlocklists();
    return Results.Ok(blocklists);
});

app.MapGet("/textblocklist/{name}", (string name, ITextBlocklistService textBloclistService) =>
{
    var blocklist = textBloclistService.GetBlocklistByName(name);
    return blocklist != null ? Results.Ok(blocklist) : Results.NotFound("Blocklist not found.");
});

app.MapGet("/textblocklist/{blocklistName}/items", (string blocklistName, ITextBlocklistService textBloclistService) =>
{
    var blockItems = textBloclistService.GetBlockItems(blocklistName);
    return Results.Ok(blockItems);
});

app.MapGet("/textblocklist/{blocklistName}/items/{blockItemId}", (string blockItemId, string blocklistName, ITextBlocklistService textBloclistService) =>
{
    var blockItem = textBloclistService.GetBlockItem(blockItemId, blocklistName);
    return blockItem != null ? Results.Ok(blockItem) : Results.NotFound("Block item not found.");
});

app.MapDelete("/textblocklist/{blocklistName}/items/{blocklistItemId}", (string blocklistItemId, string blocklistName, ITextBlocklistService textBloclistService) =>
{
    var result = textBloclistService.RemoveBlockItem(blocklistItemId, blocklistName);
    return result ? Results.Ok() : Results.BadRequest("Failed to remove block item.");
});

app.MapDelete("/textblocklist/{blocklistName}/items", ([FromBody] List<string> blocklistItemIds, string blocklistName, ITextBlocklistService textBloclistService) =>
{
    var result = textBloclistService.RemoveBlockItems(blocklistItemIds, blocklistName);
    return result ? Results.Ok() : Results.BadRequest("Failed to remove block items.");
});

app.MapDelete("/textblocklist/{blocklistName}", (string blocklistName, ITextBlocklistService textBloclistService) =>
{
    var result = textBloclistService.DeleteBlockList(blocklistName);
    return result ? Results.Ok() : Results.BadRequest("Failed to delete blocklist.");
});

app.MapPost("/image/moderate", (string imagePath, IImageService imageService) =>
{
    var result = imageService.AnalyzeImageSeverity(imagePath);
    return Results.Ok(result);
});

app.MapPost("/image/caption", (string imagePath, IImageService imageService) =>
{
    var result = imageService.GetImageCaption(imagePath);
    return Results.Ok(result);
});

app.MapPost("/image/denseCaption", (string imagePath, IImageService imageService) =>
{
    var result = imageService.GetImageDenseCaption(imagePath);
    return Results.Ok(result);
});

app.MapPost("/image/tags", (string imagePath, IImageService imageService) =>
{
    var result = imageService.GetImageTags(imagePath);
    return Results.Ok(result);
});

app.MapPost("/image/objects", (string imagePath, IImageService imageService) =>
{
    var result = imageService.GetImageObjects(imagePath);
    return Results.Ok(result);
});

app.MapPost("/image/smartCrops", (string imagePath, IImageService imageService) =>
{
    var result = imageService.GetImageSmartCrops(imagePath);
    return Results.Ok(result);
});

app.MapPost("/image/people", (string imagePath, IImageService imageService) =>
{
    var result = imageService.GetImagePeople(imagePath);
    return Results.Ok(result);
});

app.MapPost("/image/text", (string imagePath, IImageService imageService) =>
{
    var result = imageService.GetImageText(imagePath);
    return Results.Ok(result);
});

app.MapPost("/image/allfeatures", (string imagePath, IImageService imageService) =>
{
    var result = imageService.GetAllImageFeatures(imagePath);
    return Results.Ok(result);
});


app.Run();

