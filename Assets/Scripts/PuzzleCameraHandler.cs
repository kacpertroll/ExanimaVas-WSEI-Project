using Unity.Cinemachine;
using UnityEngine;

public class PuzzleCameraHandler : MonoBehaviour
{
    public static PuzzleCameraHandler Instance { get; private set; }

    [Header("Cameras")]
    [SerializeField] private CinemachineCamera fppCamera;

    [Header("Player References")]
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private MonoBehaviour playerMovement; // drag your movement script here

    [Header("Timing")]
    [SerializeField] private float transitionDuration = 0.75f;

    private const int PRIORITY_HIGH = 20;
    private const int PRIORITY_LOW = 10;

    private PuzzleBase _activePuzzle;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void EnterPuzzle(PuzzleBase puzzle)
    {
        if (_activePuzzle != null) return; // already in a puzzle
        _activePuzzle = puzzle;

        // Cinemachine handles the blend automatically based on priority
        puzzle.PuzzleCamera.Priority = PRIORITY_HIGH;
        fppCamera.Priority = PRIORITY_LOW;

        // Lock player
        playerInteraction.enabled = false;
        playerMovement.enabled = false;

        // Wait for camera to finish panning, then show UI
        Invoke(nameof(ShowPuzzleUI), transitionDuration);
    }

    public void ExitPuzzle()
    {
        if (_activePuzzle == null) return;

        _activePuzzle.Hide();
        _activePuzzle.PuzzleCamera.Priority = PRIORITY_LOW;
        fppCamera.Priority = PRIORITY_HIGH;

        var exiting = _activePuzzle;
        _activePuzzle = null;

        // Restore player after camera pans back
        Invoke(nameof(RestorePlayer), transitionDuration);
    }

    void ShowPuzzleUI() => _activePuzzle?.Show();

    void RestorePlayer()
    {
        playerInteraction.enabled = true;
        playerMovement.enabled = true;
    }
}