using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GUIColorizer : EditorWindow
{
    private static Dictionary<ElementTypes, Texture2D> _cachedTextures = new Dictionary<ElementTypes, Texture2D>();

    public static GUIStyle GetTileStyle(ElementTypes type)
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

    public static Color GetTileColor(ElementTypes type)
    {
        return type switch
        {
            ElementTypes.Red => Color.red,
            ElementTypes.Green => Color.green,
            ElementTypes.Blue => Color.blue,
            ElementTypes.Yellow => Color.yellow,
            ElementTypes.Black => Color.black,
            ElementTypes.Cyan => Color.cyan,
            _ => Color.gray
        };
    }

    public static GUIStyle GetSelectedButtonStyle(Color? textColor = null)
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.normal.background = CreateSelectedTexture();
        style.normal.textColor = textColor ?? Color.white;
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleCenter;
        return style;
    }

    private static Texture2D CreateSelectedTexture()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(0.2f, 0.6f, 1f, 0.8f));
        texture.Apply();
        return texture;
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
