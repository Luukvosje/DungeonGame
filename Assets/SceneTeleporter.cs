using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTeleporter : MonoBehaviour
{
    public int SceneId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<LevelLoader>().LoadNextLevel(SceneId);
        }
    }
}
