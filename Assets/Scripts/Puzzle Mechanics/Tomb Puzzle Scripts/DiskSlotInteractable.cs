using UnityEngine;
using UnityEngine.Events;

public class DiskSlotInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private DiskType requiredDisk;
    [SerializeField] private DiskSequencePuzzle puzzleController;
    [SerializeField] private string promptText = "Press E to insert disk";
    [SerializeField] private GameObject diskVisual;

    [Header("Feedback Events")]
    [SerializeField] private UnityEvent emptyHanded;
    [SerializeField] private UnityEvent partHanded;
    [SerializeField] private UnityEvent wrongDisk;

    public bool IsFilled { get; private set; }
    public DiskType RequiredDisk => requiredDisk;

    public void Interact()
    {
        if (IsFilled) return;

        if (!PlayerInventory.Instance.HasDisk(requiredDisk))
        {
            bool playerHoldsNothing = PlayerInventory.Instance._heldDisks.Count == 0;
            bool puzzleHasProgress = puzzleController.HasAnyProgress;

            if (playerHoldsNothing && !puzzleHasProgress)
                emptyHanded?.Invoke();
            else
                partHanded?.Invoke();

            return;
        }

        PlayerInventory.Instance.RemoveDisk(requiredDisk);
        IsFilled = true;

        if (diskVisual != null)
            diskVisual.SetActive(true);

        puzzleController.OnDiskInserted(this);
    }

    public string GetPromptText()
    {
        return promptText;
    }

    public void ResetSlot()
    {
        IsFilled = false;
        if (diskVisual != null)
            diskVisual.SetActive(false);
    }
}