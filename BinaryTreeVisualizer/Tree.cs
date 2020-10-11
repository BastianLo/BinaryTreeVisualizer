using System.Collections.Generic;
using System.Windows.Controls;

namespace BinaryTreeVisualizer
{
    public class Tree
    {
        private TreeNode _root;
        public TreeNode Root
        {
            get
                => _root;
            set
            {
                if (_root == null)
                {
                    _root = value;
                }

                if (_root != null)
                {
                    drawCanvas.Children.Add(_root);
                }
            }
        }
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


        public Tree(Canvas drawCanvas, int TreeDensity, TreeNode root = null)
        {
            Root = root;
            this.drawCanvas = drawCanvas;
            this.TreeDensity = TreeDensity;
        }

        public void AddNode(double value)
        {
            var node = new TreeNode(ref drawCanvas, value: value);
            Root?.Append(node);
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
            return Root?.TraversePreOrder(Root);
        }

        public void ReFitNodes()
        {
            Root?.ReFitNodes(GetDepth() - 1, TreeDensity);
            ReconnectNodes();
        }

        public void ReconnectNodes()
        {
            Root?.ReconnectNode(ref drawCanvas);
        }
    }
}