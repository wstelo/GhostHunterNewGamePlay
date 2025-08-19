using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : SpawnableObject<Ghost>
{
    private MeshRenderer _renderer;
    private Color _color;
    public ElementTypes Type { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public override void Init(Color color, ElementTypes elementType)
    {
        _color = color;
        _renderer.material.color = _color;
        Type = elementType;

        Vector3 zPosition = new Vector3(transform.position.x, transform.position.y, 0);         ///////////////////////////////////////////////////////////
        transform.localPosition = zPosition;
    }
}
