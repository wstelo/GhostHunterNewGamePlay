//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class TestTypeSelectWindow : EditorWindow
//{
//    private readonly static ElementTypes[] Types = (ElementTypes[])Enum.GetValues(typeof(ElementTypes));
//    private readonly static int ElementTypesLength = Enum.GetValues(typeof(ElementTypes)).Length;

//    private TestMainWindow _mainWindow;
//    private int _currentIndex = 0;

//    private int _currentRow = 0;
//    private int _currentColumn = 0;

//    public void Initialize(TestMainWindow mainWindow, int row, int column)
//    {
//        _mainWindow = mainWindow;
//        _currentRow = row;
//        _currentColumn = column;
//    }

//    //public void Initialize(TestMainWindow mainWindow, int index)
//    //{
//    //    _mainWindow = mainWindow;
//    //    _currentIndex = index;
//    //}

//    private void OnGUI()
//    {
//        EditorGUILayout.BeginHorizontal();
//        {
//            //for (int i = 0; i < ElementTypesLength; i++)
//            //{
//            //    if (GUILayout.Button(Types[i].ToString(), GUIColorizer.GetTileColor(Types[i]), GUILayout.Width(50), GUILayout.Height(30)))
//            //    {
//            //        _mainWindow.SetTileType(_currentIndex, Types[i], EnemyTypes.Ghost, 5);
//            //        Close();
//            //    }
//            //}
//        }
//        EditorGUILayout.EndHorizontal();
//    }
//}
