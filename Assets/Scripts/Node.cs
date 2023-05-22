using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    Open = 0,
    Blocked = 1
}

public class Node
{
    [SerializeField] private NodeType _nodeType = NodeType.Open;

    public NodeType NodeType => _nodeType;

    [SerializeField] private int _xIndex = -1;
    [SerializeField] private int _yIndex = -1;

    public int XIndex => _xIndex;

    public int YIndex => _yIndex;

    public Vector3 position;

    public List<Node> _neighbours = new List<Node>();

    public IEnumerable<Node> Neighbours => _neighbours;

    private Node _previous = null;

    public Node Previous { 
        get { return _previous; }
        set { _previous = value; }
    }

    public Node(int xIndex, int yIndex, NodeType nodeType)
    {
        this._xIndex = xIndex;
        this._yIndex = yIndex;
        this._nodeType = nodeType;
    }

    public void Reset()
    {
        _previous = null;
    }

    public void SetNeighbours(List<Node> neighbours)
    {
        _neighbours = neighbours;
    }

}
