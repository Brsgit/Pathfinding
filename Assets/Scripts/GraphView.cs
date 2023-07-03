using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Graph))]
public class GraphView : MonoBehaviour
{
    [SerializeField] private GameObject _nodeViewPrefab;

    public NodeView[,] nodeViews;

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

                Color originalColor = MapData.GetColorFromNodeType(n.NodeType);
                nodeView.ColorNode(originalColor);
            }
        }
    }

    public void ColorNodes(List<Node> nodes, Color color, bool lerpColor = false, float lerpValue = 0.5f)
    {
        foreach(Node n in nodes)
        {
            if (n != null)
            {
                NodeView nodeView = nodeViews[n.XIndex, n.YIndex];
                Color newColor = color;

                if (lerpColor)
                {
                    Color originalColor = MapData.GetColorFromNodeType(n.NodeType);
                    newColor = Color.Lerp(originalColor, newColor, lerpValue);
                }

                if(nodeView != null)
                {
                    nodeView.ColorNode(newColor);
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
