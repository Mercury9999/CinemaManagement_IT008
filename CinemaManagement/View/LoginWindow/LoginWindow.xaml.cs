using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CinemaManagement.View.LoginWindow
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private int imageIndex = 0;
        private List<BitmapImage> listImages = new List<BitmapImage>();
        private DispatcherTimer time;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string imageRelativePath = @"..\..\..\Resource\Poster";
            string imageAbsolutePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageRelativePath);

            if (Directory.Exists(imageAbsolutePath))
            {
                // Nạp ảnh vào bộ nhớ
                string[] imageFiles = Directory.GetFiles(imageAbsolutePath, "*.jpg");
                foreach (var path in imageFiles)
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    listImages.Add(bitmap);
                }

                if (listImages.Count > 0)
                {
                    imgPosterBackground.Source = listImages[0];
                    time = new DispatcherTimer()
                    {
                        Interval = TimeSpan.FromSeconds(2)
                    };
                    time.Tick += Time_Tick;
                    time.Start();
                }
            }
        }

        private void Time_Tick(object? sender, EventArgs e)
        {
            imageIndex = (imageIndex + 1) % listImages.Count;
            imgPosterBackground.Source = listImages[imageIndex];
        }
    }
}