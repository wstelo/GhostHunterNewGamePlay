using System;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class MainWindow : EditorWindow
{
    private const int VisibleRows = 10;
    private const int SliderMaxValue = 100;
    private const int SliderMinValue = 0;

    private ElementTypes[,] _buttonValues;
    private int _sliderValue = 1;
    private float _levelSpeed = 0.1f;
    private Vector2 _scrollPosition;
    private int _levelNumber = 0;

    private readonly int _maxRowLength = GameStaticData.MaxColumnCount;

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
        EditorGUILayout.Space(30);

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("Введите номер уровня.", GUILayout.Width(200), GUILayout.Height(20));
            _levelNumber = EditorGUILayout.IntField(_levelNumber, GUILayout.Width(50), GUILayout.Height(20));
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(30);

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("Введите скорость для уровня.", GUILayout.Width(200), GUILayout.Height(20));
            _levelSpeed = EditorGUILayout.FloatField(_levelSpeed, GUILayout.Width(50), GUILayout.Height(20)); 
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(30);

        int oldSliderValue = _sliderValue;
        _sliderValue = EditorGUILayout.IntSlider("RowsCount", _sliderValue, SliderMinValue, SliderMaxValue, GUILayout.Width(600), GUILayout.Height(20));
        EditorGUILayout.Space(30);

        if (oldSliderValue != _sliderValue)
        {
            InitializeButtonValues();
        }

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(VisibleRows * 35));

        for (int i = 0; i < _sliderValue; i++)
        {
            EditorGUILayout.BeginHorizontal();
            {
                for (int j = 0; j < _maxRowLength; j++)
                {
                    if (GUILayout.Button(_buttonValues[i, j].ToString(), GUIColorizer.GetTileColor(_buttonValues[i, j]), GUILayout.Width(50), GUILayout.Height(30)))
                    {
                        ShowTypeSelectWindow(i, j);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

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
            if (EditorUtility.DisplayDialog("Подтверждение", "Вы уверены, что хотите сбросить настройки?", "Да", "Нет"))
            {
                ResetButtons();
            }
        }
    }

    private void ResetButtons()
    {
        _sliderValue = 1;
        _levelNumber = 0;
        _levelSpeed = 0;

        for (int i = 0; i < _sliderValue; i++)
        {
            for (int j = 0; j < _maxRowLength; j++)
            {
                _buttonValues[i, j] = ElementTypes.Red;
            }
        }

        InitializeButtonValues();
        Repaint();
    }

    private void InitializeButtonValues()
    {
        if (_buttonValues == null || _buttonValues.GetLength(0) != _sliderValue || _buttonValues.GetLength(1) != _maxRowLength)
        {
            var newArray = new ElementTypes[_sliderValue, _maxRowLength];

            if (_buttonValues != null)
            {
                int copyRows = Mathf.Min(_sliderValue, _buttonValues.GetLength(0));
                int copyCols = Mathf.Min(_maxRowLength, _buttonValues.GetLength(1));

                for (int i = 0; i < copyRows; i++)
                {
                    for (int j = 0; j < copyCols; j++)
                    {
                        newArray[i, j] = _buttonValues[i, j];
                    }
                }
            }

            _buttonValues = newArray;
        }
    }

    private void ShowTypeSelectWindow(int row, int column)
    {
        int minHeight = 400;
        int minWidth = 100;
        int yOffset = 20;

        Vector2 mousePosition = Event.current.mousePosition;
        Rect buttonRect = GUILayoutUtility.GetLastRect();
        Vector2 screenPosition = GUIUtility.GUIToScreenPoint(mousePosition);

        var selectWindow = EditorWindow.GetWindow<TypeSelectWindow>();
        selectWindow.Initialize(this, row, column);
        selectWindow.Show();

        Rect position = new Rect(screenPosition.x, screenPosition.y + yOffset, minHeight, minWidth);
        selectWindow.position = position;
    }

    public void SetTileType(int row, int column, ElementTypes type)
    {
        _buttonValues[row, column] = type;
        Repaint();
    }

    private void SaveToJsonFile()
    {
        LevelConfig config = new LevelConfig(_buttonValues, _levelNumber, _levelSpeed);
        JsonSaver.SaveToJsonFile(config);
    }

    private void LoadFromJsonFile()
    {
        LevelConfig json = JsonSaver.LoadFromJsonFile();

        try
        {
            _buttonValues = json.ButtonValues;
            _sliderValue = _buttonValues.GetLength(0);
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
}
