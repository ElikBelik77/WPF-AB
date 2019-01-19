using System.Windows;
using System.Windows.Input;

namespace WPF_Metro_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Member Variables
        ApplicationManager _Manager;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();
            this.WindowStyle = WindowStyle.None;
            this.Style = (System.Windows.Style)FindResource(typeof(Window));
//            _Manager = new ApplicationManager(this, new CryptoArbitrager.API.Clients.Observer_Client.ObserverSocketClient("ABC", "DEF"));

        }

        #endregion


        #region User Interaction Logic
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _Manager.Initialize();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

    } 
}
