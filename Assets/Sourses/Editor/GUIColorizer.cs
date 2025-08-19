using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GUIColorizer : EditorWindow
{
    private static Dictionary<ElementTypes, Texture2D> _cachedTextures = new Dictionary<ElementTypes, Texture2D>();

    public static GUIStyle GetTileColor(ElementTypes type)
    {
        if (_cachedTextures.TryGetValue(type, out var texture) == false|| texture == null)
        {
            texture = CreateTextureForType(type);
            _cachedTextures[type] = texture;
        }

        return new GUIStyle(GUI.skin.button)
        {
            normal = { background = texture, textColor = Color.white },
            hover = { background = texture },
            active = { background = texture }
        };
    }

    private static Texture2D CreateTextureForType(ElementTypes type)
    {
        Color color = type switch
        {
            ElementTypes.Red => Color.red,
            ElementTypes.Green => Color.green,
            ElementTypes.Blue => Color.blue,
            ElementTypes.Yellow => Color.yellow,
            ElementTypes.Black => Color.black,
            ElementTypes.Cyan => Color.cyan,
            _ => Color.gray
        };

        return CreateSolidTexture(2, 2, color);
    }

    private static Texture2D CreateSolidTexture(int width, int height, Color color)
    {
        var texture = new Texture2D(width, height);
        var pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = color;

        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }
}
