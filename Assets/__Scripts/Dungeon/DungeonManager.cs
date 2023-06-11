using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DungeonManager : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    public int currentRoom;
    public int endRoom;

    public List<GameObject> enemys = new List<GameObject>(); 
    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
