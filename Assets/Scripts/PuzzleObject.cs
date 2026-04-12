using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleObject : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CinemachineCamera puzzleCamera;

    [Header("On Solve")]
    [SerializeField] private UnityEvent onSolved;

    public bool IsSolved { get; private set; }

    public void Activate()
    {
        if (IsSolved)
        {
            Debug.Log("Already solved");
            return;
        }
        PuzzleCameraHandler.Instance.EnterPuzzle(puzzleCamera);
    }

    public void Solve()
    {
        if (IsSolved) return;

        IsSolved = true;
        PuzzleCameraHandler.Instance.ExitPuzzle();
        onSolved?.Invoke();
    }
}
