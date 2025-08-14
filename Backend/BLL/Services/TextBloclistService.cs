using ai_powered_content_moderator_backend.BLL.Helpers;
using ai_powered_content_moderator_backend.BLL.Models.Text;
using Azure.AI.ContentSafety;
using Azure.Core;
public interface ITextBlocklistService
{
    bool CreateOrUpdateTextBlockList(string blocklistName, string description);
    bool AddBlocklistitems(List<string> items, string blocklistName);
    List<Blocklist> GetBlocklists();
    Blocklist GetBlocklistByName(string name);
    List<BlockItem> GetBlockItems(string blocklistName);
    BlockItem GetBlockItem(string blockItemId, string blocklistName);
    bool RemoveBlockItem(string blocklistItemId, string blockListName);
    bool RemoveBlockItems(List<string> blocklistItemIds, string blockListName);
    bool DeleteBlockList(string blocklistName);
}
public class TextBloclistService : ITextBlocklistService
{
    private readonly IConfiguration _configuration;
    public TextBloclistService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public bool CreateOrUpdateTextBlockList(string blocklistName, string description)
    {
        var blocklistClient = Authentication.GetBlocklistClient(_configuration);
        var name = blocklistName;
        var blocklistDescription = description;
        var data = new
        {
            Description = blocklistDescription
        };
        var createResponse = blocklistClient.CreateOrUpdateTextBlocklist(name, RequestContent.Create(data));

        if (createResponse.Status == 201)
            return true;

        return false;
    }
    public bool AddBlocklistitems(List<string> items, string blocklistName)
    {
        var blocklistClient = Authentication.GetBlocklistClient(_configuration);
        var blockitems = new TextBlocklistItem[items.Count];
        foreach (var item in items)
        {
            var index = items.IndexOf(item);
            blockitems[index] = new TextBlocklistItem(item);
        }
        var addedBlockItems = blocklistClient.AddOrUpdateBlocklistItems(blocklistName, new AddOrUpdateTextBlocklistItemsOptions(blockitems));

        if (addedBlockItems != null && addedBlockItems.Value != null)
            return true;

        return false;
    }
    public List<Blocklist> GetBlocklists()
    {
        var blocklistClient = Authentication.GetBlocklistClient(_configuration);
        var blocklists = blocklistClient.GetTextBlocklists();
        List<Blocklist> blocklistItems = new List<Blocklist>();
        foreach (var blocklist in blocklists)
        {
            blocklistItems.Add(new Blocklist
            {
                Name = blocklist.Name,
                Description = blocklist.Description,
            });
        }
        return blocklistItems;
    }
    public Blocklist GetBlocklistByName(string name)
    {
        var blocklistClient = Authentication.GetBlocklistClient(_configuration);
        var blocklist = blocklistClient.GetTextBlocklist(name);

        if (blocklist != null)
        {
            return new Blocklist
            {
                Name = blocklist.Value.Name,
                Description = blocklist.Value.Description,
            };
        }
        return null;
    }
    public List<BlockItem> GetBlockItems(string blocklistName)
    {
        var blocklistClient = Authentication.GetBlocklistClient(_configuration);
        var allBlockItems = blocklistClient.GetTextBlocklistItems(blocklistName);
        List<BlockItem> blockItems = new List<BlockItem>();
        foreach (var blocklist in allBlockItems)
        {
            blockItems.Add(new BlockItem
            {
                Id = blocklist.BlocklistItemId,
                Text = blocklist.Text,
                Description = blocklist.Description
            });
        }

        return blockItems;
    }
    public BlockItem GetBlockItem(string blockItemId, string blocklistName)
    {
        var blocklistClient = Authentication.GetBlocklistClient(_configuration);
        var getBlockItem = blocklistClient.GetTextBlocklistItem(blocklistName, blockItemId);

        return new BlockItem
        {
            Id = getBlockItem.Value.BlocklistItemId,
            Text = getBlockItem.Value.Text,
            Description = getBlockItem.Value.Description
        };

    }
    public bool RemoveBlockItem(string blocklistItemId, string blockListName)
    {
        var blocklistClient = Authentication.GetBlocklistClient(_configuration);
        var removeBlockItemIds = new List<string> { blocklistItemId };
        var response = blocklistClient.RemoveBlocklistItems(blockListName, new RemoveTextBlocklistItemsOptions(removeBlockItemIds));
        if (response.Status == 201)
            return true;
        return false;
    }
    public bool RemoveBlockItems(List<string> blocklistItemIds, string blockListName)
    {
        var blocklistClient = Authentication.GetBlocklistClient(_configuration);
        var response = blocklistClient.RemoveBlocklistItems(blockListName, new RemoveTextBlocklistItemsOptions(blocklistItemIds));
        if (response != null && response.Status == 204)
            return true;

        return false;
    }
    public bool DeleteBlockList(string blocklistName)
    {
        var blocklistClient = Authentication.GetBlocklistClient(_configuration);
        var response = blocklistClient.DeleteTextBlocklist(blocklistName);
        if (response != null && response.Status == 204)
            return true;

        return false;
    }
}
