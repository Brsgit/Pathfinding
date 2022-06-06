using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    [SerializeField] private GameObject _tile;

    [Range(0, 0.5f)]
    [SerializeField] private float _borderSize = 0.15f;

    public void Init(Node node)
    {
        if( _tile != null)
        {
            gameObject.name = "Node (" + node.XIndex + "," + node.YIndex + ")";
            gameObject.transform.position = node.position;
            _tile.transform.localScale = new Vector3(1f - _borderSize, 1f, 1f - _borderSize);
        }
    }

    private void ColorNode(Color color, GameObject go)
    {
        if(go != null)
        {
            Renderer goRenderer = go.GetComponent<Renderer>();

            if (goRenderer)
            {
                goRenderer.material.color = color;
            }
        }
    }

    public void ColorNode(Color color)
    {
        ColorNode(color, _tile);
    }
}
