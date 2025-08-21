using UnityEngine;
using static UnityEditor.Progress;

public class BuildConstructor : MonoBehaviour
{
    [SerializeField] private BuildPreviewer _buildPreviewer;
    [SerializeField] private InputHandler _inputHandler;

    private SpawnableObjectData<Player> _spawnableObjectData;

    private bool _isPressedButton = false;

    public void CreateBuildPreview(BuildPreview _prefab, Color color)
    {
        _buildPreviewer.Activate(_prefab, color);
        _inputHandler.CancelButtonUnpressed += Activate;
    }

    private void Activate()
    {
        if (_buildPreviewer.CurrentPreviewBuilding.HasObstacle)
        {

        }
        else
        {
            Debug.Log("Bum");
            _buildPreviewer.DisableBuildPreviewer();
            _inputHandler.SelectButtonPressed -= Activate;
        }

        _isPressedButton = false;
    }
}
