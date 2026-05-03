using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class PuzzleCameraHandler : MonoBehaviour
{
    public static PuzzleCameraHandler Instance { get; private set; }

    [Header("Cameras")]
    [SerializeField] private CinemachineCamera playerCamera;

    [Header("Exit Input")]
    [SerializeField] private KeyCode exitKey = KeyCode.Escape;

    [Header("Player Controllers")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerCamera _playerCam;

    private CinemachineCamera _activePuzzleCamera;
    public bool _inPuzzle;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Update()
    {
        if (_inPuzzle && Input.GetKeyDown(exitKey))
        {
            ExitPuzzle();
        }
    }

    public void EnterPuzzle(CinemachineCamera puzzleCamera)
    {
        if (_inPuzzle) return;

        _activePuzzleCamera = puzzleCamera;
        _activePuzzleCamera.Priority = playerCamera.Priority + 10;
        _inPuzzle = true;
        _playerCam.enabled = false;
        _playerMovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;

    }

    public void ExitPuzzle()
    {
        if (!_inPuzzle) return;

        _activePuzzleCamera.Priority = playerCamera.Priority - 10;
        _activePuzzleCamera = null;
        _inPuzzle = false;
        _playerCam.enabled = true;
        _playerMovement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}