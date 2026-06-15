using UnityEngine;

public class DiskSlot : MonoBehaviour
{
    public bool HasDisk => currentDisk != null;
    public int CurrentDiskID => currentDisk != null ? currentDisk.ID : -1;

    private DiskItem currentDisk;

    [SerializeField] private Transform snapPoint;
    [SerializeField] private DiskPuzzleController puzzle;

    public void PlaceDisk(DiskItem disk)
    {
        if (currentDisk != null) return;

        currentDisk = disk;

        // snapowanie
        disk.transform.position = snapPoint.position;
        disk.transform.rotation = snapPoint.rotation;

        disk.OnPlaced();

        puzzle.TrySolve();
    }

    public void ClearSlot()
    {
        if (currentDisk != null)
        {
            Destroy(currentDisk.gameObject);
        }
    }
}