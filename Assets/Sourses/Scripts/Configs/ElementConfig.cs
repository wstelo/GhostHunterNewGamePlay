using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewElementConfig", menuName = "NewElementConfig / NewConfig")]
public class ElementConfig : ScriptableObject
{
    [SerializeField] private ElementTypes _type;
    [SerializeField] private Ghost _unitPrefab;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private Color _typeColor;
    [SerializeField] private ObjectPreview _unitPreviewPrefab;

    public ElementTypes Type => _type;
    public Ghost UnitPrefab => _unitPrefab;
    public Color Color => _typeColor;
    public Projectile ProjectilePrefab => _projectilePrefab;
    public GameObject HitEffect => _hitEffect;
    public ObjectPreview UnitPreviewPrefab => _unitPreviewPrefab;
}
