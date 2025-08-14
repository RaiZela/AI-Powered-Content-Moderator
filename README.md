# AI-Powered-Content-Moderator
An intelligent content moderation tool built in **C#** that uses AI to detect and handle harmful, offensive, or policy-violating text, images, and other content in real-time.    The application can be integrated into websites, chat applications, forums, or social media platforms to ensure safe and respectful communication.

# This is a work in progress project

# Features
## 1. Text Moderation
- User can input or paste text into a textbox.
- Submit the text to Azure Content Moderator.

- Displayed results showing:
  
  1)Profanity detection (flag bad words).
  
  2)Classification (e.g., offensive, threatening).
  
  3)Suggestions for flagged content (if available).

## 2. Image Moderation
- User can upload an image (JPEG, PNG).
- Submit the image to Azure Content Moderator.
- Display results showing:

  1) Adult content detection.
  
  2) Racy content detection.
   
  3) Additional metadata (if provided by the API).

## 3. Clear Result Presentation
- Show a clean, user-friendly summary of moderation results.
- Highlight flagged sections in text with colors/icons.
- Show image with overlay icons if flagged.

## 4. Error Handling
- Inform the user if API call fails or input is invalid.
- Prevent submission of empty input.

## 5. Responsive UI
- Works well on desktop and mobile browsers.
- Simple, minimal design focusing on input → results flow.

## 6. Existing APIs
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
  
# Screenshots or GIF demos
//Updated while working
# Technologies Used
- .NET8.0
- Minimal APIs
- Azure
- Azure Computer Vision
- Azure Cognitive Services
  
# Setup & Installation Instructions

# How to Obtain and Configure Azure Content Moderator API Key

# Usage Guide

# Future Improvements

# License

