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
    "ai.natml.vision.meet": "1.0.2"
  }
}
```

## Predicting the Matte
First, create the predictor:
```csharp
// Fetch the model data from NatML Hub
var modelData = await MLModelData.FromHub("@natml/meet");
// Deserialize the model
var model = modelData.Deserialize();
// Create the Meet predictor
var predictor = new MeetPredictor(model);
```

Then predict the human matte:
```csharp
// Predict
Texture2D image = ...; // This can also be a `WebCamTexture` or any other image feature
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
- Unity 2020.3+

## Quick Tips
- Discover more ML models on [NatML Hub](https://hub.natml.ai).
- See the [NatML documentation](https://docs.natml.ai/unity).
- Join the [NatML community on Discord](https://hub.natml.ai/community).
- Discuss [NatML on Unity Forums](https://forum.unity.com/threads/open-beta-natml-machine-learning-runtime.1109339/).
- Contact us at [hi@natml.ai](mailto:hi@natml.ai).

Thank you very much!