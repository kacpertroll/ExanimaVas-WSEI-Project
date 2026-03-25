using Unity.Cinemachine;
using UnityEngine;

public abstract class PuzzleBase : MonoBehaviour
{
    [SerializeField] private CinemachineCamera puzzleCamera;
    [SerializeField] private GameObject puzzleUI;

    public CinemachineCamera PuzzleCamera => puzzleCamera;
    public bool IsSolved { get; private set; }

    public void Show()
    {
        puzzleUI.SetActive(true);
        OnPuzzleOpened();
    }

    public void Hide()
    {
        puzzleUI.SetActive(false);
        OnPuzzleClosed();
    }

    // Call this from your puzzle logic when correct solution is entered
    protected void CompletePuzzle()
    {
        IsSolved = true;
        PuzzleCameraHandler.Instance.ExitPuzzle();
        OnPuzzleCompleted();
    }

    // Call this from your close/back button
    public void ExitWithoutSolving()
    {
        PuzzleCameraHandler.Instance.ExitPuzzle();
    }

    protected virtual void OnPuzzleOpened() { }
    protected virtual void OnPuzzleClosed() { }
    protected abstract void OnPuzzleCompleted();
}