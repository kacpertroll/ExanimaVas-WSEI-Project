using UnityEngine;

public class DiskPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private DiskType diskType;
    [SerializeField] private string promptText = "Press E to pick up";

    public void Interact()
    {
        PlayerInventory.Instance.AddDisk(diskType);
        gameObject.SetActive(false);
    }

    public string GetPromptText()
    {
        return promptText;
    }
}