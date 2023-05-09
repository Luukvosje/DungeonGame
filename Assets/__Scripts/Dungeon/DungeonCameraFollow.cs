using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCameraFollow : MonoBehaviour
{
    public int roomCount;
    private DungeonManager manager;

    private void Start()
    {
        manager = FindObjectOfType<DungeonManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.camera.Follow = gameObject.transform;
        manager.currentRoom = roomCount;
    }
}
