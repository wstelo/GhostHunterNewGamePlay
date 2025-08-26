//using System;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class TestMainWindow : EditorWindow
//{
//    private List<TESTEditorWindowCell> _cells;
//    private TESTEditorWindowCell[,] _buttonValues;
//    private Vector2 _scrollPosition;

//    private const int VisibleRows = 10;
//    private int _cellsCount = 1;
//    private int _maxRowLength = 13;

//    private const int SliderMaxValue = 100;
//    private const int SliderMinValue = 0;
//    private int _sliderValue = 1;

//    private int _levelNumber = 0;
//    private float _levelSpeed = 0.1f;

//    [MenuItem("Custom/TestLevelConfigurator")]
//    private static void ShowWindow()
//    {
//        TestMainWindow window = GetWindow<TestMainWindow>("TestLevelConfigurator");
//        window.minSize = new Vector2(1500, 800);
//        window.Show();
//    }

//    private void OnEnable()
//    {
//        InitializeButtonValues();
//        //InitializeButtons();
//    }

//    private void OnGUI()
//    {
//        EditorGUILayout.LabelField($"ПРИ ДОБАВЛЕНИИ ElementTypes (Цвета) - НЕОБХОДИМО добавить в GUIColorizer", GUILayout.Width(800));

//       // InitializeButtons();
//        GUILevelInfo();
//        int oldSliderValue = _sliderValue;
//        _sliderValue = EditorGUILayout.IntSlider("RowsCount", _sliderValue, SliderMinValue, SliderMaxValue, GUILayout.Width(600), GUILayout.Height(20));
//        EditorGUILayout.Space(30);

//        if (oldSliderValue != _sliderValue)
//        {
//            InitializeButtonValues();
//        }

//        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(VisibleRows * 35));

//        for (int i = 0; i < _sliderValue; i++)
//        {
//            EditorGUILayout.BeginHorizontal();
//            {
//                for (int j = 0; j < _maxRowLength; j++)
//                {
//                    if (GUILayout.Button(_buttonValues[i, j].ToString(), GUIColorizer.GetTileColor(_buttonValues[i, j].ElementType), GUILayout.Width(50), GUILayout.Height(30)))
//                    {
//                        ShowTypeSelectWindow(i, j);
//                    }
//                }
//            }
//            EditorGUILayout.EndHorizontal();
//        }

//        EditorGUILayout.EndScrollView();
//        //      GUIMainWindow();
//        GUISaveLoadButtons();
//    }

//    private void InitializeButtonValues()
//    {
//        if (_buttonValues == null || _buttonValues.GetLength(0) != _sliderValue || _buttonValues.GetLength(1) != _maxRowLength)
//        {
//            var newArray = new TESTEditorWindowCell[_sliderValue, _maxRowLength];

//            if (_buttonValues != null)
//            {
//                int copyRows = Mathf.Min(_sliderValue, _buttonValues.GetLength(0));
//                int copyCols = Mathf.Min(_maxRowLength, _buttonValues.GetLength(1));

//                for (int i = 0; i < copyRows; i++)
//                {
//                    for (int j = 0; j < copyCols; j++)
//                    {
//                        newArray[i, j] = _buttonValues[i, j];
//                    }
//                }
//            }

//            _buttonValues = newArray;
//        }
//    }

//    private void ShowTypeSelectWindow(int row, int column)
//    {
//        int minHeight = 400;
//        int minWidth = 100;
//        int yOffset = 20;

//        Vector2 mousePosition = Event.current.mousePosition;
//        Rect buttonRect = GUILayoutUtility.GetLastRect();
//        Vector2 screenPosition = GUIUtility.GUIToScreenPoint(mousePosition);

//        var selectWindow = EditorWindow.GetWindow<TestTypeSelectWindow>();
//        selectWindow.Initialize(this, row, column);
//        selectWindow.Show();

//        Rect position = new Rect(screenPosition.x, screenPosition.y + yOffset, minHeight, minWidth);
//        selectWindow.position = position;
//    }

//    private void InitializeButtons()
//    {
//        if (_cells == null)
//        {
//            _cells = new List<TESTEditorWindowCell>();
//        }

//        if (_cells.Count != _cellsCount)
//        {
//            var tempCells = new List<TESTEditorWindowCell>();

//            int tempCount = Mathf.Min(_cellsCount, _cells.Count);

//            for (int i = 0; i < tempCount; i++)
//            {
//                tempCells.Add(_cells[i]);
//            }

//            for (int i = tempCount; i < _cellsCount; i++)
//            {
//                tempCells.Add(new TESTEditorWindowCell(ElementTypes.Red, EnemyTypes.Ghost, 1));
//            }

//            _cells = tempCells;
//        }
//    }

//    private void GUIMainWindow()
//    {
//        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(VisibleRows * 35));

//        int buttonCounter = 0;

//        while (buttonCounter < _cellsCount)
//        {
//            EditorGUILayout.BeginHorizontal();

//            int buttonInThisRow = 0;

//            for (int i = buttonInThisRow; i < _maxRowLength; i++)
//            {
//                if (buttonCounter < _cellsCount)
//                {
//                    if (GUILayout.Button($"{_cells[i].ElementType},\n {_cells[i].EnemyType},\n {_cells[i].Count}", GUIColorizer.GetTileColor(_cells[i].ElementType), GUILayout.Width(100), GUILayout.Height(60)))
//                    {
//                        //ShowTypeSelectWindow(i);
//                    }

//                    buttonCounter++;
//                    buttonInThisRow++;
//                }
//            }

