using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DungeonGridGeneration : MonoBehaviour
{
    private int gridX, gridY;
    [SerializeField] private int gridSpaceX, gridSpaceY;
    [SerializeField] private GameObject gridHolder;

    public int DungeonAmount;
    private int roomCount;

    public List<DungeonTile> tiles = new List<DungeonTile>();
    public List<DungeonTile> roomList = new List<DungeonTile>();

    [SerializeField] private Vector2 currentTile;
    [SerializeField] private Vector2 checkTile;
    private DungeonTile oldTile;
    public int lastdir = 0;

    //Animation
    [SerializeField] private Animator loadingScreen;
    private bool LoadingTerrain = false;


    void Start()
    {
        //Zorgt ervoor dat er niet meer kamers kunnen zijn dan gridtiles
        LoadingTerrain = true;
        gridX = DungeonAmount;
        gridY = DungeonAmount;
        roomCount = 0;

        FindObjectOfType<PlayerMovement>().StopWalking();

        TileGen();
    }

    private void TileGen()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                GameObject tmpTile = Instantiate(gridHolder, new Vector3(0 + gridSpaceX * x, 0 + gridSpaceY * y, 0), Quaternion.identity);
                tmpTile.GetComponent<DungeonTile>().x = x;
                tmpTile.GetComponent<DungeonTile>().y = y;
                tiles.Add(tmpTile.GetComponent<DungeonTile>());
                tmpTile.name = x + " | " + y;
                if (x == gridX / 2 && y == gridY / 2)
                {
                    roomList.Add(tmpTile.GetComponent<DungeonTile>());
                    tmpTile.GetComponent<DungeonTile>().occupied = true;
                }
            }
        }
        Debug.Log(roomList.Count);
        Vector2 center = new Vector2(gridX / 2, gridY / 2);
        currentTile = center;
        checkTile = currentTile;
        oldTile = roomList[0];
        StartCoroutine(startGeneration());
        StartTileGeneration(roomList[roomCount]);

    }
    
    void StartTileGeneration(DungeonTile tile)
    {
        int random = Random.Range(1, 5);

        switch (random)
        {
            case 1:
                //Up
                FindTile(checkTile.x , checkTile.y + 1, 1);
                break;
            case 2:
                //Right
                FindTile(checkTile.x + 1, checkTile.y, 2);
                break;
            case 3:
                //Down
                FindTile(checkTile.x , checkTile.y - 1, 3);
                break;
            case 4:
                //Left
                FindTile(checkTile.x - 1, checkTile.y, 4);
                break;
        }
    }

    public void FindTile(float x, float y, int dir)
    {

        foreach (var item in tiles)
        {
            if (x == item.x && y == item.y)
            {
                if (!item.occupied && roomCount <= DungeonAmount)
                {
                    roomCount++;
                    if (dir == 3)
                        oldTile.Bottom = true;
                    else if (dir == 4)
                        oldTile.Left = true;
                    else if (dir == 1)
                        oldTile.Top = true;
                    else if (dir == 2)
                        oldTile.Right = true;
                    item.roomCount = roomCount;
                    currentTile.x = item.x;
                    currentTile.y = item.y;
                    checkTile = currentTile;
                    item.occupied = true;
                    roomList.Add(item);
                    oldTile = item;
                }
            }
        }
        lastdir = dir;
        if (roomCount < DungeonAmount)
            StartTileGeneration(roomList[roomCount - 1]);
    }

    IEnumerator startGeneration()
    {
        //Startloading and creating the grid
        Debug.Log("Loadinggg");
        yield return new WaitForSeconds(0.5f);
        foreach (var item in roomList)
        {
            item.CheckNextRoom();
        }
        GameSetup();
        yield return new WaitForSeconds(0.3f);
        foreach (var item in roomList)
        {
            item.spawnTile();
        }
        for (int i = 0; i < tiles.Count; i++)
        {
            Destroy(tiles[i].gameObject);
        }
        tiles.Clear();
        yield return new WaitForSeconds(1);
        loadingScreen.SetBool("End", true);
        LoadingTerrain = false;
        FindObjectOfType<DungeonManager>().endRoom = roomList.Count -1;
        if (roomList.Count < DungeonAmount)
        {
            SceneManager.LoadScene(1);
        }
        //Done Loading --Start game--
        FindObjectOfType<PlayerMovement>().canWalk = true;
        Debug.Log("Dungeon Generated!!!");
    }

    private void GameSetup()
    {

        FindObjectOfType<PlayerMovement>().gameObject.transform.position = roomList[0].transform.position;
    }
}
