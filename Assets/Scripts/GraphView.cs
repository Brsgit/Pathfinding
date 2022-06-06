using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Graph))]
public class GraphView : MonoBehaviour
{
    [SerializeField] private GameObject _nodeViewPrefab;

    [SerializeField] private Color _baseColor = Color.white;
    [SerializeField] private Color _wallColor = Color.black;

    public void Init(Graph graph)
    {
        if(graph == null)
        {
            Debug.LogWarning("NO GRAPH TO INIT!");
                return;
        }

        foreach(Node n in graph.Nodes)
        {
            var instance = Instantiate(_nodeViewPrefab, Vector3.zero, Quaternion.identity);
            NodeView nodeView = instance.GetComponent<NodeView>();

            if(nodeView != null)
            {
                nodeView.Init(n);

                if (n.NodeType == NodeType.Blocked)
                    nodeView.ColorNode(_wallColor);
                else
                    nodeView.ColorNode(_baseColor);
            }
        }
    }

}
