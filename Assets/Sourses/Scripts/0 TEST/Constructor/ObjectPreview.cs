using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPreview : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacleMask;

    private List<Collider> _currentObstacles = new List<Collider>();
    private MeshRenderer _currentMaterial;
    private bool _isActiveInstallationMode = true;

    public bool HasObstacle => _currentObstacles.Count > 0;

    public event Action<ObjectPreview> ConstructionEnded;

    private void Awake()
    {
        _currentMaterial = GetComponent<MeshRenderer>();
    }

    public void Init(Color color)
    {
        color.a = 0.3f;
        _currentMaterial.material.color = color;
    }

    public void EndedConstruction()
    {
        ConstructionEnded?.Invoke(this);
    }

    public void DisableInstallationMode()
    {
        _isActiveInstallationMode = false;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (_isActiveInstallationMode)
    //    {
    //        if (_obstacleMask.IsContains(other.gameObject.layer))
    //        {
    //            _currentObstacles.Add(other);
    //        }

    //        if (_currentObstacles.Count > 0)
    //        {
    //            _currentMaterial.material = _redMaterial;
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (_isActiveInstallationMode)
    //    {
    //        if (_obstacleMask.IsContains(other.gameObject.layer))
    //        {
    //            _currentObstacles.Remove(other);
    //        }

    //        if (_currentObstacles.Count == 0)
    //        {
    //            _currentMaterial.material = _standartMaterial;
    //        }
    //    }
    //}
}
