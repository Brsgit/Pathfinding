using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Graph))]
public class GraphView : MonoBehaviour
{
    [SerializeField] private GameObject _nodeViewPrefab;

    public NodeView[,] nodeViews;

    [SerializeField] private Color _baseColor = Color.white;
    [SerializeField] private Color _wallColor = Color.black;

    public void Init(Graph graph)
    {
        if(graph == null)
        {
            Debug.LogWarning("NO GRAPH TO INIT!");
                return;
        }

        nodeViews = new NodeView[graph.Width, graph.Height];

        foreach(Node n in graph.Nodes)
        {
            var instance = Instantiate(_nodeViewPrefab, Vector3.zero, Quaternion.identity);
            NodeView nodeView = instance.GetComponent<NodeView>();

            if(nodeView != null)
            {
                nodeView.Init(n);
                nodeViews[n.XIndex, n.YIndex] = nodeView;

                if (n.NodeType == NodeType.Blocked)
                    nodeView.ColorNode(_wallColor);
                else
                    nodeView.ColorNode(_baseColor);
            }
        }
    }

    public void ColorNodes(List<Node> nodes, Color color)
    {
        foreach(Node n in nodes)
        {
            if (n != null)
            {
                NodeView nodeView = nodeViews[n.XIndex, n.YIndex];

                if(nodeView != null)
                {
                    nodeView.ColorNode(color);
                }
            }
        }
    }

    public void ShowNodeArrows(Node node, Color color)
    {
        if (node != null)
        {
            NodeView nodeView = nodeViews[node.XIndex, node.YIndex];
            if (nodeView)
            {
                nodeView.ShowArrow(color);
            }
        }
    }

    public void ShowNodeArrows(List<Node> list, Color color)
    {
        foreach(var n in list)
        {
            ShowNodeArrows(n, color);
        }
    }

}
