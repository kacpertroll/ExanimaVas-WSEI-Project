using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [SerializeField] private CinemachineCamera playerCam;
    [SerializeField] private float range = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private TextMeshProUGUI interactUI;
    [SerializeField] private string interactText;
    [SerializeField] private PuzzleCameraHandler puzzleCameraHandler;

    void Update()
    {
        ScanForInteractable();
    }

    void ScanForInteractable()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, range, interactableLayer))
        {
            var puzzle = hit.collider.GetComponent<PuzzleObject>();
            if (puzzle != null)
            {
                interactUI.text = interactText;
                interactUI.gameObject.SetActive(true);

                if (Input.GetKeyDown(interactKey))
                {
                    puzzle.Activate();
                }

                if (puzzleCameraHandler._inPuzzle)
                {
                    interactUI.gameObject.SetActive(false);
                }
                return;
            }
        }

        interactUI.gameObject.SetActive(false);
    }
}