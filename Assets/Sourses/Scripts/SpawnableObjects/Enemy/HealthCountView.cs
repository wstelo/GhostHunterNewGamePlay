using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthCountView : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = Vector3.zero; 
    [SerializeField] private TMP_Text _text;

    private Camera _targetCamera;


    private void Awake()
    {
        _targetCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (_targetCamera == null)
            return;

        transform.rotation = _targetCamera.transform.rotation;

    }

    public void Init(int count)
    {
        _text.text = count.ToString();
    }
}
