using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameStaticData
{
    public const int MaxColumnCount = 13;
    public const int MaximumProjectileCellNumber = 8;
    public const float StartAttackDelay = 1.5f;
    public const float LoadingScreenLoadingTime = 3f;
    public const float PercentOfLineToSpawnNewUit = 1.5f;

    public static readonly Vector3 UnitLineSpawnPoint = new Vector3(0, 0, -12);
    public static readonly Vector3 LinePositionForSpawnNewLine = new Vector3(0, 0, -11);
}
