using System;
using UnityEditor;
using UnityEngine;

public class TypeSelectWindow : EditorWindow
{
    private static TypeSelectWindow _typeSelectWindow;
    private readonly static ElementTypes[] Types = (ElementTypes[])Enum.GetValues(typeof(ElementTypes));
    private readonly static int ElementTypesLength = Enum.GetValues(typeof(ElementTypes)).Length;

    private MainWindow _mainWindow;
    private int _currentRow = 0;
    private int _currentColumn = 0;

    public static void ShowWindow()
    {
        _typeSelectWindow = GetWindow<TypeSelectWindow>("LevelConfigurator");
        _typeSelectWindow.minSize = new Vector2(600, 300);
        _typeSelectWindow.Show();
    }

    public void Initialize(MainWindow mainWindow, int row, int column)
    {
        _mainWindow = mainWindow;
        _currentRow = row;
        _currentColumn = column;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        {
            for (int i = 0; i < ElementTypesLength; i++)
            {
                if (GUILayout.Button(Types[i].ToString(), GUIColorizer.GetTileColor(Types[i]), GUILayout.Width(50), GUILayout.Height(30)))
                {
                    _mainWindow.SetTileType(_currentRow, _currentColumn, Types[i]);
                    Close();
                }
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}
