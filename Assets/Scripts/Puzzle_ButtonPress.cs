using UnityEngine;
using Unity.Cinemachine;

public class Puzzle_ButtonPress : MonoBehaviour
{
    [SerializeField] private PuzzleObject puzzle;
    [SerializeField] private Animator gateAnimator;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && PuzzleCameraHandler.Instance._inPuzzle)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                    puzzle.Solve();
            }
        }
    }

    void OnMouseEnter()
    {
        if (PuzzleCameraHandler.Instance._inPuzzle)
            GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }

    public void OpenGate()
    {
        GetComponent<Animator>().SetTrigger("press");
        gateAnimator.SetBool("open", true);
    }
}