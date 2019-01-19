using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF_Metro_GUI.Application_Blocks
{
    /// <summary>
    /// A menu for an application block.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.StackPanel" />
    public class ApplicationBlockMenu : StackPanel
    {
        #region Member Variables
        private double _ElementHeight, _ElementWidth;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationBlockMenu"/> class.
        /// </summary>
        /// <param name="elementHeight">Height of the element.</param>
        /// <param name="elementWidth">Width of the element.</param>
        public ApplicationBlockMenu(double elementHeight, double elementWidth)
        {
            this.Orientation = Orientation.Horizontal;
            this.HorizontalAlignment = HorizontalAlignment.Right;
            this._ElementHeight = elementHeight;
            this._ElementWidth = elementWidth;
            this.Children.Add(new Label() { Content = "" });
        }
        #endregion



        /// <summary>
        /// Adds an item to the menu.
        /// </summary>
        /// <param name="icon">The icon.</param>
        /// <param name="clickEvent">The click event.</param>
        public void AddItem(Image icon, MouseButtonEventHandler clickEvent)
        {
            Button newButton = new Button()
            {
                Background = new ImageBrush(icon.Source)
                {
                    Stretch = Stretch.None,
                },
                Style = Resources["MetroButton"] as Style,
                Content = "",
                Width = _ElementWidth,
                Height = _ElementHeight,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            if(clickEvent != null)
                newButton.PreviewMouseDown += clickEvent;
            this.Children.Add(newButton);
        }

    }
}
