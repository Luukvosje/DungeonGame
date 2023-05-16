using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private float transitionTime;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void LoadNextLevel(int sceneID)
    {
        StartCoroutine(LoadLevel(sceneID));
    }

    private IEnumerator LoadLevel(int sceneid)
    {
        anim.SetTrigger("NextScene");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneid);
    }

}
