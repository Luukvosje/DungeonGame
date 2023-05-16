using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public List<GameObject> SpikeObjects = new List<GameObject>();

    private void Start()
    {
        foreach (var spikes in SpikeObjects)
        {
            spikes.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GetComponentInParent<DungeonCameraFollow>().roomCount > 0)
        foreach (var spike in SpikeObjects)
        {
            spike.SetActive(true);
        }
    }
}
