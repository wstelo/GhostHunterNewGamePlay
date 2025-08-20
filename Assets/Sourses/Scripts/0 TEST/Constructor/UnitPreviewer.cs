using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPreviewer : MonoBehaviour
{
    [SerializeField] private LayerMask _terrainMask;

    private bool _isActiveConstructionMode = false;
    private Camera _camera;

    public ObjectPreview CurrentPreviewBuilding { get; private set; }
    public Vector3 CurrentMousePosition { get; private set; } = Vector3.zero;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void Activate(ObjectPreview _prefab, Color color)
    {
        StartCoroutine(EnablePreviewMode(_prefab, color));
    }

    public void DisableConstructionMode()
    {
        _isActiveConstructionMode = false;
    }

    //public BuildPreview GetBuildPreview()
    //{
    //    return CurrentPreviewBuilding;
    //}

    public void DisableBuildPreviewer()
    {
        _isActiveConstructionMode = false;
       // _campFactory.DeleteBuildPreview(CurrentPreviewBuilding);
    }

    private IEnumerator EnablePreviewMode(ObjectPreview _prefab, Color color)
    {
        CurrentPreviewBuilding = null;
        _isActiveConstructionMode = true;
        CurrentPreviewBuilding = Instantiate(_prefab, new Vector3(0,-30,0), Quaternion.identity);
        CurrentPreviewBuilding.Init(color);

        while (_isActiveConstructionMode)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _terrainMask))
            {
                CurrentMousePosition = hit.point;
                CurrentPreviewBuilding.transform.position = CurrentMousePosition;
            }

            yield return null;
        }
    }
}
