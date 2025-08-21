using System.Collections;
using UnityEngine;

public class BuildPreviewer : MonoBehaviour
{
    [SerializeField] private LayerMask _terrainMask;

    private bool _isActiveConstructionMode = false;
    private Camera _camera;
    private Coroutine _currentCoroutine;

    public BuildPreview CurrentPreviewBuilding { get; private set; }
    public Vector3 CurrentMousePosition { get; private set; } = Vector3.zero;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void Activate(BuildPreview _prefab, Color color)
    {
        if(_currentCoroutine != null)
        {
            StopCoroutine( _currentCoroutine );
        }

        _currentCoroutine = StartCoroutine(EnablePreviewMode(_prefab, color));
    }

    public void DisableBuildPreviewer()
    {
        _isActiveConstructionMode = false;

        if(CurrentPreviewBuilding != null)
        {
            Destroy(CurrentPreviewBuilding.gameObject);///////////////////////
        }                                                               
    }

    private IEnumerator EnablePreviewMode(BuildPreview _prefab, Color color)
    {
        CurrentPreviewBuilding = null;
        _isActiveConstructionMode = true;
        CurrentPreviewBuilding = Instantiate(_prefab, new Vector3(0, -30, 0), Quaternion.identity);          //////////////////////////
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
