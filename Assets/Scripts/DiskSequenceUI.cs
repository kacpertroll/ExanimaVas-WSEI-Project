using TMPro;
using UnityEngine;

public class DiskSequenceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] positionTexts; // 3 pola, po jednym na pozycjê w sekwencji
    [SerializeField] private string emptyLabel = "-";

    public void ShowDisk(int positionIndex, DiskType disk)
    {
        if (positionIndex < 0 || positionIndex >= positionTexts.Length) return;
        positionTexts[positionIndex].text = disk.ToString();
    }

    public void ResetDisplay()
    {
        foreach (var text in positionTexts)
            text.text = emptyLabel;
    }
}