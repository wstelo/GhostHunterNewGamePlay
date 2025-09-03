using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MainWindow : EditorWindow
{
    private const int VisibleRows = 10;
    private const int SliderMaxValue = 300;
    private const int SliderMinValue = 0;

    private EditorWindowEnemyCell[,] _buttonValues;
    private int _ñellsCount = 1;
    private float _levelSpeed = 1f;
    private Vector2 _scrollPosition;
    private int _levelNumber = 0;

    private readonly static EnemyTypes[] NormalEnemyTypesArray = EnemyTypesFilter.GetEnemyCategories().normal;
    private EnemyTypes _defaultEnemyType = EnemyTypes.Ghost;

    private readonly int _maxRowLength = 13;

    [MenuItem("Custom/LevelConfigurator")]
    private static void ShowWindow()
    {
        MainWindow window = GetWindow<MainWindow>("LevelConfigurator");
        window.minSize = new Vector2(1500, 800);
        window.Show();
    }

    private void OnEnable()
    {
        InitializeButtonValues();
    }

    private void OnGUI()
    {
        GUILevelInfo();
        EditorGUILayout.Space(30);

        EditorGUILayout.LabelField($"Âûáåðèòå äåôîëòíûé òèï ìîíñòðîâ íà óðîâíå.", GUILayout.Width(800));         
        EditorGUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        {
            EnemyTypes oldDefaultType = _defaultEnemyType;

            foreach (EnemyTypes type in NormalEnemyTypesArray)
            {
                if (GUILayout.Button($"{type}", GUILayout.Width(100), GUILayout.Height(70)))
                {               
                    _defaultEnemyType = type;
                }
            }

            if (oldDefaultType != _defaultEnemyType)
            {
                ApplyDefaultEnemyTypeToAllCells();
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(30);

        GUIEnemySelectedButton();

        GUISaveLoadButtons();
    }

    private void ApplyDefaultEnemyTypeToAllCells()
    {
        if (_buttonValues == null) return;

        for (int i = 0; i < _buttonValues.GetLength(0); i++)
        {
            for (int j = 0; j < _buttonValues.GetLength(1); j++)
            {
                if (_buttonValues[i, j] != null)
                {
                    _buttonValues[i, j].SetParameters(
                        _buttonValues[i, j].ElementTypes,
                        _defaultEnemyType,
                        _buttonValues[i, j].Count,
                        _buttonValues[i, j].Health,
                        _buttonValues[i, j].IsMultipleElements
                    );
                }
            }
        }

        Repaint();
    }

    private void GUIEnemySelectedButton()
    {
        int oldCellsCount = _ñellsCount;
        _ñellsCount = EditorGUILayout.IntSlider("Cells Count", _ñellsCount, SliderMinValue, SliderMaxValue, GUILayout.Width(600), GUILayout.Height(20));
        EditorGUILayout.Space(30);

        if (oldCellsCount != _ñellsCount)
        {
            InitializeButtonValues();
        }

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(VisibleRows * 35));

        int cellCounter = 0;

        for (int i = 0; i < _ñellsCount; i++)
        {
            EditorGUILayout.BeginHorizontal();
            {
                int cellsInThisRow = Mathf.Min(_maxRowLength, _ñellsCount - cellCounter);

                for (int j = 0; j < cellsInThisRow; j++)
                {
                    int row = cellCounter / _maxRowLength;
                    int col = cellCounter % _maxRowLength;

                    if (row >= 0 && row < _buttonValues.GetLength(0) && col >= 0 && col < _buttonValues.GetLength(1) && _buttonValues[row, col] != null)
                    {
                        EditorWindowEnemyCell currentCell = _buttonValues[row, col];

                        string elementText = currentCell.IsMultipleElements ?
                            string.Join(",", currentCell.ElementTypes) :
                            currentCell.ElementTypes.First().ToString();

                        string buttonText = $"{elementText} \n {currentCell.EnemyType} \n Count - {currentCell.Count} \n Health - {currentCell.Health}";

                        GUIStyle buttonStyle = currentCell.IsMultipleElements ? GUI.skin.button : GUIColorizer.GetTileStyle(currentCell.ElementTypes.First());

                        if(GUILayout.Button(buttonText, buttonStyle, GUILayout.Width(100), GUILayout.Height(70)))
                        {
                            ShowTypeSelectWindow(row, col);
                        }
                    }

                    cellCounter++;
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
    }

    private void GUILevelInfo()
    {
        EditorGUILayout.LabelField($"ÏÐÈ ÄÎÁÀÂËÅÍÈÈ ElementTypes (Öâåòà) - ÍÅÎÁÕÎÄÈÌÎ äîáàâèòü â GUIColorizer", GUILayout.Width(800));         ////////////////////////////////
        EditorGUILayout.Space(30);

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("Ââåäèòå íîìåð óðîâíÿ.", GUILayout.Width(200), GUILayout.Height(20));
            _levelNumber = EditorGUILayout.IntField(_levelNumber, GUILayout.Width(50), GUILayout.Height(20));
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(30);

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("Ââåäèòå ñêîðîñòü äëÿ óðîâíÿ.", GUILayout.Width(200), GUILayout.Height(20));
            _levelSpeed = EditorGUILayout.FloatField(_levelSpeed, GUILayout.Width(50), GUILayout.Height(20));
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(30);
    }

    private void GUISaveLoadButtons()
    {
        EditorGUILayout.Space(50);

        if (GUILayout.Button("Save level config", GUILayout.Width(600), GUILayout.Height(20)))
        {
            SaveToJsonFile();
        }

        EditorGUILayout.Space(50);

        if (GUILayout.Button("Load level config", GUILayout.Width(600), GUILayout.Height(20)))
        {
            LoadFromJsonFile();
        }

        EditorGUILayout.Space(50);

        if (GUILayout.Button("Reset", GUILayout.Width(200), GUILayout.Height(30)))
        {
            if (EditorUtility.DisplayDialog("Ïîäòâåðæäåíèå", "Âû óâåðåíû, ÷òî õîòèòå ñáðîñèòü íàñòðîéêè?", "Äà", "Íåò"))
            {
                ResetButtons();
            }
        }
    }

    private void ResetButtons()
    {
        _ñellsCount = 1;
        _levelNumber = 0;
        _levelSpeed = 0;
        _defaultEnemyType = EnemyTypes.Ghost;

        InitializeButtonValues();

        for (int i = 0; i < _buttonValues.GetLength(0); i++)
        {
            for (int j = 0; j < _buttonValues.GetLength(1); j++)
            {
                if (_buttonValues[i, j] != null)
                {
                    _buttonValues[i, j].SetElements(new List<ElementTypes> { ElementTypes.Red});
                }
            }
        }

        Repaint();
    }

    private void InitializeButtonValues()
    {
        if (_buttonValues == null)
        {
            _buttonValues = new EditorWindowEnemyCell[0, 0];
        }

        int requiredRows = Mathf.CeilToInt((float)_ñellsCount / _maxRowLength);
        int requiredColumns = _maxRowLength;

        if (_buttonValues.GetLength(0) != requiredRows || _buttonValues.GetLength(1) != requiredColumns)
        {
            var newArray = new EditorWindowEnemyCell[requiredRows, requiredColumns];

            for (int i = 0; i < Mathf.Min(requiredRows, _buttonValues.GetLength(0)); i++)
            {
                for (int j = 0; j < Mathf.Min(requiredColumns, _buttonValues.GetLength(1)); j++)
                {
                    if (_buttonValues[i, j] != null)
                    {
                        newArray[i, j] = _buttonValues[i, j];
                    }
                    else
                    {
                        newArray[i, j] = new EditorWindowEnemyCell(new List<ElementTypes> { ElementTypes.Red }, _defaultEnemyType, 1);
                    }
                }
            }

            for (int i = 0; i < requiredRows; i++)
            {
                for (int j = 0; j < requiredColumns; j++)
                {
                    if (newArray[i, j] == null)
                    {
                        newArray[i, j] = new EditorWindowEnemyCell(new List<ElementTypes> { ElementTypes.Red }, _defaultEnemyType, 1);
                    }
                }
            }

            _buttonValues = newArray;
        }
    }

    private void ShowTypeSelectWindow(int row, int column)
    {
        Vector2 mousePosition = Event.current.mousePosition;
        Vector2 screenPosition = GUIUtility.GUIToScreenPoint(mousePosition);

        EditorWindowEnemyCell currentCell = _buttonValues[row, column];

        ElementTypes currentElement = currentCell.ElementTypes.FirstOrDefault();
        EnemyTypes currentEnemy = currentCell.EnemyType;
        int currentCount = currentCell.Count;
        bool isMultiple = currentCell.IsMultipleElements;
        List<ElementTypes> currentElements = currentCell.ElementTypes;
        int currentHealth = currentCell.Health;

        TypeSelectWindow.ShowWindow(
            this,
            row,
            column,
            screenPosition,
            currentElement,
            currentEnemy,
            currentCount,
            isMultiple,
            currentElements,
            currentHealth
        );
    }

    public void SetTileType(int row, int column, List<ElementTypes> elementTypes, EnemyTypes enemyType, int count, bool isMultiple, int health = 1)
    {
        _buttonValues[row, column].SetParameters(elementTypes, enemyType, count, health, isMultiple);
        Repaint();
    }

    public void SetTileType(int row, int column, ElementTypes elementType, EnemyTypes enemyType, int count)
    {
        SetTileType(row, column, new List<ElementTypes> { elementType }, enemyType, count, false, 1);
    }

    private void SaveToJsonFile()
    {

        LevelConfig config = new LevelConfig(GetEnemiesConfig(), _levelNumber, _levelSpeed);
        JsonSaver.SaveToJsonFile(config);
    }

    private void LoadFromJsonFile()
    {
        LevelConfig json = JsonSaver.LoadFromJsonFile();

        try
        {
            _buttonValues = GetEnemyCells(json.EnemiesLevelConfigs);
            _levelNumber = json.LevelNumber;
            _levelSpeed = json.LevelSpeed;

            InitializeButtonValues();
            Repaint();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load config: {ex.Message}");
        }
    }

    private EditorWindowEnemyCell[,] GetEnemyCells(List<EnemiesLevelConfig> enemiesConfigs)
    {
        if (enemiesConfigs == null)
        {
            enemiesConfigs = new List<EnemiesLevelConfig>();
        }

        _ñellsCount = enemiesConfigs.Count;

        int rows = Mathf.CeilToInt((float)_ñellsCount / _maxRowLength);
        var result = new EditorWindowEnemyCell[rows, _maxRowLength];

        int configIndex = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < _maxRowLength; col++)
            {
                if (configIndex < _ñellsCount)
                {
                    var config = enemiesConfigs[configIndex];
                    result[row, col] = new EditorWindowEnemyCell(
                        config.ElementTypes,
                        config.EnemyType,
                        config.Count,
                        config.Health,
                        config.IsMultiple
                        );

                    configIndex++;
                }
                else
                {
                    result[row, col] = new EditorWindowEnemyCell(new List<ElementTypes> { ElementTypes.Red }, EnemyTypes.Ghost, 1);
                }
            }
        }

        return result;
    }

    private List<EnemiesLevelConfig> GetEnemiesConfig()
    {
        List<EnemiesLevelConfig> enemiesConfigs = new List<EnemiesLevelConfig>();

        int cellCounter = 0;

        for (int i = 0; i < _buttonValues.GetLength(0); i++)
        {
            for (int j = 0; j < _buttonValues.GetLength(1); j++)
            {
                if(cellCounter >= _ñellsCount)
                {
                    return enemiesConfigs;
                }

                EditorWindowEnemyCell currentCell = _buttonValues[i, j];
                EnemiesLevelConfig newConfig = new EnemiesLevelConfig(currentCell.ElementTypes, currentCell.EnemyType, currentCell.Count, currentCell.IsMultipleElements, currentCell.Health);
                enemiesConfigs.Add(newConfig);

                cellCounter++;
            }
        }

        return enemiesConfigs;
    }
}
