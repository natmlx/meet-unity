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
      "scopes": ["ai.natml"]
    }
  ],
  "dependencies": {
    "ai.natml.vision.meet": "1.0.4"
  }
}
```

## Predicting the Matte
First, create the predictor:
```csharp
// Create the model
var model = await MLEdgeModel.Create("@natml/meet");
// Create the Meet predictor
var predictor = new MeetPredictor(model);
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
// Visualize the map into a `RenderTexture`
var result = new RenderTexture(image.width, image.height, 0);
matte.Render(result);
```

___

## Requirements
- Unity 2021.2+

## Quick Tips
- Discover more ML models on [NatML Hub](https://hub.natml.ai).
- See the [NatML documentation](https://docs.natml.ai/unity).
- Join the [NatML community on Discord](https://natml.ai/community).
- Contact us at [hi@natml.ai](mailto:hi@natml.ai).

Thank you very much!