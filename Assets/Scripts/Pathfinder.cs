using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Graph _graph;
    private GraphView _graphView;
    private Node _startNode;
    private Node _goalNode;

    private Queue<Node> _frontierNodes = new Queue<Node>();
    private List<Node> _visitedNodes = new List<Node>();
    private List<Node> _pathNodes = new List<Node>();

    [SerializeField] private Color _startNodeColor = Color.green;
    [SerializeField] private Color _goalNodeColor = Color.red;
    [SerializeField] private Color _frontierNodeColor = Color.yellow;
    [SerializeField] private Color _visitedNodeColor = Color.gray;

    public void Init(Graph graph, GraphView graphView, Node startNode, Node goalNode)
    {
        if (graph == null || graphView == null || startNode == null || goalNode == null)
        {
            Debug.LogWarning("PATHFINDER: Missing initialization references!");
            return;
        }

        if(startNode.NodeType == NodeType.Blocked || goalNode.NodeType == NodeType.Blocked)
        {
            string msg = startNode.NodeType == NodeType.Blocked ? "Start node " : "Goal node ";
            Debug.LogWarning("PATHFINDER: Start and goal nodes can't be blocked! " + msg + "blocked.");
            return;
        }

        _graph = graph;
        _graphView = graphView;
        _startNode = startNode;
        _goalNode = goalNode;

        NodeView startView = graphView.nodeViews[startNode.XIndex, startNode.YIndex];

        if(startView != null)
        {
            startView.ColorNode(_startNodeColor);
        }

        NodeView goalView = graphView.nodeViews[goalNode.XIndex, goalNode.YIndex];

        if (goalView != null)
        {
            goalView.ColorNode(_goalNodeColor);
        }

        _frontierNodes.Enqueue(startNode);

        for (int y = 0; y < graph.Height; y++)
        {
            for (int x = 0; x < graph.Width; x++)
            {
                _graph.Nodes[x, y].Reset();
            }
        }
    }

}
