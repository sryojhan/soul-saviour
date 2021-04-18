using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public static class RoomManager
{
    public static float wall_threshold = 0.85f;
    public enum RoomTypes
    {
        none, special, lava, holes, turrets, info
    }
    public static Room[] ManageRooms(Room[] rooms, int nSpecial, int roomSize, tileMap tiles, tileMaps tileMap)
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
            CreateRoomInterior(room, roomSize, tiles, tileMap.background);
        foreach (Room room in rooms)
            CreateRoomBorder(room, roomSize, tiles, tileMap.walls);

        return rooms;
    }

    public static void CreateInitialRoom(Room initial_room, int size, tileMap tiles, tileMaps tilemap)
    {
        CreateRoomInterior(initial_room, size, tiles, tilemap.background);
        CreateRoomBorder(initial_room, size, tiles, tilemap.walls);
    }

    
    private static void CreateRoomBorder(Room room, int size, tileMap tiles, Tilemap tilemap)
    {
        Vector2Int pos = new Vector2Int((int)room.transform.position.x, (int)room.transform.position.y);

        pos -= new Vector2Int(size / 2, size / 2);

        if (!room.bottom)
            for (int x = pos.x; x <= pos.x + size; x++)
                tilemap.SetTile(new Vector3Int(x, pos.y, 0), tiles.wall);
        else
        {
            if(Random.value > wall_threshold)
            {
                for(int x = pos.x; x < pos.x + size / 2 - 1; x++)
                    tilemap.SetTile(new Vector3Int(x, pos.y, 0), tiles.wall);

                for (int x = pos.x + size / 2 + 1; x <= pos.x + size ; x++)
                    tilemap.SetTile(new Vector3Int(x, pos.y, 0), tiles.wall);
            }
        }
        if (!room.up)
            for (int x = pos.x; x <= pos.x + size; x++)
                tilemap.SetTile(new Vector3Int(x, pos.y + size, 0), tiles.wall);
        else
        {
            if (Random.value > wall_threshold)
            {
                for (int x = pos.x; x < pos.x + size / 2 - 1; x++)
                    tilemap.SetTile(new Vector3Int(x, pos.y + size, 0), tiles.wall);

                for (int x = pos.x + size / 2 + 1; x <= pos.x + size; x++)
                    tilemap.SetTile(new Vector3Int(x, pos.y + size, 0), tiles.wall);
            }
        }
        if (!room.left)
            for (int y = pos.y; y <= pos.y + size; y++)
                tilemap.SetTile(new Vector3Int(pos.x, y, 0), tiles.wall);
        else
        {
            if (Random.value > wall_threshold)
            {
                for (int y = pos.y; y < pos.y + size / 2 - 1; y++)
                    tilemap.SetTile(new Vector3Int(pos.x, y, 0), tiles.wall);

                for (int y = pos.y + size / 2 + 1; y <= pos.y + size; y++)
                    tilemap.SetTile(new Vector3Int(pos.x, y, 0), tiles.wall);
            }
        }
        if (!room.right)
            for (int y = pos.y; y <= pos.y + size; y++)
                tilemap.SetTile(new Vector3Int(pos.x + size, y, 0), tiles.wall);
        else
        {
            if (Random.value > wall_threshold)
            {
                for (int y = pos.y; y < pos.y + size / 2 - 1; y++)
                    tilemap.SetTile(new Vector3Int(pos.x + size, y, 0), tiles.wall);

                for (int y = pos.y + size / 2 + 1; y <= pos.y + size; y++)
                    tilemap.SetTile(new Vector3Int(pos.x + size, y, 0), tiles.wall);
            }
        }
    }
    static void CreateRoomInterior(Room room, int size, tileMap tiles, Tilemap tilemap)
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

        //if (!room.bottom)
        //    for (int x = pos.x; x <= pos.x + size; x++)
        //        tilemap.SetTile(new Vector3Int(x, pos.y, 0), tiles.wall);
        //if (!room.up)
        //    for (int x = pos.x; x <= pos.x + size; x++)
        //        tilemap.SetTile(new Vector3Int(x, pos.y + size, 0), tiles.wall);
        //if (!room.left)
        //    for (int y = pos.y; y <= pos.y + size; y++)
        //        tilemap.SetTile(new Vector3Int(pos.x, y, 0), tiles.wall);
        //if (!room.right)
        //    for (int y = pos.y; y <= pos.y + size; y++)
        //        tilemap.SetTile(new Vector3Int(pos.x + size, y, 0), tiles.wall);
        //CreateRoomBorder(room, size, tiles, tilemap);
    }

    static void print(object mssg)
    {
        Debug.Log(mssg.ToString());
    }
}
