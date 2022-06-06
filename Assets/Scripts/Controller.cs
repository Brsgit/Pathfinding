using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private MapData _mapData;
    [SerializeField] private Graph _graph;

    private void Start()
    {
        if (_mapData != null && _graph != null)
        {
            int[,] mapInstance = _mapData.MakeMap();
            _graph.Init(mapInstance);

            GraphView graphView = _graph.GetComponent<GraphView>();

            if (graphView != null)
            {
                graphView.Init(_graph);
            }
        }
    }

}
