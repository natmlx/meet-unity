/* 
*   Meet
*   Copyright © 2023 NatML Inc. All Rights Reserved.
*/

namespace NatML.Vision {

    using System;
    using NatML.Features;
    using NatML.Internal;
    using NatML.Types;

    /// <summary>
    /// Meet segmentation from MediaPipe.
    /// </summary>
    public sealed partial class MeetPredictor : IMLPredictor<MeetPredictor.Matte> {

        #region --Client API--
        /// <summary>
        /// Create the meet predictor.
        /// </summary>
        /// <param name="model">Meet model.</param>
        public MeetPredictor (MLEdgeModel model) => this.model = model as MLEdgeModel;

        /// <summary>
        /// Segment a person in an image.
        /// </summary>
        /// <param name="inputs">Input image.</param>
        /// <returns>Human matte.</returns>
        public Matte Predict (params MLFeature[] inputs) {
            // Check
            if (inputs.Length != 1)
                throw new ArgumentException(@"Meet predictor expects a single feature", nameof(inputs));
            // Check type
            var input = inputs[0];
            if (!MLImageType.FromType(input.type))
                throw new ArgumentException(@"Meet predictor expects an an array or image feature", nameof(inputs));
            // Predict
            using var inputFeature = (input as IMLEdgeFeature).Create(model.inputs[0]);
            using var outputFeatures = model.Predict(inputFeature);
            // Marshal
            var matte = new MLArrayFeature<float>(outputFeatures[0]);
            var result = new Matte(matte.shape[2], matte.shape[1], matte.ToArray());
            // Return
            return result;
        }
        #endregion


        #region --Operations--
        private readonly MLEdgeModel model;

        void IDisposable.Dispose () { }
        #endregion
    }
}