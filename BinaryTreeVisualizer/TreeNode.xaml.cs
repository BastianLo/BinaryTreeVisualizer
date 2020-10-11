using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BinaryTreeVisualizer
{
    /// <summary>
    /// Interaktionslogik für TreeNode.xaml
    /// </summary>
    public partial class TreeNode : UserControl
    {
        public double Value;

        public TreeNode ChildLeft;
        public TreeNode ChildRight;

        public TreeNode(double posX = 0, double posY = 0, double value = 0)
        {
            InitializeComponent();
            Canvas.SetLeft(this, posX);
            Canvas.SetTop(this, posY);
            Value = value;
            TxtBox.Text = value.ToString();
        }

        public static int Clamp(int value, int min, int max) => (value < min) ? min : (value > max) ? max : value;

        public int GetDepth()
        {
            var leftDepth = 0;
            var rightDepth = 0;


            if (ChildLeft != null)
            {
                leftDepth = ChildLeft.GetDepth();
            }

            if (ChildRight != null)
            {
                rightDepth = ChildRight.GetDepth();
            }


            return Math.Max(leftDepth, rightDepth) + 1;
        }

        public int SearchNodeDepth(TreeNode node)
        {
            var leftDepth = 0;
            var rightDepth = 0;

            if (this == node)
                return 1;
            /*
            if (ChildRight == node)
                return 1;
            if (ChildLeft == node)
                return 1;*/

            if (ChildLeft != null)
            {
                leftDepth = ChildLeft.SearchNodeDepth(node);
            }

            if (ChildRight != null)
            {
                rightDepth = ChildRight.SearchNodeDepth(node);
            }

            if (leftDepth > 0)
                return leftDepth + 1;
            if (rightDepth > 0)
                return rightDepth + 1;
            return 0;
            //return Math.Max(leftDepth, rightDepth) + 1;

        }

        public void ReFitNodes(int exp, int density = 100)
        {
            ChildLeft?.SetPosition(GetX() - density * Math.Pow(2, exp), GetY() + 100);
            ChildRight?.SetPosition(GetX() + density * Math.Pow(2, exp), GetY() + 100);
            ChildLeft?.ReFitNodes(Clamp(exp - 1, 0, 100), density);
            ChildRight?.ReFitNodes(Clamp(exp - 1, 0, 100), density);
        }

        public IEnumerable<TreeNode> TraversePreOrder(TreeNode node)
        {

            yield return node;
            if (node.ChildLeft != null)
            {
                foreach (var c in TraversePreOrder(node.ChildLeft))
                {
                    yield return c;
                }
            }
            if (node.ChildRight != null)
            {
                foreach (var c in TraversePreOrder(node.ChildRight))
                {
                    yield return c;
                }
            }
        }

        public void Append(TreeNode node)
        {
            if (node.Value < Value)
            {
                if (ChildLeft == null)
                {
                    ChildLeft = node;
                    node.SetPosition(GetX() - 100, GetY() + 100);
                }
                else
                    ChildLeft.Append(node);
            }
            if (node.Value > Value)
            {
                if (ChildRight == null)
                {
                    ChildRight = node;
                    node.SetPosition(GetX() + 100, GetY() + 100);
                }
                else
                    ChildRight.Append(node);
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (node.Value == Value)
            {
                throw new InvalidOperationException("Cant add The same value twice");
            }
        }

        public void SetPosition(double x, double y)
        {
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

        public double GetX() => (double)this.GetValue(Canvas.LeftProperty);
        public double GetY() => (double)this.GetValue(Canvas.TopProperty);
    }
}
