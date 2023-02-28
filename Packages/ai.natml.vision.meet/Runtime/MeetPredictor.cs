/* 
*   Meet
*   Copyright © 2023 NatML Inc. All Rights Reserved.
*/

namespace NatML.Vision {

    using System;
    using System.Threading.Tasks;
    using NatML.Features;
    using NatML.Internal;
    using NatML.Types;

    /// <summary>
    /// Meet segmentation from MediaPipe.
    /// </summary>
    public sealed partial class MeetPredictor : IMLPredictor<MeetPredictor.Matte> {

        #region --Client API--
        /// <summary>
        /// Segment a person in an image.
        /// </summary>
        /// <param name="inputs">Input image.</param>
        /// <returns>Human matte.</returns>
        public Matte Predict (params MLFeature[] inputs) {
            // Pre-process
            var input = inputs[0];
            if (input is MLImageFeature imageFeature) {
                (imageFeature.mean, imageFeature.std) = model.normalization;
                imageFeature.aspectMode = model.aspectMode;
            }
            // Predict
            using var inputFeature = (input as IMLEdgeFeature).Create(model.inputs[0]);
            using var outputFeatures = model.Predict(inputFeature);
            // Marshal
            var matte = new MLArrayFeature<float>(outputFeatures[0]);
            var result = new Matte(matte.shape[2], matte.shape[1], matte.ToArray());
            // Return
            return result;
        }

        /// <summary>
        /// Dispose the model and release resources.
        /// </summary>
        public void Dispose () => model.Dispose();

        /// <summary>
        /// Create the Meet predictor.
        /// </summary>
        /// <param name="configuration">Edge model configuration.</param>
        /// <param name-"accessKey">NatML access key.</param>
        public static async Task<MeetPredictor> Create (
            MLEdgeModel.Configuration configuration = null,
            string accessKey = null
        ) {
            var model = await MLEdgeModel.Create("@natml/meet", configuration, accessKey);
            var predictor = new MeetPredictor(model);
            return predictor;
        }
        #endregion


        #region --Operations--
        private readonly MLEdgeModel model;

        private MeetPredictor (MLEdgeModel model) => this.model = model as MLEdgeModel;
        #endregion
    }
}