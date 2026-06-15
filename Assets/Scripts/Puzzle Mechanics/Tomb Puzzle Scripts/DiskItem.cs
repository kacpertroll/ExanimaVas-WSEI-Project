using UnityEngine;

public class DiskItem : MonoBehaviour
{
    public int ID;

    private bool isCollected = false;

    public void Collect()
    {
        isCollected = true;
        gameObject.SetActive(false);

        PlayerInventory.Instance.AddDisk(this);
    }

    public void OnPlaced()
    {
        // blokujemy dalsze interakcje
        GetComponent<Collider>().enabled = false;
    }
}