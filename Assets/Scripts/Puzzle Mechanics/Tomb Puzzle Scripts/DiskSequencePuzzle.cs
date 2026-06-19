using System.Collections.Generic;
using UnityEngine;

public class DiskSequencePuzzle : MonoBehaviour
{
    [SerializeField] private PuzzleObject puzzle;
    [SerializeField] private DiskSlotInteractable[] slotsInCorrectOrder;
    public bool HasAnyProgress => _insertedOrder.Count > 0;

    private readonly List<DiskSlotInteractable> _insertedOrder = new List<DiskSlotInteractable>();

    public void OnDiskInserted(DiskSlotInteractable slot)
    {
        _insertedOrder.Add(slot);

        int index = _insertedOrder.Count - 1;

        // sprawdzamy dopiero gdy WSZYSTKIE sloty s¹ zape³nione
        if (_insertedOrder.Count < slotsInCorrectOrder.Length)
            return;

        if (IsCorrectSequence())
        {
            puzzle.Solve();
        }
        else
        {
            ResetPuzzle();
        }
    }

    private bool IsCorrectSequence()
    {
        for (int i = 0; i < slotsInCorrectOrder.Length; i++)
        {
            if (_insertedOrder[i] != slotsInCorrectOrder[i])
                return false;
        }
        return true;
    }

    private void ResetPuzzle()
    {
        foreach (var slot in slotsInCorrectOrder)
        {
            if (slot.IsFilled)
                PlayerInventory.Instance.AddDisk(slot.RequiredDisk);

            slot.ResetSlot();
        }

        _insertedOrder.Clear();
    }
}