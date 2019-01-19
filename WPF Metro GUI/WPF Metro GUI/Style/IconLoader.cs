using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_Metro_GUI.Style
{
    /// <summary>
    /// Class for icon loading
    /// </summary>
    public static class IconLoader
    {
        #region Paths
        /// <summary>
        /// The configuration icon path
        /// </summary>
        public static readonly string ConfigurationIconPath = @"..\..\Resources\configIcon.png";
        /// <summary>
        /// The configuration highlighted icon path
        /// </summary>
        public static readonly string ConfigurationHighlightedIconPath = @"..\..\Resources\configHighlightedIcon.png";
        /// <summary>
        /// The crypto balance icon path
        /// </summary>
        public static readonly string CryptoBalanceIconPath = @"..\..\Resources\cryptoBalance.png";
        /// <summary>
        /// The crypto balance highlighted icon path
        /// </summary>
        public static readonly string CryptoBalanceHighlightedIconPath = @"..\..\Resources\cryptoBalanceHighlightedIcon.png";
        /// <summary>
        /// The graph icon path
        /// </summary>
        public static readonly string GraphIconPath = @"..\..\Resources\graphIcon.png";
        /// <summary>
        /// The graph icon path
        /// </summary>
        public static readonly string GraphHighlightedIconPath = @"..\..\Resources\graphHighlightedIcon.png";
        /// <summary>
        /// The outgoing data icon path
        /// </summary>
        public static readonly string OutgoingDataIconPath = @"..\..\Resources\outgoingData.png";
        /// <summary>
        /// The pin icon path
        /// </summary>
        public static readonly string PinIconPath = @"..\..\Resources\pinIcon.png";
        /// <summary>
        /// The quit icon path
        /// </summary>
        public static readonly string QuitIconPath = @"..\..\Resources\quitIcon.png";
        /// <summary>
        /// The automatic pilot icon path
        /// </summary>
        public static readonly string AutoPilotIconPath = @"..\..\Resources\autopilotIcon.png";
        /// <summary>
        /// The manual icon path
        /// </summary>
        public static readonly string ManualIconPath = @"..\..\Resources\manualIcon.png";
        /// <summary>
        /// The help icon path
        /// </summary>
        public static readonly string HelpIconPath = @"..\..\Resources\helpIcon.png";
        /// <summary>
        /// The bitcoin icon path
        /// </summary>
        public static readonly string BitcoinIconPath = @"..\..\Resources\bitcoinIcon.png";
        /// <summary>
        /// The divider icon path
        /// </summary>
        public static readonly string DividerIconPath = @"..\..\Resources\dividerIcon.png";
        /// <summary>
        /// The overiew icon path
        /// </summary>
        public static readonly string OveriewIconPath = @"..\..\Resources\overviewIcon.png";
        /// <summary>
        /// The overview highlight icon path
        /// </summary>
        public static readonly string OverviewHighlightIconPath = @"..\..\Resources\overviewHighlightedIcon.png";
        /// <summary>
        /// The adwisory icon
        /// </summary>
        public static readonly string AdwisoryIcon = @"..\..\Resources\adwisoryLabIcon.png";
        /// <summary>
        /// The quit hightlighted icon
        /// </summary>
        public static readonly string QuitHightlightedIcon = @"..\..\Resources\quitHighlightedIcon.png";
        #endregion

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <returns></returns>
        public static Image GetImage(string path, int w, int h) 
        {
            return new Image()
            {
                Source = Resize(new BitmapImage(new Uri(path, UriKind.Relative)), w, h, 0)
            };
        }

        /// <summary>
        /// Resizes the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="margin">The margin.</param>
        /// <returns></returns>
        public static BitmapSource Resize(BitmapSource source, int width, int height, double margin)
        {
            var rect = new Rect(margin, margin, width - margin * 2, height - margin * 2);

            var group = new DrawingGroup();
            RenderOptions.SetBitmapScalingMode(group, BitmapScalingMode.HighQuality);
            group.Children.Add(new ImageDrawing(source, rect));

            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
                drawingContext.DrawDrawing(group);

            var resizedImage = new RenderTargetBitmap(
                width, height,         // Resized dimensions
                96, 96,                // Default DPI values
                PixelFormats.Default); // Default pixel format
            resizedImage.Render(drawingVisual);

            return BitmapFrame.Create(resizedImage);
        }
    }
}
