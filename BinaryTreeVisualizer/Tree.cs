using System.Collections.Generic;
using System.Windows.Controls;

namespace BinaryTreeVisualizer
{
    public class Tree
    {
        public readonly TreeNode Root;
        private Canvas drawCanvas;
        private int _treeDensity;

        public int TreeDensity
        {
            get
            {
                return _treeDensity;
            }
            set
            {
                _treeDensity = TreeNode.Clamp(value, 20, 200);
            }
        }


        public Tree(TreeNode root, Canvas drawCanvas, int TreeDensity)
        {
            Root = root;
            this.drawCanvas = drawCanvas;
            drawCanvas.Children.Add(Root);
            this.TreeDensity = TreeDensity;
        }

        public void AddNode(double value)
        {
            var node = new TreeNode(value: value);
            Root.Append(node);
            drawCanvas.Children.Add(node);
            ReFitNodes();
        }

        public int GetDepth()
        {
            return Root.GetDepth();
        }

        public int SearchNodeDepth(TreeNode node)
        {
            return Root.SearchNodeDepth(node);
        }

        public IEnumerable<TreeNode> TraversePreOrder()
        {
            return Root.TraversePreOrder(Root);
        }

        public void ReFitNodes()
        {
            Root.ReFitNodes(GetDepth() - 1, TreeDensity);
        }
    }
}