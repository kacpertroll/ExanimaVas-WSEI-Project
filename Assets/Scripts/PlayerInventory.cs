using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public readonly List<DiskType> _heldDisks = new List<DiskType>();

    void Awake()
    {
        Instance = this;
    }

    public void AddDisk(DiskType disk)
    {
        if (!_heldDisks.Contains(disk))
            _heldDisks.Add(disk);
    }

    public void RemoveDisk(DiskType disk)
    {
        _heldDisks.Remove(disk);
    }

    public bool HasDisk(DiskType disk)
    {
        return _heldDisks.Contains(disk);
    }
}