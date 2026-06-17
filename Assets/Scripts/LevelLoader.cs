using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator transition;

    public float transitionTime = 1.0f;

    private void Start()
    {
        transition = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
    }

    public void LoadNextLevel(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        

        SceneManager.LoadScene(sceneIndex);
    }
}
