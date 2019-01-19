using System.Windows.Media;

namespace WPF_Metro_GUI.Style
{
    /// <summary>
    /// Interface for fontable UIElements
    /// </summary>
    public interface IFontable
    {
        /// <summary>
        /// Sets the font.
        /// </summary>
        /// <param name="f">The font family.</param>
        void SetFont(FontFamily f);
    }
}
