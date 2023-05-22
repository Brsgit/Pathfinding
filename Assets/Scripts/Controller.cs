using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private MapData _mapData;
    [SerializeField] private Graph _graph;

    [SerializeField] private Pathfinder _pathfinder;
    [SerializeField] private int _startX = 0;
    [SerializeField] private int _startY = 0;
    [SerializeField] private int _goalX = 15;
    [SerializeField] private int _goalY = 9;

    [SerializeField] public float timeStep = 0.1f;

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

                Camera.main.transform.position = new Vector3((_mapData.Width - 1) / 2f, 20f, (_mapData.Height - 1) / 2f);
                Camera.main.orthographicSize = _mapData.Height / 2f;
            }

            if (_graph.IsWithinBounds(_startX, _startY) && _graph.IsWithinBounds(_goalX, _goalY) && _pathfinder != null)
            {
                Node startNode = _graph.Nodes[_startX, _startY];
                Node goalNode = _graph.Nodes[_goalX, _goalY];
                _pathfinder.Init(_graph, graphView, startNode, goalNode);
                StartCoroutine(_pathfinder.SearchRoutine(timeStep));
            }
        }
    }

}
