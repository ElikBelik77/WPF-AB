using WPF_Metro_GUI.Application_Blocks;
using WPF_Metro_GUI.Style;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace WPF_Metro_GUI.Application_Blocks
{
    /// <summary>
    /// Class representing a tab control.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.StackPanel" />
    /// <seealso cref="WPF_Metro_GUI.IColorable" />
    public abstract class Tab : StackPanel, IColorable
    {
        #region Member Variables
        private Button _TabButton;
        private ColorPalette _CurrentTheme;
        private Image _UnselectedIcon, _SelectedIcon;
        private Label _DescriptionLabel;

        /// <summary>
        /// The primary blocks that are linked to the tab.
        /// </summary>
        protected List<ApplicationBlock> _PrimaryLinkedBlocks;
        #endregion

        #region Properties        
        /// <summary>
        /// Gets the primary blocks.
        /// </summary>
        /// <value>
        /// The primary blocks.
        /// </value>
        public List<ApplicationBlock> PrimaryBlocks
        {
            get { return _PrimaryLinkedBlocks; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets the unselected icon.
        /// </summary>
        /// <value>
        /// The unselected icon.
        /// </value>
        public Image UnselectedIcon
        {
            get => this._UnselectedIcon;
        }

        /// <summary>
        /// Gets the selected icon.
        /// </summary>
        /// <value>
        /// The selected icon.
        /// </value>
        public Image SelectedIcon
        {
            get => this._SelectedIcon;
        }
        #endregion

        #region Events        
        /// <summary>
        /// Event handler for tab click event.
        /// </summary>
        /// <param name="t">The clicked tab.</param>
        public delegate void OnClickEventHandler(Tab t);

        /// <summary>
        /// Occurs when [on click] the tab.
        /// </summary>
        public event OnClickEventHandler OnClick;

        /// <summary>
        /// Raises the on click.
        /// </summary>
        public virtual void RaiseOnClick()
        {
            OnClick?.Invoke(this);
        }
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="Tab"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="unselectedIcon">The unselected icon.</param>
        /// <param name="selectedIcon">The selected icon.</param>
        public Tab(string title, Image unselectedIcon, Image selectedIcon) : base()
        {
            this._PrimaryLinkedBlocks = new List<ApplicationBlock>();

            this._UnselectedIcon = unselectedIcon;
            this._SelectedIcon = selectedIcon;
            this._TabButton = new Button()
            {
                Style = App.Current.TryFindResource("ButtonStyleNoHighlighting") as Style,
                Background = new ImageBrush(unselectedIcon.Source),
                Content = "",
                Width = 50,
                Height = 50,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            _DescriptionLabel = new Label()
            {
                Content = title,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            this.Children.Add(_TabButton);
            this.Children.Add(_DescriptionLabel);
            this.Orientation = Orientation.Vertical;
            _TabButton.BorderThickness = new Thickness(0);
            
            
            //this.Orientation = Orientation.Horizontal;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            this._TabButton.PreviewMouseDown += _TabButton_PreviewMouseDown;
            this.Margin = new Thickness(0, 10, 0, 10);
        }
        #endregion

        #region Theme Methods        
        /// <summary>
        /// Sets the theme.
        /// </summary>
        /// <param name="palette">The palette.</param>
        public virtual void SetTheme(ColorPalette palette)
        {
            foreach(ApplicationBlock block in this._PrimaryLinkedBlocks)
            {
                block.SetTheme(palette);
            }
            this._CurrentTheme = palette;
            this.Background = ColorPalette.GetBrush(palette.PrimaryColor); 
            this._TabButton.Foreground = ColorPalette.GetBrush(palette.TextColor);
            
        }

        /// <summary>
        /// Gets the theme.
        /// </summary>
        /// <returns>returns the current color theme of the control.</returns>
        public ColorPalette GetTheme()
        {
            return this._CurrentTheme;
        }
        #endregion

        #region Styling Methods
        /// <summary>
        /// Sets the style of the tab to the selected style.
        /// </summary>
        public void SetSelected()
        {
            this.IsSelected = true;
            this._DescriptionLabel.Foreground = ColorPalette.GetBrush(_CurrentTheme.TextColor);
            SetIcon(_SelectedIcon);
            ShowBorder();
        }

        /// <summary>
        /// Sets the style of the tab to unselected style.
        /// </summary>
        public void SetUnselected()
        {
            this.IsSelected = false;
            SetIcon(_UnselectedIcon);
            this._DescriptionLabel.Foreground = ColorPalette.GetBrush(Colors.Black);
            if (!IsMouseOver)
                HideBorder();
        }

        private void SetIcon(Image icon)
        {
            this._TabButton.Background = new ImageBrush(icon.Source);
        }

        private void ShowBorder()
        {
            this._TabButton.Effect = new DropShadowEffect()
            {
                Color = Colors.Black,
                Direction = 225,
                ShadowDepth = 10,
                BlurRadius = 10,
            };
            //this._DescriptionLabel.Effect = new DropShadowEffect()
            //{
            //    Color = Colors.Black,
            //    Direction = 225,
            //    ShadowDepth = 10,
            //    BlurRadius = 10,
            //};
        }

        private  void HideBorder()
        {
            this._TabButton.ClearValue(EffectProperty);
            //this._DescriptionLabel.ClearValue(EffectProperty);
        }
        #endregion

        #region User Interaction Logic

        /// <summary>
        /// Handles the PreviewMouseDown event of the _TabButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void _TabButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            RaiseOnClick();
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseEnter" /> attached event is raised on this element. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            //SetTheme(this._CurrentTheme.Dim());
            this._DescriptionLabel.Foreground = ColorPalette.GetBrush(_CurrentTheme.TextColor);
            SetIcon(this._SelectedIcon);
            ShowBorder();

        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseLeave" /> attached event is raised on this element. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (!IsSelected)
            {
                //SetTheme(this._CurrentTheme.Lighten());
                this._DescriptionLabel.Foreground = ColorPalette.GetBrush(Colors.Black);
                SetIcon(_UnselectedIcon);
                HideBorder();
            }
        }

        #endregion

        /// <summary>
        /// Shows the children of the tab on the grid.
        /// </summary>
        /// <param name="primaryGrid">The primary grid.</param>
        public void ShowChildren(Grid primaryGrid)
        {
            Dispatcher.Invoke(() =>
            {
                int counter = 0;
                primaryGrid.RowDefinitions.Clear();
                primaryGrid.ColumnDefinitions.Clear();
                PrepareGrid(primaryGrid);
                int primaryColAmount = primaryGrid.ColumnDefinitions.Count == 0 ? 1 : primaryGrid.ColumnDefinitions.Count + 1;
                foreach (ApplicationBlock block in _PrimaryLinkedBlocks)
                {
                    primaryGrid.Children.Add(block);
                    Grid.SetColumn(block, counter % primaryColAmount);
                    Grid.SetRow(block, counter / primaryColAmount);
                    counter++;
                }
                counter = 0;
            });
            
        }

        /// <summary>
        /// Prepares the grid for displaying the tab's children.
        /// </summary>
        /// <param name="primaryGrid">The primary grid.</param>
        public abstract void PrepareGrid(Grid primaryGrid);
    }
}
