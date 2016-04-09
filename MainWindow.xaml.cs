using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimelordDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();

            Loaded += (o, e) =>
            {
                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

                this.Left = corner.X - this.ActualWidth;
                this.Top = corner.Y - this.ActualHeight;

                currentWork.Focus();
            };
        }

        private void submitHandler( object sender, ExecutedRoutedEventArgs args)
        {
            Console.WriteLine("Submitted");
            mainWindow.Background = new SolidColorBrush(Colors.Green);

            ColorAnimation animation = new ColorAnimation();
            animation.To = Colors.DimGray;
            animation.Duration = Duration.Automatic;

            mainWindow.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }
    }

    public static class Command
    {
        public static RoutedCommand SubmitCommand = new RoutedCommand();
    }

}
