using WPF_Metro_GUI.Application_Blocks;
using WPF_Metro_GUI.Style;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace WPF_Metro_GUI
{
    /// <summary>
    /// Class managing the whole api application.
    /// </summary>
    public class ApplicationManager : IDisposable
    {
        #region Member Variables
        private MainWindow _ChildWindow;
        private FontFamily _Font;
        private List<Tab> _Tabs;
        private Tab _SelectedTab;
        private ColorPalette _ApplicationTheme;
        private ObserverSocketClient _Client;
        private System.Timers.Timer _UpdateTimer;
        private bool _Locked = false;
        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the child window.
        /// </summary>
        /// <value>
        /// The child window.
        /// </value>
        public MainWindow ChildWindow
        {
            get { return _ChildWindow; }
            set { _ChildWindow = value; }
        }

        /// <summary>
        /// Gets or sets the tabs.
        /// </summary>
        /// <value>
        /// The tabs.
        /// </value>
        public List<Tab> Tabs
        {
            get { return _Tabs; }
            set { _Tabs = value; }
        }

        /// <summary>
        /// Gets or sets the selected tab.
        /// </summary>
        /// <value>
        /// The selected tab.
        /// </value>
        public Tab SelectedTab
        {
            get { return _SelectedTab; }
            set
            {
                if (_Tabs.Contains(value))
                    _SelectedTab = value;
            }
        }
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationManager"/> class.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="client">The client.</param>
        public ApplicationManager(MainWindow window, ObserverSocketClient client)
        {
            _ApplicationTheme = new ColorPalette(
                Color.FromArgb(255,66, 66, 80), Color.FromArgb(255, 0, 125, 81), Color.FromArgb(255, 255, 255, 255));
            _Client = client;
            ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(Int32.MaxValue));
            this._Font = new FontFamily("Caviar Dreams");
            this._ChildWindow = window;
            Image iconImage = IconLoader.GetImage(IconLoader.AdwisoryIcon, 50, 33);
            iconImage.HorizontalAlignment = HorizontalAlignment.Center;
            iconImage.VerticalAlignment = VerticalAlignment.Center;
            iconImage.Width = 50;
            iconImage.Height = 33;
            iconImage.Margin = new Thickness(0, 10, 5, 10);
            this._ChildWindow._TabBar.Children.Add(iconImage);
            int primaryRowCount = _ChildWindow._PrimaryBlockGrid.RowDefinitions.Count == 0 ? 1 : _ChildWindow._PrimaryBlockGrid.RowDefinitions.Count;
            int primaryColumnCount = _ChildWindow._PrimaryBlockGrid.ColumnDefinitions.Count == 0 ? 1 : _ChildWindow._PrimaryBlockGrid.ColumnDefinitions.Count;

            int primaryCount = primaryRowCount * primaryColumnCount;

            this._Tabs = new List<Tab>
            {
            };

            foreach (Tab t in _Tabs)
            {
                this._ChildWindow._TabBar.Children.Add(t);
                t.SetTheme(_ApplicationTheme);
                t.OnClick += T_OnClick;
            }

            this._SelectedTab = _Tabs[0];
            this._SelectedTab.SetSelected();
            _Tabs[0].ShowChildren(_ChildWindow._PrimaryBlockGrid);
            
            _ChildWindow._MainGrid.Background = ColorPalette.GetBrush(_ApplicationTheme.PrimaryColor);
            _UpdateTimer = new System.Timers.Timer(4000);
            _UpdateTimer.Elapsed += _UpdateTimer_Elapsed;
            // _UpdateTimer.Start();
        }
        #endregion

        
        private void _UpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!_Locked)
            {
                _Locked = true;


                //(sender as System.Timers.Timer).Stop();
                foreach (Tab t in _Tabs)
                {
                    t.Dispatcher.Invoke(() => {  
                    foreach (ApplicationBlock b in t.PrimaryBlocks)
                    {
                        System.Threading.Thread.Sleep(1);
                        b.UpdateData();
                    }
                });

                }
                _Locked = false;
            }
        }

        #region User Interaction Logic

        private void T_OnClick(Tab t)
        {
            this._SelectedTab.SetUnselected();
            this._SelectedTab = t;
            this._SelectedTab.SetSelected();
            _ChildWindow._PrimaryBlockGrid.Children.Clear();
            this._SelectedTab.ShowChildren(_ChildWindow._PrimaryBlockGrid);
        }

        #endregion

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            _Client.StartConnecting();
        }

        #region IDisposable Functions        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _UpdateTimer.Dispose();
        }
        #endregion
    }
}
