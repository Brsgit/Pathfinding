using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapData : MonoBehaviour
{
    [SerializeField] private int _width = 10;
    [SerializeField] private int _height = 5;

    [SerializeField] private TextAsset _textAsset;
    [SerializeField] private string resourcePath = "MapData";

    public int Width => _width;
    public int Height => _height;

    public int[,] MakeMap()
    {
        List<string> lines = GetTextFromFile();
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

        map[1, 0] = 1;
        map[1, 1] = 1;
        map[1, 2] = 1;

        return map;
    }

    public List<string> GetTextFromFile(TextAsset text)
    {
        List<string> lines = new List<string>();

        if (text != null)
        {
            string textData = text.text;
            string[] delimeters = { "\r\n", "\n" };
            lines.AddRange(textData.Split(delimeters, StringSplitOptions.None));
            lines.Reverse();
        }
        else
        {
            Debug.LogWarning("INVALID TEXT ASSET!");
        }

        return lines;
    }

    public List<string> GetTextFromFile()
    {
        if (_textAsset == null)
        {
            string levelName = SceneManager.GetActiveScene().name;
            _textAsset = Resources.Load(resourcePath + "/" + levelName) as TextAsset;
        }

        return GetTextFromFile(_textAsset);
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
