using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapData : MonoBehaviour
{
    [SerializeField] private int _width = 10;
    [SerializeField] private int _height = 5;

    [SerializeField] private TextAsset _textAsset;
    [SerializeField] private Texture2D _texture;
    [SerializeField] private string resourcePath = "MapData";

    public int Width => _width;
    public int Height => _height;

    private void Awake()
    {
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
                    if (texture.GetPixel(x, y) == Color.black)
                    {
                        newLine += "1";
                    }
                    else
                    {
                        newLine += "0";
                    }
                }
                lines.Add(newLine);
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
}
