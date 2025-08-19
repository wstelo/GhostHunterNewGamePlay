using System;
using UnityEngine;

public class UnitLineMover : MonoBehaviour
{
    private float _moveSpeed = 0;
    private Vector3 _moveDirection = Vector3.forward;

    public event Action LineWalked;

    public void Init(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        transform.Translate(_moveDirection * _moveSpeed * Time.fixedDeltaTime);

        if (transform.position.z > GameStaticData.LinePositionForSpawnNewLine.z)
        {
            LineWalked?.Invoke();
        }
    }
}
