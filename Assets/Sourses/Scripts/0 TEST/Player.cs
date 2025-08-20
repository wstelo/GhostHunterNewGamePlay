using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SpawnableObject<Player>
{
    [SerializeField] private PlayerAnimatorController _playerAnimatorController;
    [SerializeField] private MeshRenderer _renderer;

    private ElementTypes _elementType;

    public ElementTypes Type => _elementType;

    public override void Init(Color color, ElementTypes elementType)
    {
        _elementType = elementType;
        SetMaterial(color);
    }

    private void SetMaterial(Color color)
    {
        _renderer.material.color = color;
    }
}
