using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    [SerializeField] private GameObject _tile;
    [SerializeField] private GameObject _arrow;

    private Node _node;

    [Range(0, 0.5f)]
    [SerializeField] private float _borderSize = 0.15f;

    public void Init(Node node)
    {
        if( _tile != null)
        {
            gameObject.name = "Node (" + node.XIndex + "," + node.YIndex + ")";
            gameObject.transform.position = node.position;
            _tile.transform.localScale = new Vector3(1f - _borderSize, 1f, 1f - _borderSize);
            _node = node;
            EnableObject(_arrow, false);
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

    private void EnableObject(GameObject obj, bool state)
    {
        if (obj)
        {
            obj.SetActive(state);
        }
    }

    public void ShowArrow(Color color)
    {
        if(_node != null && _arrow != null && _node.Previous != null)
        {
            EnableObject(_arrow, true);

            Vector3 direction = (_node.Previous.position - _node.position).normalized;
            _arrow.transform.rotation = Quaternion.LookRotation(direction);

            Renderer arrowRenderer = _arrow.GetComponent<Renderer>();
            if(arrowRenderer != null)
            {
                arrowRenderer.material.color = color;
            }
        }
    }
}
