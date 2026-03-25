using UnityEngine;

public class PuzzleTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private PuzzleBase puzzle;
    [SerializeField] private string examinePrompt = "Press E to examine";
    [SerializeField] private string solvedPrompt = "Already unlocked";

    public void Interact()
    {
        if (puzzle != null)
        {
            if (puzzle.IsSolved) return;
        }
        PuzzleCameraHandler.Instance.EnterPuzzle(puzzle);
    }

    public string GetPromptText()
    {
        if (puzzle != null)
        {
            return solvedPrompt;
        }
        else return examinePrompt;
    }
}