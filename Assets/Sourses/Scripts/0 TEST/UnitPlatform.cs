using UnityEngine;

public class UnitPlatform : MonoBehaviour
{
    public bool IsEmpty { get; private set; } = true;

    public void Occupy()
    {
        IsEmpty = false;
    }

    public void Clear()
    {
        IsEmpty = true;
    }
}
