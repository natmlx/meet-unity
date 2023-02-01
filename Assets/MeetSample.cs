/*
*   Meet
*   Copyright © 2023 NatML Inc. All Rights Reserved.
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

        private MLEdgeModel model;
        private MeetPredictor predictor;
        private RenderTexture matteTexture;

        private async void Start () {
            // Create the model
            model = await MLEdgeModel.Create("@natml/meet");
            // Create the Meet predictor
            predictor = new MeetPredictor(model);
            // Listen for camera frames
            cameraManager.OnCameraFrame.AddListener(OnCameraFrame);
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

        private void OnDisable () {
            // Stop listening for camera frames
            cameraManager.OnCameraFrame.RemoveListener(OnCameraFrame);
            // Dispose model
            model?.Dispose();
        }
    }
}