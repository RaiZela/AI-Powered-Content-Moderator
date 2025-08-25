# AI-Powered-Content-Moderator
An intelligent content moderation tool built in **C#** that uses AI to detect and handle harmful, offensive, or policy-violating text, images, and other content in real-time.    The application can be integrated into websites, chat applications, forums, or social media platforms to ensure safe and respectful communication.

# Features
## 1. Text Moderation
- User can input or paste text into a textbox.
- Submit the text to Azure Content Moderator.

- Displayed results showing:
  
  1)Profanity level detection.
  
  2)Classification (e.g., violence, self harm).


## 2. Image Moderation
- User can upload an image (JPEG, PNG).
- Submit the image to Azure Content Moderator.
- Return results showing:

  1)Profanity level detection.
  
  2)Classification (e.g., violence, self harm).
  
  3)Caption
     
  4)Dense caption
     
  5)Image Tags
     
  6)Object detection
     
  7)Smart crops
      
  8)People
      
  9)Text
      
  10)All features


## 3. Error Handling
- Inform the user if API call fails.
- Prevent submission of empty input.

## 4. Existing APIs
- GET /textmoderator/get-severity 
- GET /textmoderator/get-severity-with-blocklist 
- POST /textblocklist/add-blocklist
- POST /textblocklist/add-blocklist-items 
- GET /textblocklist/get-blocklists 
- GET /textblocklist/get-blocklist-by-name/{name} 
- GET /textblocklist/{blocklistName}/get-items 
- GET /textblocklist/{blocklistName}/get-block-item/{blockItemId} 
- DELETE /textblocklist/{blocklistName}/remove-block-item/{blocklistItemId}
- DELETE /textblocklist/{blocklistName}/remove-block-items
- DELETE /textblocklist/delete-blocklist/{blocklistName}
- POST /image/get-severity
- POST /image/get-caption
- POST /image/get-denseCaption
- POST /image/get-tags
- POST /image/get-objects
- POST /image/get-smartCrops
- POST /image/get-people
- POST /image/get-text
- POST /image/get-allfeatures
  

# Technologies Used
- .NET8.0
- Minimal APIs
- Azure
- Azure Computer Vision
- Azure Cognitive Services
  
# Setup & Installation Instructions

# How to Obtain and Configure Azure Content Moderator API Key
## 🔑 How to Obtain and Configure Azure Content Moderator API Key

To use the **Azure Content Moderator API**, you’ll need an API key and endpoint from the Azure Portal.

### Step 1: Create a Content Moderator Resource
1. Go to the [Azure Portal](https://portal.azure.com/).
2. Click **Create a resource** → search for **Content Moderator**.
3. Select **Content Moderator** and click **Create**.
4. Fill in the required details:
   - **Subscription**: Choose your subscription.
   - **Resource group**: Select an existing one or create a new one.
   - **Region**: Pick the closest region.
   - **Name**: Enter a unique name for your resource.
5. Click **Review + Create** and then **Create**.

### Step 2: Get API Key and Endpoint
1. Once the resource is deployed, go to the **Resource Overview** page.
2. Navigate to **Keys and Endpoint** in the left-hand menu.
3. Copy:
   - **Key1** (or Key2 as a backup)
   - **Endpoint URL**

### Step 3: Configure in Your Application
In your project, store the values in a **secure configuration file** (e.g., `appsettings.json` for .NET):

```json
{
  "AzureContentModerator": {
    "Endpoint": "YOUR_ENDPOINT_HERE",
    "ApiKey": "YOUR_API_KEY_HERE"
  }
}
```

Then load them in your application code:
 ```var endpoint = Configuration["AzureContentModerator:Endpoint"]; var apiKey = Configuration["AzureContentModerator:ApiKey"]; ```

# License
- MIT License
