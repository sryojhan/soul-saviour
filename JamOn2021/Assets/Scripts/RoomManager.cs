using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public static class RoomManager
{

    public enum RoomTypes
    {
        none, special, lava, holes, turrets, info
    }
    public static void ManageRooms(Room[] rooms, int nSpecial, int roomSize, tileMap tiles, Tilemap tileMap)
    {
        int N = rooms.Length;

        for (int i = 0; i < N; i++)
        {
            int r = Random.Range(0, N);
            Room aux = rooms[r];
            rooms[r] = rooms[i];
            rooms[i] = aux;
        }

        for (int i = 0; i < nSpecial; i++)
        {
            rooms[i].type = RoomTypes.special;
        }
        for (int i = nSpecial; i < N; i++)
        {
            rooms[i].type = (RoomTypes)Random.Range(2, 5);
        }

        foreach (Room room in rooms)
            CreateRoom(room, roomSize, tiles, tileMap);
    }
    private static void CreateRoom(Room room, int size, tileMap tiles, Tilemap tilemap)
    {
        Vector2Int pos = new Vector2Int((int)room.transform.position.x, (int)room.transform.position.y);

        pos -= new Vector2Int(size / 2, size / 2);

        for (int x = pos.x; x < pos.x + size; x++)
        {
            for (int y = pos.y; y < pos.y + size; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tiles.ground);
            }
        }


        if (!room.bottom)
            for (int x = pos.x; x <= pos.x + size; x++)
                tilemap.SetTile(new Vector3Int(x, pos.y - 1, 0), tiles.wall);
        if (!room.up)
            for (int x = pos.x; x <= pos.x + size; x++)
                tilemap.SetTile(new Vector3Int(x, pos.y + size + 1, 0), tiles.wall);
        if (!room.left)
            for (int y = pos.y; y <= pos.y + size; y++)
                tilemap.SetTile(new Vector3Int(pos.x - 1,y, 0), tiles.wall);
        if (!room.right)
            for (int y = pos.y; y <= pos.y + size; y++)
                tilemap.SetTile(new Vector3Int(pos.x + size + 1, y, 0), tiles.wall);


        if (room.type == RoomTypes.special)
        {
            room.GetComponent<SpriteRenderer>().color = Color.red;
            return;
        }
    }

    static void print(object mssg)
    {
        Debug.Log(mssg.ToString());
    }
}
