using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private bool _isComplete = false;
    private int _iterations = 0;

    public void Init(Graph graph, GraphView graphView, Node startNode, Node goalNode)
    {
        if (graph == null || graphView == null || startNode == null || goalNode == null)
        {
            Debug.LogWarning("PATHFINDER: Missing initialization references!");
            return;
        }

        if (startNode.NodeType == NodeType.Blocked || goalNode.NodeType == NodeType.Blocked)
        {
            string msg = startNode.NodeType == NodeType.Blocked ? "Start node " : "Goal node ";
            Debug.LogWarning("PATHFINDER: Start and goal nodes can't be blocked! " + msg + "blocked.");
            return;
        }

        _graph = graph;
        _graphView = graphView;
        _startNode = startNode;
        _goalNode = goalNode;

        ShowColors(graphView, startNode, goalNode);

        _frontierNodes.Enqueue(startNode);

        for (int y = 0; y < graph.Height; y++)
        {
            for (int x = 0; x < graph.Width; x++)
            {
                _graph.Nodes[x, y].Reset();
            }
        }

        _isComplete = false;
        _iterations = 0;
    }

    private void ShowColors()
    {
        ShowColors(_graphView, _startNode, _goalNode);
    }

    private void ShowColors(GraphView graphView, Node startNode, Node goalNode)
    {
        if (graphView == null || startNode == null || goalNode == null)
            return;

        if (_frontierNodes != null)
        {
            graphView.ColorNodes(_frontierNodes.ToList(), _frontierNodeColor);
        }

        if (_visitedNodes != null)
        {
            graphView.ColorNodes(_visitedNodes, _visitedNodeColor);
        }


        NodeView startView = graphView.nodeViews[startNode.XIndex, startNode.YIndex];

        if (startView != null)
        {
            startView.ColorNode(_startNodeColor);
        }

        NodeView goalView = graphView.nodeViews[goalNode.XIndex, goalNode.YIndex];

        if (goalView != null)
        {
            goalView.ColorNode(_goalNodeColor);
        }
    }

    public IEnumerator SearchRoutine(float timeStep = 0.1f)
    {
        yield return null;

        while (!_isComplete)
        {
            if (_frontierNodes.Count > 0)
            {
                Node currentNode = _frontierNodes.Dequeue();
                _iterations++;

                if (!_visitedNodes.Contains(currentNode))
                {
                    _visitedNodes.Add(currentNode);
                }

                ExpandFurther(currentNode);
                ShowColors();

                yield return new WaitForSeconds(timeStep);
            }
            else
            {
                _isComplete = true;
            }
        }
    }

    private void ExpandFurther(Node node)
    {
        if(node != null)
        {
            for(int i = 0; i < node._neighbours.Count(); i++)
            {
                if(!_visitedNodes.Contains(node._neighbours[i]) 
                    && !_frontierNodes.Contains(node._neighbours[i]))
                {
                    node._neighbours[i].Previous = node;
                    _frontierNodes.Enqueue(node._neighbours[i]);
                }
            }
        }
    }
}
