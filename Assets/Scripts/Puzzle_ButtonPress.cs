using UnityEngine;
using Unity.Cinemachine;

public class Puzzle_ButtonPress : MonoBehaviour
{
    [SerializeField] private PuzzleObject puzzle;
    [SerializeField] private Animator gateAnimator;

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && PuzzleCameraHandler.Instance._inPuzzle)
        {
            if (hit.collider.gameObject == gameObject)
                puzzle.Solve();
        }
    }

    void OnMouseEnter()
    {
        if (PuzzleCameraHandler.Instance._inPuzzle)
            GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    public void OpenGate()
    {
        gameObject.GetComponent<Animator>().SetTrigger("press");
        gateAnimator.SetBool("open", true);
    }
}