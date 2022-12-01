using Kitware.VTK;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BounceBall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        double moveX;
        double moveY;
        double limitX;
        double limitY;
        int speedX = 5;
        int speedY = 5;
        public MainWindow()
        {
            InitializeComponent();
            Random rand = new Random();
            myCanvas.Focus();
            moveX = rand.Next(-10, 10) / 10.0;
            moveY = rand.Next(-10, 10) / 10.0;

            myCanvas.Background = System.Windows.Media.Brushes.AliceBlue;

            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(10);
            gameTimer.Start();

        }
        private void GameTimerEvent(object sender, EventArgs e)
        {
            MoveBall();
        }
        private void MoveBall()
        {

            if (Canvas.GetLeft(myCircle) + moveX * speedX < 0)
            {
                Canvas.SetLeft(myCircle, (10 - Canvas.GetLeft(myCircle)));
                Canvas.SetTop(myCircle, Canvas.GetTop(myCircle) + moveY * speedY);
                speedX *= -1;
            }
            if (Canvas.GetLeft(myCircle) + moveX * speedX > limitX)
            {
                Canvas.SetLeft(myCircle, limitX - (Canvas.GetLeft(myCircle) - limitX));
                Canvas.SetTop(myCircle, Canvas.GetTop(myCircle) + moveY * speedY);
                speedX *= -1;
            }
            if (Canvas.GetTop(myCircle) + moveY * speedY < 0)
            {
                Canvas.SetLeft(myCircle, Canvas.GetLeft(myCircle) + moveX * speedX);
                Canvas.SetTop(myCircle, (10 - Canvas.GetTop(myCircle)));
                speedY *= -1;
            }
            if (Canvas.GetTop(myCircle) + moveY * speedY > limitY)
            {
                Canvas.SetLeft(myCircle, Canvas.GetLeft(myCircle) + moveX * speedX);
                Canvas.SetTop(myCircle, (limitY - (Canvas.GetTop(myCircle) - limitY)));
                speedY *= -1;
            }
            else
            {
                Canvas.SetLeft(myCircle, Canvas.GetLeft(myCircle) + moveX * speedX);
                Canvas.SetTop(myCircle, Canvas.GetTop(myCircle) + moveY * speedY);
            }
        }

        private void onLoad(object sender, RoutedEventArgs e)
        {
            limitX = myCanvas.ActualWidth - myCircle.ActualWidth;
            limitY = myCanvas.ActualHeight - myCircle.ActualHeight;

        }

        private void myCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            limitX = myCanvas.ActualWidth - myCircle.ActualWidth;
            limitY = myCanvas.ActualHeight - myCircle.ActualHeight;
        }

        private void sizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sizeBox.Text.Any() && int.TryParse(sizeBox.Text, out _))
            {
                int newSize = Convert.ToInt32(sizeBox.Text);
                if (newSize < myCanvas.ActualWidth && newSize < myCanvas.ActualHeight && newSize > 0)
                {
                    myCircle.Height = newSize;
                    myCircle.Width = newSize;
                    limitX = myCanvas.ActualWidth - myCircle.Width;
                    limitY = myCanvas.ActualHeight - myCircle.Height;
                    sizeBox.Clear();
                }
                else
                {
                    MessageBox.Show("Number is too large", "Invalid value");
                }

            }
            else
            {
                MessageBox.Show("Incorect input, only numeric values accepted", "Invalid value");
            }
        }

        private void speedBtn_Click(object sender, RoutedEventArgs e)
        {
            if (speedBox.Text.Any() && int.TryParse(speedBox.Text, out _))
            {
                int newSpeed = Convert.ToInt32(speedBox.Text);
                if (newSpeed > 0 && newSpeed <= 50)
                {
                    speedX = speedX >= 0 ? newSpeed : -newSpeed;
                    speedY = speedY >= 0 ? newSpeed : -newSpeed;
                    speedBox.Clear();
                }
                else
                {
                    MessageBox.Show("Enter a number between 1 and 50", "Invalid value");
                }

            }
            else
            {
                MessageBox.Show("Incorect input, only numeric values accepted", "Invalid value");
            }
        }
    }

}
