using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TypeSelectWindow : EditorWindow
{
    private static TypeSelectWindow _selectWindow;
    private readonly static ElementTypes[] ElementTypesArray = (ElementTypes[])Enum.GetValues(typeof(ElementTypes));

    private readonly static EnemyTypes[] EnemyTypesArray = (EnemyTypes[])Enum.GetValues(typeof(EnemyTypes));
    private readonly static EnemyTypes[] NormalEnemyTypesArray = EnemyTypesFilter.GetEnemyCategories().normal;
    private readonly static EnemyTypes[] BossEnemyTypesArray = EnemyTypesFilter.GetEnemyCategories().boss;

    private MainWindow _mainWindow;
    private int _currentRow = 0;
    private int _currentColumn = 0;
    private int _columnsInRow = 10;
    private int _minUnitCount = 1;
    private int _maxUnitCount = 20;

    private EnemyTypes _lastNormalEnemyType = EnemyTypes.Ghost;
    private ElementTypes _selectedElement = ElementTypes.Red;
    private EnemyTypes _selectedType;
    private EnemyTypes _defaultTypeForSelectedCell;
    private int _unitCount = 0;
    private bool _hasSelectedSingleElement = false;
    private bool _hasSelectedType = false;

    private bool _isMultiple = false;
    private List<ElementTypes> _selectedMultiplyElements = new List<ElementTypes>();
    private bool _hasSelectedMultiplyElement = false;
    private int _selectedHealth = 1;
    private int _minHealth = 2;
    private int _maxHealth = 8;

    public static void ShowWindow(MainWindow mainWindow, int row, int column, Vector2 screenPosition,
                                ElementTypes currentElement, EnemyTypes currentEnemy, int currentCount,
                                bool isMultiple = false, List<ElementTypes> currentElements = null, int currentHealth = 1)
    {
        if (_selectWindow != null)
        {
            _selectWindow.Close();
        }

        _selectWindow = CreateInstance<TypeSelectWindow>();
        _selectWindow.titleContent = new GUIContent("Select Element Type");
        _selectWindow.Initialize(mainWindow, row, column, currentElement, currentEnemy, currentCount, isMultiple, currentElements, currentHealth);

        _selectWindow.position = CalculateWindowPosition(screenPosition);
        _selectWindow.minSize = new Vector2(900, 800);
        _selectWindow.maxSize = new Vector2(1000, 1000);

        _selectWindow.Show();
    }

    public void Initialize(MainWindow mainWindow, int row, int column, ElementTypes currentElement,
                         EnemyTypes currentEnemy, int currentCount, bool isMultiple,
                         List<ElementTypes> currentElements, int currentHealth)
    {
        _mainWindow = mainWindow;
        _currentRow = row;
        _currentColumn = column;

        _selectedElement = currentElement;
        _selectedType = currentEnemy;
        _defaultTypeForSelectedCell = currentEnemy;
        _unitCount = currentCount;
        _isMultiple = isMultiple;
        _selectedHealth = currentHealth;

        if (!isMultiple && NormalEnemyTypesArray.Contains(currentEnemy))
        {
            _lastNormalEnemyType = currentEnemy;
        }

        if (currentElements != null)
        {
            _selectedMultiplyElements = new List<ElementTypes>(currentElements);
        }

        _hasSelectedSingleElement = true;
        _hasSelectedType = true;
        _hasSelectedMultiplyElement = _selectedMultiplyElements.Count > 0;
    }


    private void OnGUI()
    {
        EditorGUILayout.LabelField($"Select type for cell at row {_currentRow + 1}, column {_currentColumn + 1}", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("Enemy Type Selection:", EditorStyles.boldLabel, GUILayout.Width(150));
            bool oldIsMultiple = _isMultiple;
            _isMultiple = EditorGUILayout.Toggle("Multiple", _isMultiple, GUILayout.Width(150));

            if (oldIsMultiple != _isMultiple)
            {
                OnModeChanged(oldIsMultiple, _isMultiple);
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(30);

        if (_isMultiple)
        {
            _unitCount = 1;
            BossEnemySelector();
        }
        else
        {
            StandartEnemySelector();
        }

        GUISaveOrCancelButton();
    }

    private void OnModeChanged(bool oldMode, bool newMode)
    {
        if (newMode)
        {
            _selectedType = BossEnemyTypesArray.First();
            _hasSelectedType = false;
            _selectedMultiplyElements.Clear();
            _hasSelectedMultiplyElement = false;
            _selectedHealth = _minHealth;
        }
        else 
        {
            _selectedType = _lastNormalEnemyType;
            _hasSelectedType = true;

            if (_selectedMultiplyElements.Count > 0)
            {
                _selectedElement = _selectedMultiplyElements[0];
            }
            else
            {
                _selectedElement = ElementTypes.Red;
            }
            _hasSelectedSingleElement = true;
        }
    }

    private void BossEnemySelector()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("Current:", GUILayout.Width(60));

            string elementsText = _selectedMultiplyElements.Count > 0
                ? string.Join(", ", _selectedMultiplyElements)
                : "None";
            EditorGUILayout.LabelField($"Elements: {elementsText}", GUILayout.Width(500));
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField($"Enemy: {_selectedType}", GUILayout.Width(150));
            EditorGUILayout.LabelField($"Count: {_unitCount}", GUILayout.Width(80));

            EditorGUILayout.LabelField("Health:", GUILayout.Width(50));
            _selectedHealth = EditorGUILayout.IntSlider(_selectedHealth, _minHealth, _maxHealth, GUILayout.Width(150));
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);
       
        GUIMultipleElementButtons();
        GUIEnemyTypeButtons(BossEnemyTypesArray);
    }

    private void StandartEnemySelector()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("Current:", GUILayout.Width(60));
            EditorGUILayout.LabelField($"Element: {_selectedElement}", GUILayout.Width(150));
            EditorGUILayout.LabelField($"Enemy: {_selectedType}", GUILayout.Width(150));
            EditorGUILayout.LabelField($"Count: {_unitCount}", GUILayout.Width(80));
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        GUISingleElementButtons();
        GUIEnemyTypeButtons(NormalEnemyTypesArray);
        GUICountSelector();

        EditorGUILayout.Space(10);
    }

    private void ApplySelectionParameters()
    {
        if (_isMultiple)
        {
            _mainWindow.SetTileType(
                _currentRow,
                _currentColumn,
                _selectedMultiplyElements,
                _selectedType,
                _unitCount,
                true,
                _selectedHealth
            );
        }
        else
        {
            _lastNormalEnemyType = _selectedType;

            _mainWindow.SetTileType(
                _currentRow,
                _currentColumn,
                new List<ElementTypes> { _selectedElement },
                _selectedType,
                _unitCount,
                false
            );
        }
        Close();
    }

    private void GUISaveOrCancelButton()
    {
        EditorGUILayout.LabelField($"Select type for cell at row {_currentRow + 1}, column {_currentColumn + 1}", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        bool canApply;
        string message;

        if (_isMultiple)
        {
            canApply = (_selectedMultiplyElements.Count >= 2 && _hasSelectedType);
            message = _selectedMultiplyElements.Count < 2 ?
                "Please select at least 2 Element Types for Boss" :
                "Please select Enemy Type";
        }
        else
        {
            canApply = (_hasSelectedSingleElement && _hasSelectedType);
            message = "Please select both Element Type and Enemy Type";
        }

        EditorGUILayout.BeginHorizontal();
        {
            GUI.enabled = canApply;

            if (GUILayout.Button("Apply", GUILayout.Height(30)))
            {
                ApplySelectionParameters();
                Close();
            }

            GUI.enabled = true;

            if (GUILayout.Button("Cancel", GUILayout.Height(30)))
            {
                Close();
            }
        }
        EditorGUILayout.EndHorizontal();

        if (!canApply)
        {
            string messages = _isMultiple ?
                "Please select at least one Element Type and Enemy Type" :
                "Please select both Element Type and Enemy Type";
            EditorGUILayout.HelpBox(message, MessageType.Info);
        }
    }

    private void GUICountSelector()
    {
        EditorGUILayout.Space(30);
        _unitCount = EditorGUILayout.IntSlider("Cells Count", _unitCount, _minUnitCount, _maxUnitCount, GUILayout.Width(600), GUILayout.Height(20));
    }

    private void GUIEnemyTypeButtons(EnemyTypes[] typesArray)
    {
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField($"Select the EnemyType", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        int currentColumn = 0;

        EditorGUILayout.BeginHorizontal();
        {
            for (int i = 0; i < typesArray.Length; i++)
            {
                if (currentColumn >= _columnsInRow)
                {
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    currentColumn = 0;
                }

                bool isSelected = _hasSelectedType && _selectedType == typesArray[i];
                GUIStyle buttonStyle = isSelected ? GUIColorizer.GetSelectedButtonStyle() : GUI.skin.button;

                if (GUILayout.Button(typesArray[i].ToString(), buttonStyle, GUILayout.Width(80), GUILayout.Height(40)))
                {
                    _selectedType = typesArray[i];
                    _hasSelectedType = true;
                }

                currentColumn++;

                if (i < ElementTypesArray.Length - 1 && currentColumn < _columnsInRow)
                {
                    GUILayout.Space(5);
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        if (_hasSelectedType)
        {
            EditorGUILayout.LabelField($"Selected: {_selectedType}", EditorStyles.helpBox);
        }
    }

    private void GUIMultipleElementButtons()
    {
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField($"Select Multiple Element Types (for Boss)", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        int currentColumn = 0;

        EditorGUILayout.BeginHorizontal();
        {
            for (int i = 0; i < ElementTypesArray.Length; i++)
            {
                if (currentColumn >= _columnsInRow)
                {
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    currentColumn = 0;
                }

                bool isSelected = _selectedMultiplyElements.Contains(ElementTypesArray[i]);
                GUIStyle buttonStyle = isSelected
                    ? GUIColorizer.GetSelectedButtonStyle(GUIColorizer.GetTileColor(ElementTypesArray[i]))
                    : GetButtonStyle(ElementTypesArray[i]);

                if (GUILayout.Button(ElementTypesArray[i].ToString(), buttonStyle, GUILayout.Width(80), GUILayout.Height(40)))
                {
                    if (isSelected)
                    {                       
                        _selectedMultiplyElements.Remove(ElementTypesArray[i]);
                    }
                    else
                    {
                        _selectedMultiplyElements.Add(ElementTypesArray[i]);                        
                    }

                    _hasSelectedMultiplyElement = _selectedMultiplyElements.Count > 0;
                }

                currentColumn++;

                if (i < ElementTypesArray.Length - 1 && currentColumn < _columnsInRow)
                {
                    GUILayout.Space(5);
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        if (_selectedMultiplyElements.Count > 0)
        {
            EditorGUILayout.LabelField($"Selected: {string.Join(", ", _selectedMultiplyElements)}", EditorStyles.helpBox);
        }
        else
        {
            EditorGUILayout.HelpBox("No elements selected", MessageType.Warning);
        }
    }

    private void GUISingleElementButtons()
    {
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField($"Select the ElementType", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        int currentColumn = 0;

        EditorGUILayout.BeginHorizontal();
        {
            for (int i = 0; i < ElementTypesArray.Length; i++)
            {
                if (currentColumn >= _columnsInRow)
                {
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    currentColumn = 0;
                }

                bool isSelected = _hasSelectedSingleElement && _selectedElement == ElementTypesArray[i];
                GUIStyle buttonStyle = isSelected ? GUIColorizer.GetSelectedButtonStyle(GUIColorizer.GetTileColor(ElementTypesArray[i])) : GetButtonStyle(ElementTypesArray[i]);

                if (GUILayout.Button(ElementTypesArray[i].ToString(), buttonStyle, GUILayout.Width(80), GUILayout.Height(40)))
                {
                    _selectedElement = ElementTypesArray[i];
                    _hasSelectedSingleElement = true;
                }

                currentColumn++;

                if (i < ElementTypesArray.Length - 1 && currentColumn < _columnsInRow)
                {
                    GUILayout.Space(5);
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        if (_hasSelectedSingleElement)
        {
            EditorGUILayout.LabelField($"Selected: {_selectedElement}", EditorStyles.helpBox);
        }
    }

    private static Rect CalculateWindowPosition(Vector2 screenPosition)
    {
        float width = 1000;
        float height = 800;

        float x = Mathf.Min(screenPosition.x, Screen.currentResolution.width - width - 20);
        float y = Mathf.Min(screenPosition.y + 20, Screen.currentResolution.height - height - 20);

        return new Rect(x, y, width, height);
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
