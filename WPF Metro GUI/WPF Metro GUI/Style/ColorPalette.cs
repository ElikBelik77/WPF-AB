using System;
using System.Windows.Media;
namespace WPF_Metro_GUI.Style
{
    /// <summary>
    /// Class for application color palette.
    /// </summary>
    public class ColorPalette
    {
        #region Properties
        /// <summary>
        /// Gets or sets the color of the primary.
        /// </summary>
        /// <value>
        /// The color of the primary.
        /// </value>
        public Color PrimaryColor { get; set; }


        /// <summary>
        /// Gets or sets the color of the secondary.
        /// </summary>
        /// <value>
        /// The color of the secondary.
        /// </value>
        public Color SecondaryColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>
        /// The color of the text.
        /// </value>
        public Color TextColor { get; set; }
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPalette"/> class.
        /// </summary>
        /// <param name="primary">The primary.</param>
        /// <param name="secondary">The secondary.</param>
        /// <param name="text">The text.</param>
        public ColorPalette(Color primary, Color secondary, Color text)
        {
            PrimaryColor = primary;
            SecondaryColor = secondary;
            TextColor = text;
        }
        #endregion

        /// <summary>
        /// Dims this instance.
        /// </summary>
        /// <returns></returns>
        public ColorPalette Dim()
        {
            Color newPrimary = Color.FromArgb((byte)(Math.Max(0, PrimaryColor.A - 50)), PrimaryColor.R, PrimaryColor.G, PrimaryColor.B);
            Color newSecondary = Color.FromArgb((byte)(Math.Max(0, SecondaryColor.A - 50)), SecondaryColor.R, SecondaryColor.G, SecondaryColor.B);
            return new ColorPalette(newPrimary, newSecondary,TextColor);
        }

        /// <summary>
        /// Lightens this instance.
        /// </summary>
        /// <returns></returns>
        public ColorPalette Lighten()
        {
            Color newPrimary = Color.FromArgb((byte)(Math.Max(0, PrimaryColor.A + 50)), PrimaryColor.R, PrimaryColor.G, PrimaryColor.B);
            Color newSecondary = Color.FromArgb((byte)(Math.Max(0, SecondaryColor.A + 50)), SecondaryColor.R, SecondaryColor.G, SecondaryColor.B);
            return new ColorPalette(newPrimary, newSecondary, TextColor);
        }


        /// <summary>
        /// Gets the brush.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        public static SolidColorBrush GetBrush(Color c)
        {
            return new SolidColorBrush(c);
        }
    }
}
