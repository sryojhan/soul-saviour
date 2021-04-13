using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoomManager
{
    public enum RoomTypes
    {
        none, special, lava, holes, turrets, info
    }
    public static void ManageRooms(Room[] rooms, int nSpecial)
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
            CreateRoom(room);
    }
    private static void CreateRoom(Room room)
    {
        if(room.type == RoomTypes.special)
        {
            room.GetComponent<SpriteRenderer>().color = Color.red;
            return;
        }

    }
}
