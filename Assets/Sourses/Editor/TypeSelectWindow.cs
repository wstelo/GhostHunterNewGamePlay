using System;
using UnityEditor;
using UnityEngine;

public class TypeSelectWindow : EditorWindow
{
    private static TypeSelectWindow _selectWindow;
    private readonly static ElementTypes[] ElementTypesArray = (ElementTypes[])Enum.GetValues(typeof(ElementTypes));
    private readonly static EnemyTypes[] EnemyTypesArray = (EnemyTypes[])Enum.GetValues(typeof(EnemyTypes));
    private readonly static int ElementTypesLength = Enum.GetValues(typeof(ElementTypes)).Length;
    private readonly static int EnemyTypesLength = Enum.GetValues(typeof(EnemyTypes)).Length;

    private Vector2 _scrollPosition;
    private MainWindow _mainWindow;
    private int _currentRow = 0;
    private int _currentColumn = 0;
    private int _columnsInRow = 10;
    private int _minUnitCount = 1;
    private int _maxUnitCount = 20;

    private ElementTypes _selectedElement = ElementTypes.Red; 
    private EnemyTypes _selectedEnemy = EnemyTypes.Ghost;
    private int _unitCount = 0;
    private bool _hasSelectedElement = false;
    private bool _hasSelectedEnemy = false;

    public static void ShowWindow(MainWindow mainWindow, int row, int column, Vector2 screenPosition)
    {
        if (_selectWindow != null)
        {
            _selectWindow.Close();
        }

        _selectWindow = CreateInstance<TypeSelectWindow>();
        _selectWindow.titleContent = new GUIContent("Select Element Type");
        _selectWindow.Initialize(mainWindow, row, column);


        _selectWindow.position = CalculateWindowPosition(screenPosition);
        _selectWindow.minSize = new Vector2(900, 800);
        _selectWindow.maxSize = new Vector2(1000, 1000);

        _selectWindow.Show();
    }

    private static Rect CalculateWindowPosition(Vector2 screenPosition)
    {
        float width = 1000;
        float height = 800;

        float x = Mathf.Min(screenPosition.x, Screen.currentResolution.width - width - 20);
        float y = Mathf.Min(screenPosition.y + 20, Screen.currentResolution.height - height - 20);

        return new Rect(x, y, width, height);
    }

    public void Initialize(MainWindow mainWindow, int row, int column)
    {
        _mainWindow = mainWindow;
        _currentRow = row;
        _currentColumn = column;
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField($"Select type for cell at row {_currentRow + 1}, column {_currentColumn + 1}", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        GUIElementButtons();
        GUIENemyButtons();
        GUICountSelector();

        EditorGUILayout.Space(10);

        GUISaveOrCancelButton();
    }

    private void ApplySelectionParameters()
    {
        _mainWindow.SetTileType(_currentRow, _currentColumn, _selectedElement, _selectedEnemy, _unitCount);
    }

    private void GUISaveOrCancelButton()
    {
        EditorGUILayout.LabelField($"Select type for cell at row {_currentRow + 1}, column {_currentColumn + 1}", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        {
            GUI.enabled = _hasSelectedElement && _hasSelectedEnemy; 

            if (GUILayout.Button("Apply", GUILayout.Height(30)))
            {
                ApplySelectionParameters();
                Close();
            }

            GUI.enabled = true; // Всегда включаем кнопку отмены

            if (GUILayout.Button("Cancel", GUILayout.Height(30)))
            {
                Close();
            }
        }
        EditorGUILayout.EndHorizontal();

        if (!_hasSelectedElement || !_hasSelectedEnemy)
        {
            EditorGUILayout.HelpBox("Please select both Element Type and Enemy Type", MessageType.Info);
        }
    }

    private void GUICountSelector()
    {
        EditorGUILayout.Space(30);
        _unitCount = EditorGUILayout.IntSlider("Cells Count", _unitCount, _minUnitCount, _maxUnitCount, GUILayout.Width(600), GUILayout.Height(20));
    }

    private void GUIENemyButtons()
    {
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField($"Select the EnemyType", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        int currentColumn = 0;

        EditorGUILayout.BeginHorizontal();
        {
            for (int i = 0; i < EnemyTypesLength; i++)
            {
                if (currentColumn >= _columnsInRow)
                {
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    currentColumn = 0;
                }

                bool isSelected = _hasSelectedEnemy && _selectedEnemy == EnemyTypesArray[i];
                GUIStyle buttonStyle = isSelected ? GUIColorizer.GetSelectedButtonStyle() : GUI.skin.button;

                if (GUILayout.Button(EnemyTypesArray[i].ToString(), buttonStyle, GUILayout.Width(80), GUILayout.Height(40)))
                {
                    _selectedEnemy = EnemyTypesArray[i];
                    _hasSelectedEnemy = true;
                }

                currentColumn++;

                if (i < ElementTypesLength - 1 && currentColumn < _columnsInRow)
                {
                    GUILayout.Space(5);
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        if(_hasSelectedEnemy)
        {
            EditorGUILayout.LabelField($"Selected: {_selectedEnemy}", EditorStyles.helpBox);
        }
    }

    private void GUIElementButtons()
    {
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField($"Select the ElementType", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        int currentColumn = 0;

        EditorGUILayout.BeginHorizontal();
        {
            for (int i = 0; i < ElementTypesLength; i++)
            {
                if (currentColumn >= _columnsInRow)
                {
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    currentColumn = 0;
                }

                bool isSelected = _hasSelectedElement && _selectedElement == ElementTypesArray[i];
                GUIStyle buttonStyle = isSelected ? GUIColorizer.GetSelectedButtonStyle(GUIColorizer.GetTileColor(ElementTypesArray[i])) : GetButtonStyle(ElementTypesArray[i]);

                if (GUILayout.Button(ElementTypesArray[i].ToString(), buttonStyle, GUILayout.Width(80), GUILayout.Height(40)))
                {
                    _selectedElement = ElementTypesArray[i];
                    _hasSelectedElement = true;
                }

                currentColumn++;

                if (i < ElementTypesLength - 1 && currentColumn < _columnsInRow)
                {
                    GUILayout.Space(5);
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        if (_hasSelectedElement)
        {
            EditorGUILayout.LabelField($"Selected: {_selectedElement}", EditorStyles.helpBox);
        }
    }

    private GUIStyle GetButtonStyle(ElementTypes type)
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = GUIColorizer.GetTileColor(type);
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleCenter;
        return style;
    }

    private void Update()
    {
        if (Event.current != null && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape)
        {
            Close();
            return;
        }

        if (EditorWindow.focusedWindow != this && EditorWindow.focusedWindow != null)
        {
            Close();
        }
    }

    private void OnDestroy()
    {
        _selectWindow = null;
    }
}
