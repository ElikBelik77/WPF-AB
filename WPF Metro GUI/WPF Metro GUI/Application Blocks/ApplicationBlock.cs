
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json.Linq;
using WPF_Metro_GUI.Networking;
using WPF_Metro_GUI.Style;

namespace WPF_Metro_GUI.Application_Blocks
{
    /// <summary>
    /// Class representing a stand alone graphic block of the application
    /// </summary>
    /// <seealso cref="System.Windows.Controls.DockPanel" />
    /// <seealso cref="WPF_Metro_GUI.IColorable" />
    /// <seealso cref="WPF_Metro_GUI.IFontable" />
    public abstract class ApplicationBlock : DockPanel, IColorable, IFontable, INotifiable
    {
        #region Member Variables
        private TextBlock _TitleLabel;
        /// <summary>
        /// The client that the block uses for updates.
        /// </summary>
        protected IDataClient _Client;

        /// <summary>
        /// The current theme of the block.
        /// </summary>
        protected ColorPalette _CurrentTheme;

        /// <summary>
        /// The block's menu.
        /// </summary>
        protected ApplicationBlockMenu _BlockMenu;

        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the title label.
        /// </summary>
        /// <value>
        /// The title label.
        /// </value>
        public TextBlock TitleLabel
        {
            get { return _TitleLabel; }
            set { _TitleLabel = value; }
        }
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationBlock"/> class.
        /// </summary>
        public ApplicationBlock()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationBlock"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="title">The title.</param>
        /// <param name="icon">The icon.</param>
        public ApplicationBlock(IDataClient client, string title, Image icon)
        {
            _Client = client;
            this.OnHelpButtonClick = new MouseButtonEventHandler(this.ShowHelp);
            //_BlockMenu = new ApplicationBlockMenu(20, 20);
            //_BlockMenu.AddItem(IconLoader.GetImage(IconLoader.PinIconPath, 20, 20), null);
            //_BlockMenu.AddItem(IconLoader.GetImage(IconLoader.HelpIconPath, 20, 20), this.OnHelpButtonClick);
            //this.Children.Add(_BlockMenu);
            this.TitleLabel = new TextBlock()
            {
                Text = title,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                TextWrapping = System.Windows.TextWrapping.WrapWithOverflow
            };
            DockPanel.SetDock(this.TitleLabel, Dock.Top);
            //icon.Stretch = System.Windows.Media.Stretch.None;
            //icon.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            //icon.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            TitleLabel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            TitleLabel.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            this.SetValue(HeightProperty, Double.NaN);
            this.SetValue(WidthProperty, Double.NaN);
            // this.Children.Add(icon);
            this.Children.Add(TitleLabel);
        }

        #endregion

        #region IFontable Functions        
        /// <summary>
        /// Sets the font.
        /// </summary>
        /// <param name="f">The font family.</param>
        public virtual void SetFont(FontFamily f)
        {
            this.TitleLabel.FontFamily = f;
            this.TitleLabel.FontSize = 18;
        }
        #endregion

        #region User Interaction Logic        
        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseEnter" /> attached event is raised on this element. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.SetTheme(_CurrentTheme.Dim());
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseLeave" /> attached event is raised on this element. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.SetTheme(_CurrentTheme.Lighten());
        }
        #endregion

        #region IColorable Functions

        /// <summary>
        /// Gets the theme.
        /// </summary>
        /// <returns></returns>
        public ColorPalette GetTheme()
        {
            return _CurrentTheme;
        }


        /// <summary>
        /// Sets the theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        public virtual void SetTheme(ColorPalette theme)
        {
            this._CurrentTheme = theme;
            
            _TitleLabel.Background = ColorPalette.GetBrush(theme.PrimaryColor);
            _TitleLabel.Foreground = ColorPalette.GetBrush(theme.TextColor);
        }
        #endregion

        #region Abstract

        /// <summary>
        /// Shows the help.
        /// </summary>
        public abstract void ShowHelp();

        /// <summary>
        /// Updates the data.
        /// </summary>
        public abstract void UpdateData();
        #endregion

        private MouseButtonEventHandler OnHelpButtonClick;

        private void ShowHelp(object sender, MouseEventArgs args)
        {
            ShowHelp();
        }

        public virtual void Notify(JObject response, string id)
        {

        }
    }
}
