using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialCombinationPuzzle : PuzzleBase
{
    [Header("Combination")]
    [SerializeField] private int[] solution = { 3, 7, 2 };

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI[] dialDisplays;   // one per dial
    [SerializeField] private Button[] upButtons;               // one per dial
    [SerializeField] private Button[] downButtons;             // one per dial
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button closeButton;

    private int[] _values;

    void Start()
    {
        _values = new int[solution.Length];

        for (int i = 0; i < solution.Length; i++)
        {
            int idx = i; // capture for lambda
            upButtons[idx].onClick.AddListener(() => AdjustDial(idx, +1));
            downButtons[idx].onClick.AddListener(() => AdjustDial(idx, -1));
        }

        confirmButton.onClick.AddListener(TryConfirm);
        closeButton.onClick.AddListener(ExitWithoutSolving);

        RefreshDisplays();
    }

    void AdjustDial(int index, int delta)
    {
        _values[index] = (_values[index] + delta + 10) % 10;
        RefreshDisplays();
    }

    void RefreshDisplays()
    {
        for (int i = 0; i < dialDisplays.Length; i++)
            dialDisplays[i].text = _values[i].ToString();
    }

    void TryConfirm()
    {
        for (int i = 0; i < solution.Length; i++)
            if (_values[i] != solution[i]) return;

        CompletePuzzle(); // inherited from PuzzleBase
    }

    protected override void OnPuzzleCompleted()
    {
        Debug.Log("Puzzle solved — trigger unlock here");
        // play audio, animate lock, etc.
    }
}