/*
*   Meet
*   Copyright (c) 2022 NatML Inc. All Rights Reserved.
*/

namespace NatML.Examples {

    using UnityEngine;
    using UnityEngine.UI;
    using NatML.VideoKit;
    using NatML.Vision;

    public class MeetSample : MonoBehaviour {

        [Header(@"Camera Manager")]
        public VideoKitCameraManager cameraManager;

        [Header(@"UI")]
        public RawImage rawImage;
        public AspectRatioFitter aspectFitter;

        private MLModel model;
        private MeetPredictor predictor;
        private RenderTexture matteTexture;

        private async void Start () {
            Debug.Log("Fetching model from NatML...");
            // Fetch the model data from NatML Hub
            var modelData = await MLModelData.FromHub("@natml/meet");
            // Create the edge model
            model = new MLEdgeModel(modelData);
            // Create the Meet predictor
            predictor = new MeetPredictor(model);
            // Listen for camera frames
            cameraManager.OnFrame.AddListener(OnCameraFrame);
        }

        private void OnCameraFrame (CameraFrame frame) {
            // Predict matte
            var matte = predictor.Predict(frame);            
            // Render matte to texture
            matteTexture = matteTexture ? matteTexture : new RenderTexture(frame.image.width, frame.image.height, 0);
            matte.Render(matteTexture);
            // Display matte texture
            rawImage.texture = matteTexture;
            aspectFitter.aspectRatio = (float)matteTexture.width / matteTexture.height;   
        }

        private void OnDisable () => model?.Dispose(); // Dispose model
    }
}