using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTile : MonoBehaviour
{
    public float x, y;
    public bool occupied;
    public int roomCount;

    public bool Top, Bottom, Left, Right;

    public GameObject TB, LR, LT, TR, LB, RB, T, R, B, L;
    private DungeonGridGeneration dgg;
    public void CheckNextRoom()
    {
        dgg = FindObjectOfType<DungeonGridGeneration>();
        //Debug.Log(roomCount + "+ " + dgg.roomList[roomCount - 1].roomCount);
        //if (dgg.roomList[roomCount-1].Top && dgg.roomList[roomCount-1].occupied)
        //    Bottom = true;
        //if (dgg.roomList[roomCount-1].Right && dgg.roomList[roomCount-1].occupied)
        //    Left = true;
        //if (dgg.roomList[roomCount-1].Bottom && dgg.roomList[roomCount-1].occupied)
        //    Top = true;
        //if (dgg.roomList[roomCount-1].Left && dgg.roomList[roomCount-1].occupied)
        //    Right = true;

        foreach (var item in dgg.tiles)
        {
            if (Top)
            {
                if (item.y == y + 1 && item.x == x)
                {
                    item.Bottom = true;
                    Debug.Log(item.gameObject + " -- bottom");
                }
            }
            if (Left)
            {
                if (item.y == y && item.x == x - 1)
                {
                    item.Right = true;
                    Debug.Log(item.gameObject + " -- right");
                }
            }
            if (Right)
            {
                if (item.y == y && item.x == x + 1)
                {
                    item.Left = true;
                    Debug.Log(item.gameObject + " -- left");
                }
            }
            if (Bottom)
            {
                if (item.y == y - 1 && item.x == x)
                {
                    item.Top = true;
                    Debug.Log(item.gameObject + " -- top");
                }
            }
        }
    }
    public void spawnTile()
    {
        GameObject tile = gameObject;
        if (!occupied)
            return;
        if (Top && Right)
            tile = Instantiate(TR, transform.position, Quaternion.identity);
        else if (Top && Bottom)
            tile = Instantiate(TB, transform.position, Quaternion.identity);
        else if (Left && Right)
            tile = Instantiate(LR, transform.position, Quaternion.identity);
        else if(Top && Left)
            tile = Instantiate(LT, transform.position, Quaternion.identity);

        else if(Bottom && Left)
           tile = Instantiate(LB, transform.position, Quaternion.identity);
        else if(Bottom && Right)
            tile = Instantiate(RB, transform.position, Quaternion.identity);

        else if(Bottom)
           tile =  Instantiate(B, transform.position, Quaternion.identity);
        else if(Top)
           tile =  Instantiate(T, transform.position, Quaternion.identity);
        else if(Right)
           tile =  Instantiate(R, transform.position, Quaternion.identity);
        else if(Left)
           tile =  Instantiate(L, transform.position, Quaternion.identity);
        tile.GetComponent<DungeonCameraFollow>().roomCount = roomCount;
        tile.GetComponentInChildren<DungeonRoom>().AssignRoomStates();
    }

}
