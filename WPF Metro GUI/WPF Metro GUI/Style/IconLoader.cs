using System;
using System.Collections.Generic;
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
        #region Resource Dictionary
        public static Dictionary<string, string> Icons = new Dictionary<string, string>();
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
                Source = Resize(new BitmapImage(new Uri(Icons[path], UriKind.Relative)), w, h, 0)
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
