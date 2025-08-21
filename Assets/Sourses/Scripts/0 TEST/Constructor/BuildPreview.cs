using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPreview : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private LayerMask _obstacleMask;

    private List<Collider> _currentObstacles = new List<Collider>();

    private bool _isActiveInstallationMode = true;

    public bool HasObstacle => _currentObstacles.Count > 0;

    public event Action<BuildPreview> ConstructionEnded;

    private Color _transparentColor;
    private Color _standartColor;

    public void Init(Color color)
    {
        _transparentColor = color;
        _standartColor = color;
        _transparentColor.a = 0.4f;
        _meshRenderer.material.color = _transparentColor;
    }

    public void EndedConstruction()
    {
        ConstructionEnded?.Invoke(this);
    }

    public void DisableInstallationMode()
    {
        _isActiveInstallationMode = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActiveInstallationMode)
        {
            if (_obstacleMask.IsContains(other.gameObject.layer))
            {
                _currentObstacles.Add(other);
            }

            if (_currentObstacles.Count > 0)
            {
                _meshRenderer.material.color = _standartColor ;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isActiveInstallationMode)
        {
            if (_obstacleMask.IsContains(other.gameObject.layer))
            {
                _currentObstacles.Remove(other);
            }

            if (_currentObstacles.Count == 0)
            {
                _meshRenderer.material.color = _transparentColor;
            }
        }
    }
}
