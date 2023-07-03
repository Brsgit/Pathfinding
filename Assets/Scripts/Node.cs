using System.Collections.Generic;
using UnityEngine;
using System;

public enum NodeType
{
    Open = 0,
    Blocked = 1,
    LightTerrain = 2,
    MedTerrain = 3,
    HeavyTerrain = 4
}

public class Node : IComparable<Node>
{
    [SerializeField] private NodeType _nodeType = NodeType.Open;

    public NodeType NodeType => _nodeType;

    [SerializeField] private int _xIndex = -1;
    [SerializeField] private int _yIndex = -1;

    public int XIndex => _xIndex;

    public int YIndex => _yIndex;

    private float _distanceTraveled = Mathf.Infinity;

    public float DistanceTraveled
    {
        get { return _distanceTraveled; }
        set { _distanceTraveled = value; }
    }

    public Vector3 position;

    public List<Node> _neighbours = new List<Node>();

    public IEnumerable<Node> Neighbours => _neighbours;

    private Node _previous = null;

    public int priority;

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

    public int CompareTo(Node other)
    {
        if (priority < other.priority) return -1;
        else if (priority > other.priority) return 1;
        else return 0;
    }
}
