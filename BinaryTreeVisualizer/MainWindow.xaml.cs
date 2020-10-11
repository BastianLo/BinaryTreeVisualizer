using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BinaryTreeVisualizer
{
    public partial class MainWindow : Window
    {
        public Point MousePosPressed = new Point(0, 0);
        public Point MousePosPressedPreviously = new Point(0, 0);

        public Tree BinaryTree;



        public MainWindow()
        {
            InitializeComponent();

            BinaryTree = new Tree(new TreeNode(Height / 2, 100, 10), MainCanvas, 20);

        }

        private void MainWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {

                // ReSharper disable CompareOfFloatsByEqualityOperator
                if (MousePosPressedPreviously.X == 0 && MousePosPressedPreviously.Y == 0)
                    MousePosPressedPreviously = MousePosPressed;


                foreach (UIElement shape in MainCanvas.Children)
                {
                    Canvas.SetLeft(shape, (double)shape.GetValue(Canvas.LeftProperty) - MousePosPressedPreviously.X + MousePosPressed.X);
                    Canvas.SetTop(shape, (double)shape.GetValue(Canvas.TopProperty) - MousePosPressedPreviously.Y + MousePosPressed.Y);
                }

                MousePosPressedPreviously = MousePosPressed;
                MousePosPressed = e.GetPosition(MainCanvas);
            }
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MousePosPressedPreviously = new Point(0, 0);
            MousePosPressed = new Point(0, 0);
        }

        private void BtnAddNode_OnClick(object sender, RoutedEventArgs e)
        {


            try
            {
                BinaryTree.AddNode(Convert.ToDouble(TextBoxNumberInput.Text));
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Number");
            }

            BinaryTree.GetDepth();
        }

        private void TextBoxNumberInput_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,-]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void MainWindow_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                BinaryTree.TreeDensity += 10;
                BinaryTree.ReFitNodes();
            }
            if (e.Delta < 0)
            {
                BinaryTree.TreeDensity -= 10;
                BinaryTree.ReFitNodes();
            }
        }
    }
}
