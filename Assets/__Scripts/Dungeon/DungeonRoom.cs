using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum roomState
{
    Fight,
    Tressure,
    Farming,
    Boss,
    Empty
}
public class DungeonRoom : MonoBehaviour
{

    [Header("DoorManagement")]
    public List<GameObject> SpikeObjects = new List<GameObject>();
    private bool doorsOpened = false;
    public bool roomcleared = false;


    [Header("DungeonSpecs")]
    public roomState roomState;
    public int roomCount;
    private DungeonManager manager;

    public List<GameObject> RoomEnemys = new List<GameObject>();
    public int needforKill;

    private void Awake()
    {
        manager = FindObjectOfType<DungeonManager>();
    }
    private void Start()
    {
        needforKill = 0;
        foreach (var spikes in SpikeObjects)
        {
            spikes.SetActive(false);
        }
        doorsOpened = false;
        if (roomState == roomState.Fight)
        {
            for (int i = 0; i < Random.Range(2, 5); i++)
            {
                Vector2 randomPos = new Vector2(Random.Range(transform.position.x - 2, transform.position.x + 2), Random.Range(transform.position.y - 2, transform.position.y + 2));
                GameObject enemy = Instantiate(manager.enemys[Random.Range(0, manager.enemys.Count)], randomPos, Quaternion.identity);
                RoomEnemys.Add(enemy);
                if (enemy.GetComponent<ZombieBehavior>())
                    enemy.GetComponent<ZombieBehavior>().speed = Random.Range(1f, 2f);
                needforKill++;
            }
        }
        foreach (var item in RoomEnemys)
        {
            item.SetActive(false);
        }
    }
    public void AssignRoomStates()
    {
        roomCount = GetComponentInParent<DungeonCameraFollow>().roomCount;
        if (roomCount == FindObjectOfType<DungeonGridGeneration>().DungeonAmount)
            roomState = roomState.Boss;
        else if (roomCount == 0)
            roomState = roomState.Empty;

    }


    //Door Management
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponentInParent<DungeonCameraFollow>().roomCount > 0 && collision.tag == "Player")
        {
            CloseDoors();
            FindObjectOfType<PlayerMovement>().currentRoom = this;
            if (roomState == roomState.Boss)
                FindObjectOfType<LevelLoader>().LoadNextLevel(0);
        }
    }

    //Remove the spikes
    public void OpenDoors()
    {
        foreach (var spike in SpikeObjects)
        {
            spike.SetActive(false);
        }
        doorsOpened = true;
        roomcleared = true;
    }

    //Place the spikes
    public void CloseDoors()
    {
        if (!doorsOpened && !roomcleared)
        {
            foreach (var spike in SpikeObjects)
            {
                spike.SetActive(true);
            }
            foreach (var item in RoomEnemys)
            {
                item.SetActive(true);
            }
        }
    }

    public void CheckKillCount()
    {
        needforKill--;
        if(needforKill <= 0)
        {
            OpenDoors();
        }
    }
}
