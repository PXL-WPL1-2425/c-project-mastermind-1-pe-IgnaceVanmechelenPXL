using System.Diagnostics;
using System.Reflection.Emit;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace c_project_mastermind_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int randomIndex;
        private string randomColor;
        int attempts;
        string[] colors = { "Red", "Yellow", "Orange", "White", "Green", "Blue" };
        List<string> secretCode = new List<string>();
        private DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            Title = $"MasterMind - poging {attempts}";
            string randomColorOne = GenerateRandomColor();
            string randomColorTwo = GenerateRandomColor();
            string randomColorThree = GenerateRandomColor();
            string randomColorFour = GenerateRandomColor();

            secretCode.Add(randomColorOne);
            secretCode.Add(randomColorTwo);
            secretCode.Add(randomColorThree);
            secretCode.Add(randomColorFour);

            secretCodeTextBox.Text = string.Join(", ", secretCode);

            foreach (string color in colors)
            {
                comboBoxOne.Items.Add(color);
                comboBoxTwo.Items.Add(color);
                comboBoxThree.Items.Add(color);
                comboBoxFour.Items.Add(color);
            }
            StartCountdown();
        }
        public string GenerateRandomColor()
        {
            Random rnd = new Random();
            randomIndex = rnd.Next(colors.Length);
            randomColor = colors[randomIndex];
            return randomColor;
        }
        private void ComboBox_SelectionChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox != null && comboBox.SelectedItem != null)
            {
                string selectedColor = comboBox.SelectedItem.ToString();
                SolidColorBrush brush = GetBrushFromColorName(selectedColor);

                if (comboBox == comboBoxOne)
                {
                    labelOne.Content = selectedColor;
                    labelOne.Background = brush;
                }
                else if (comboBox == comboBoxTwo)
                {
                    labelTwo.Content = selectedColor;
                    labelTwo.Background = brush;
                }
                else if (comboBox == comboBoxThree)
                {
                    labelThree.Content = selectedColor;
                    labelThree.Background = brush;
                }
                else if (comboBox == comboBoxFour)
                {
                    labelFour.Content = selectedColor;
                    labelFour.Background = brush;
                }
            }
        }
        private SolidColorBrush GetBrushFromColorName(string colorName)
        {
            try
            {
                return (SolidColorBrush)new BrushConverter().ConvertFromString(colorName);
            }
            catch
            {
                return Brushes.Transparent;
            }
        }
        private void CheckCodeButton_Click(object sender, RoutedEventArgs e)
        {
            attempts++;
            Title = $"MasterMind - poging {attempts}";
            StartCountdown();
            List<string> userColors = new List<string>();
            userColors.Add(comboBoxOne.SelectedItem?.ToString());
            userColors.Add(comboBoxTwo.SelectedItem?.ToString());
            userColors.Add(comboBoxThree.SelectedItem?.ToString());
            userColors.Add(comboBoxFour.SelectedItem?.ToString());

            for (int i = 0; i < userColors.Count; i++)
            {
                if (userColors[i] == secretCode[i])
                {
                    SetLabelBorder(i, Colors.DarkRed);
                }
                else if (secretCode.Contains(userColors[i]))
                {
                    SetLabelBorder(i, Colors.Wheat);
                }
            }
        }
        private void SetLabelBorder(int index, Color borderColor)
        {
            SolidColorBrush brush = new SolidColorBrush(borderColor);
            switch (index)
            {
                case 0:
                    labelOne.BorderBrush = brush;
                    labelOne.BorderThickness = new Thickness(3);
                    break;
                case 1:
                    labelTwo.BorderBrush = brush;
                    labelTwo.BorderThickness = new Thickness(3);
                    break;
                case 2:
                    labelThree.BorderBrush = brush;
                    labelThree.BorderThickness = new Thickness(3);
                    break;
                case 3:
                    labelFour.BorderBrush = brush;
                    labelFour.BorderThickness = new Thickness(3);
                    break;
            }
        }
        private void ToggleDebug(object sender, KeyEventArgs e)
        {
            if(e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.F12)
            {
                secretCodeTextBox.Visibility = Visibility.Visible;
            }
        }
        private void StartCountdown()
        {
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            
        }
    }
}