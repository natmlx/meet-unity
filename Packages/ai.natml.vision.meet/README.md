# Meet
MediaPipe Meet Segmentation for human matting in Unity Engine.

## Installing Meet
Add the following items to your Unity project's `Packages/manifest.json`:
```json
{
  "scopedRegistries": [
    {
      "name": "NatML",
      "url": "https://registry.npmjs.com",
      "scopes": ["ai.natml", "ai.fxn"]
    }
  ],
  "dependencies": {
    "ai.natml.vision.meet": "1.0.7"
  }
}
```

## Predicting the Matte
First, create the predictor:
```csharp
// Create the Meet predictor
var predictor = await MeetPredictor.Create();
```

Then predict the human matte:
```csharp
// Given an image...
Texture2D image = ...;
// Predict the human matte
MeetPredictor.Matte matte = predictor.Predict(image);
```

Finally, render the segmentation map to a `RenderTexture`:
```csharp
// Create a destination `RenderTexture`
var result = new RenderTexture(image.width, image.height, 0);
// Visualize the map into a `RenderTexture`
matte.Render(result);
```

___

## Requirements
- Unity 2022.3+

## Quick Tips
- Join the [NatML community on Discord](https://natml.ai/community).
- Discover more ML models on [NatML Hub](https://hub.natml.ai).
- See the [NatML documentation](https://docs.natml.ai/unity).
- Contact us at [hi@natml.ai](mailto:hi@natml.ai).

Thank you very much!