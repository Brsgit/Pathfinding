using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapData : MonoBehaviour
{
    [SerializeField] private int _width = 10;
    [SerializeField] private int _height = 5;
    public int Width => _width;
    public int Height => _height;

    [SerializeField] private TextAsset _textAsset;
    [SerializeField] private Texture2D _texture;
    [SerializeField] private string resourcePath = "MapData";

    public Color32 openColor = Color.white;
    public Color32 blockedColor = Color.black;
    public Color32 lightTerrainColor = new Color32(124, 194, 78, 255);
    public Color32 medTerrainColor = new Color32(252, 255, 52, 255);
    public Color32 heavyTerrainColor = new Color32(255, 129, 12, 255);

    static Dictionary<Color32, NodeType> terrainLookupTable = new();


    private void Awake()
    {
        SetupLookupTable();

        string levelName = SceneManager.GetActiveScene().name;
        if (_texture == null)
        {
            _texture = Resources.Load(resourcePath + "/" + levelName) as Texture2D;
        }

        if (_textAsset == null)
        {
            _textAsset = Resources.Load(resourcePath + "/" + levelName) as TextAsset;
        }

    }

    public int[,] MakeMap()
    {
        List<string> lines = new List<string>();

        if (_texture != null)
        {
            lines = GetMapFromTexture(_texture);
        }
        else
        {
            lines = GetMapFromTextFile(_textAsset);
        }

        SetDimensions(lines);

        var map = new int[_width, _height];

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (lines[y].Length > x)
                {
                    map[x, y] = (int)Char.GetNumericValue(lines[y][x]);
                }
            }
        }

        return map;
    }

    public List<string> GetMapFromTextFile(TextAsset text)
    {
        List<string> lines = new List<string>();

        if (text != null)
        {
            string textData = text.text;
            string[] delimeters = { "\r\n", "\n" };
            lines.AddRange(textData.Split(delimeters, StringSplitOptions.None));
            lines.Reverse();
        }

        return lines;
    }

    public List<string> GetMapFromTextFile()
    {
        return GetMapFromTextFile(_textAsset);
    }

    public List<string> GetMapFromTexture(Texture2D texture)
    {
        List<string> lines = new List<string>();

        if (_texture != null)
        {
            for (int y = 0; y < texture.height; y++)
            {
                string newLine = "";

                for (int x = 0; x < texture.width; x++)
                {
                    Color pixelColor = texture.GetPixel(x, y);

                    if (terrainLookupTable.ContainsKey(pixelColor))
                    {
                        NodeType nodeType = terrainLookupTable[pixelColor];
                        int nodeTypeNum = (int)nodeType;
                        newLine += nodeTypeNum;
                    }
                    else
                    {
                        newLine += '0';
                    }
                }
                lines.Add(newLine);
                //Debug.Log(newLine);
            }

        }

        return lines;
    }

    public void SetDimensions(List<string> textLines)
    {
        _height = textLines.Count;

        foreach (string line in textLines)
        {
            if (line.Length > _width)
                _width = line.Length;
        }
    }

    private void SetupLookupTable()
    {
        terrainLookupTable.Add(openColor, NodeType.Open);
        terrainLookupTable.Add(blockedColor, NodeType.Blocked);
        terrainLookupTable.Add(lightTerrainColor, NodeType.LightTerrain);
        terrainLookupTable.Add(medTerrainColor, NodeType.MedTerrain);
        terrainLookupTable.Add(heavyTerrainColor, NodeType.HeavyTerrain);
    }

    public static Color GetColorFromNodeType(NodeType type)
    {
        if (terrainLookupTable.ContainsValue(type))
        {
            Color colorKey = terrainLookupTable.FirstOrDefault(x => x.Value == type).Key;
            return colorKey;
        }

        return Color.white;
    }
}
