using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    [SerializeField]
    private Animator Transition;

    [SerializeField]
    private float t_time;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //play anim
        Transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(t_time);

        //load scene
        SceneManager.LoadScene(levelIndex);
    }


}
