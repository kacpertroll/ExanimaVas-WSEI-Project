using Unity.Cinemachine;
using UnityEngine;

public class PuzzleObject : MonoBehaviour
{
    [SerializeField] private CinemachineCamera puzzleCamera;

    public void Activate()
    {
        PuzzleCameraHandler.Instance.EnterPuzzle(puzzleCamera);
    }
}