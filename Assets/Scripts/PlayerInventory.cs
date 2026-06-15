using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    private List<DiskItem> disks = new List<DiskItem>();

    void Awake()
    {
        Instance = this;
    }

    public void AddDisk(DiskItem disk)
    {
        disks.Add(disk);
    }

    public DiskItem GetDisk()
    {
        if (disks.Count == 0) return null;

        DiskItem disk = disks[0];
        disks.RemoveAt(0);

        return disk;
    }
}