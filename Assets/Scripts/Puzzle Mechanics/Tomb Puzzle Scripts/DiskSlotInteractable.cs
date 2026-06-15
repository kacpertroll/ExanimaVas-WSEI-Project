using UnityEngine;

public class DiskSlotInteractable : MonoBehaviour
{
    [SerializeField] private DiskSlot slot;

    public void Interact()
    {
        var disk = PlayerInventory.Instance.GetDisk();

        if (disk != null)
        {
            slot.PlaceDisk(disk);
        }
        else
        {
            Debug.Log("Brak dysku");
        }
    }
}