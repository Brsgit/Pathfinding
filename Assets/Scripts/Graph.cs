using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{

    private Node[,] _nodes;
    private List<Node> _walls = new List<Node>();

    public Node[,] Nodes => _nodes;

    public IEnumerable<Node> Walls => _walls;

    private int[,] m_mapData;
    private int m_width;
    private int m_height;

    public static readonly Vector2[] AllDirections =
    {
        new Vector2(0f, 1f),
        new Vector2(0f, -1f),
        new Vector2(1f, 0f),
        new Vector2(1f, 1f),
        new Vector2(1f, -1f),
        new Vector2(-1f, 0f),
        new Vector2(-1f, 1f),
        new Vector2(-1f, -1f),
    };

    public void Init(int[,] mapData)
    {
        m_mapData = mapData;
        m_width = mapData.GetLength(0);
        m_height = mapData.GetLength(1);

        _nodes = new Node[m_width, m_height];

        for (int y = 0; y < m_height; y++)
        {
            for (int x = 0; x < m_width; x++)
            {
                NodeType type = (NodeType)mapData[x, y];
                Node newNode = new Node(x, y, type);
                _nodes[x, y] = newNode;

                newNode.position = new Vector3(x, 0, y);

                if (type == NodeType.Blocked)
                {
                    _walls.Add(newNode);
                }
            }
        }

        for (int y = 0; y < m_height; y++)
        {
            for (int x = 0; x < m_width; x++)
            {
                if (_nodes[x, y].NodeType != NodeType.Blocked)
                {
                    _nodes[x, y].SetNeighbours(GetNeighbours(x, y));
                }
            }
        }

    }

    public bool IsWithinBounds(int x, int y)
    {
        return (x >= 0 && x < m_width && y > 0 && y < m_height);
    }

    private List<Node> GetNeighbours(int x, int y, Node[,] nodeArray, Vector2[] directions)
    {
        List<Node> neighbourNodes = new List<Node>();

        foreach (Vector2 dir in directions)
        {
            int newX = (int)dir.x;
            int newY = (int)dir.y;

            if (IsWithinBounds(newX, newY) && nodeArray[newX, newY] != null &&
                nodeArray[newX, newY].NodeType != NodeType.Blocked)
            {
                neighbourNodes.Add(nodeArray[newX, newY]);
            }
        }
        return neighbourNodes;
    }

    private List<Node> GetNeighbours(int x, int y)
    {
        return GetNeighbours(x, y, _nodes, AllDirections);
    }
}
