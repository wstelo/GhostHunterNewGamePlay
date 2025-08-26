using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildPreview : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private LayerMask _obstacleMask;

    private UnitPlatform _currentPlatform;

    public bool IsOnPlatform => _currentPlatform != null;

    private Color _transparentColor;
    private Color _standartColor;

    public void Init(Color color)                                   ////////////////////////////// Создать колорайзер?
    {
        _transparentColor = color;
        _standartColor = color;
        _transparentColor.a = 0.4f;
        _meshRenderer.material.color = _transparentColor;
    }

    public UnitPlatform GetCurrentPlatform()
    {
        return _currentPlatform;
    }

    private void OnTriggerEnter(Collider other)                           /////////////////////// убрать в Детектор
    {
        if (other.gameObject.TryGetComponent(out UnitPlatform platform))
        {
            _currentPlatform = platform;
        }

        if (_currentPlatform != null)
        {
            _meshRenderer.material.color = _standartColor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out UnitPlatform platform))
        {
            _currentPlatform = null;
        }

        if (_currentPlatform == null)
        {
            _meshRenderer.material.color = _transparentColor;
        }
    }
}
