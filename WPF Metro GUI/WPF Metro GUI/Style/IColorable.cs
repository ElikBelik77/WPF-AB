namespace WPF_Metro_GUI.Style
{
    /// <summary>
    /// Interface for themeable UIElements.
    /// </summary>
    public interface IColorable
    {
        /// <summary>
        /// Sets the theme.
        /// </summary>
        /// <param name="palette">The palette.</param>
        void SetTheme(ColorPalette palette);

        /// <summary>
        /// Gets the theme.
        /// </summary>
        /// <returns></returns>
        ColorPalette GetTheme();
    }
}
