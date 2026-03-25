using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private TextMeshProUGUI promptText;

    private CinemachineCamera _cam;
    private IInteractable _currentTarget;

    void Start() => _cam = GetComponentInChildren<CinemachineCamera>();

    void Update()
    {
        ScanForInteractable();
        if (Input.GetKeyDown(KeyCode.E) && _currentTarget != null)
            _currentTarget.Interact();
    }

    void ScanForInteractable()
    {
        Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableMask))
        {
            var target = hit.collider.GetComponent<IInteractable>();
            if (target != null)
            {
                _currentTarget = target;
                promptText.text = target.GetPromptText();
                promptText.gameObject.SetActive(true);
                return;
            }
        }

        _currentTarget = null;
        promptText.gameObject.SetActive(false);
    }
}