//            if (buttonCounter >= _cellsCount)
//            {
//                if (GUILayout.Button("+", GUILayout.Width(50), GUILayout.Height(30)))
//                {

//                    _cellsCount++;
//                    Repaint();

//                }

//                if (GUILayout.Button("-", GUILayout.Width(50), GUILayout.Height(30)))
//                {
//                    if (_cellsCount > 1)
//                    {
//                        _cellsCount--;
//                        Repaint();
//                    }
//                }
//            }
//            else
//            {
//                GUILayout.FlexibleSpace();
//            }

//            InitializeButtons();

//            EditorGUILayout.EndHorizontal();

//        }

//        EditorGUILayout.EndScrollView();

//        EditorGUILayout.BeginHorizontal();

//        if (GUILayout.Button("+ 10", GUILayout.Width(50), GUILayout.Height(30)))
//        {
//            _cellsCount += 10;
//            Repaint();
//        }

//        if (GUILayout.Button("- 10", GUILayout.Width(50), GUILayout.Height(30)))
//        {
//            if (_cellsCount > 10)
//            {
//                _cellsCount -= 10;
//                Repaint();
//            }
//        }

//        EditorGUILayout.EndHorizontal();
//    }

//    private void GUISaveLoadButtons()
//    {
//        EditorGUILayout.Space(50);

//        if (GUILayout.Button("Save level config", GUILayout.Width(600), GUILayout.Height(20)))
//        {
//           // SaveToJsonFile();
//        }

//        EditorGUILayout.Space(50);

//        if (GUILayout.Button("Load level config", GUILayout.Width(600), GUILayout.Height(20)))
//        {
//           // LoadFromJsonFile();
//        }

//        EditorGUILayout.Space(50);

//        if (GUILayout.Button("Reset", GUILayout.Width(200), GUILayout.Height(30)))
//        {
//            if (EditorUtility.DisplayDialog("Подтверждение", "Вы уверены, что хотите сбросить настройки?", "Да", "Нет"))
//            {
//               // ResetButtons();
//            }
//        }
//    }

//    private void GUILevelInfo()
//    {
//        EditorGUILayout.Space(30);

//        EditorGUILayout.BeginHorizontal();
//        {
//            EditorGUILayout.LabelField("Введите номер уровня.", GUILayout.Width(200), GUILayout.Height(20));
//            _levelNumber = EditorGUILayout.IntField(_levelNumber, GUILayout.Width(50), GUILayout.Height(20));
//        }
//        EditorGUILayout.EndHorizontal();

//        EditorGUILayout.Space(30);

//        EditorGUILayout.BeginHorizontal();
//        {
//            EditorGUILayout.LabelField("Введите скорость для уровня.", GUILayout.Width(200), GUILayout.Height(20));
//            _levelSpeed = EditorGUILayout.FloatField(_levelSpeed, GUILayout.Width(50), GUILayout.Height(20));
//        }
//        EditorGUILayout.EndHorizontal();

//        EditorGUILayout.Space(30);
//    }

//    //private void ResetButtons()
//    //{
//    //    _sliderValue = 1;
//    //    _levelNumber = 0;
//    //    _levelSpeed = 0;

//    //    for (int i = 0; i < _sliderValue; i++)
//    //    {
//    //        for (int j = 0; j < _rowLength; j++)
//    //        {
//    //            _buttonValues[i, j] = ElementTypes.Red;
//    //        }
//    //    }

//    //    InitializeButtonValues();
//    //    Repaint();
//    //}

//    //private void ShowTypeSelectWindow(int index)
//    //{
//    //    int minHeight = 400;
//    //    int minWidth = 100;
//    //    int yOffset = 20;

//    //    Vector2 mousePosition = Event.current.mousePosition;
//    //    Rect buttonRect = GUILayoutUtility.GetLastRect();
//    //    Vector2 screenPosition = GUIUtility.GUIToScreenPoint(mousePosition);

//    //    var selectWindow = EditorWindow.GetWindow<TestTypeSelectWindow>();
//    //    selectWindow.titleContent = new GUIContent("CellConfigurator");
//    //    selectWindow.position = new Rect(100, 100, 1000, 600);
//    //    selectWindow.minSize = new Vector2(1000, 600);
//    //    selectWindow.Initialize(this, index);
//    //    selectWindow.Show();

//    //    Rect position = new Rect(screenPosition.x, screenPosition.y + yOffset, minHeight, minWidth);
//    //    selectWindow.position = position;
//    //}

//    public void SetTileType(int index, ElementTypes elementType, EnemyTypes enemyType, int count)
//    {
//        _cells[index].SetParameters(elementType, enemyType, count);
//        Repaint();
//    }

//    //private void SaveToJsonFile()
//    //{
//    //    LevelConfig config = new LevelConfig(_buttonValues, _levelNumber, _levelSpeed);
//    //    JsonSaver.SaveToJsonFile(config);
//    //}

//    //private void LoadFromJsonFile()
//    //{
//    //    LevelConfig json = JsonSaver.LoadFromJsonFile();

//    //    try
//    //    {
//    //        _buttonValues = json.ButtonValues;
//    //        _cellsCount = _buttonValues.GetLength(0);
//    //        _levelNumber = json.LevelNumber;
//    //        _levelSpeed = json.LevelSpeed;

//    //        InitializeButtons();
//    //        Repaint();
//    //    }
//    //    catch (Exception ex)
//    //    {
//    //        Debug.LogError($"Failed to load config: {ex.Message}");
//    //    }
//    //}
//}
