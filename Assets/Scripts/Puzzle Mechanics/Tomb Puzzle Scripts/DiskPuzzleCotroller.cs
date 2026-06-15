using UnityEngine;
using UnityEngine.Events;

public class DiskPuzzleController : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private DiskSlot[] slots;

    [Header("Correct Order (ID)")]
    [SerializeField] private int[] correctOrder;

    [Header("On Solved")]
    [SerializeField] private UnityEvent onSolved;

    private bool isSolved = false;

    public void TrySolve()
    {
        if (isSolved) return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].HasDisk) return;

            if (slots[i].CurrentDiskID != correctOrder[i])
                return;
        }

        Solve();
    }

    void Solve()
    {
        isSolved = true;

        Debug.Log("PUZZLE SOLVED");

        // usuwanie dysków (na demo)
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }

        onSolved?.Invoke();
    }

    public void Open()
    {
        Debug.Log("Essunia");
    }
}