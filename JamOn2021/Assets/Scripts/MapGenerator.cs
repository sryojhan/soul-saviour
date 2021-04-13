using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Provisional")]
    public GameObject prefab;

    public GameObject initial_room;
    public int numberOfRooms = 5;
    public int numberOfSpecialRooms = 3;
    public Transform scenery;

    private void Start()
    {
        RoomManager.ManageRooms(instantiateRooms(createMap()), numberOfSpecialRooms);
    }

    private List<Vector2Int> createMap()
    {
        List<Vector2Int> possibilities = new List<Vector2Int>();
        List<Vector2Int> rooms = new List<Vector2Int>();

        addAdjacent(possibilities, rooms, Vector2Int.zero);

        for (int i = 0; i < numberOfRooms; i++)
        {
            int r = Random.Range(0, possibilities.Count);

            rooms.Add(possibilities[r]);
            addAdjacent(possibilities, rooms, possibilities[r]);
            possibilities.RemoveAt(r);
        }

        return rooms;
    }

    private void addAdjacent(List<Vector2Int> list, List<Vector2Int> rooms, Vector2Int vector)
    {
        avoidRepetitions(list, rooms, vector + Vector2Int.right);
        avoidRepetitions(list, rooms, vector + Vector2Int.left);
        avoidRepetitions(list, rooms, vector + Vector2Int.down);
        avoidRepetitions(list, rooms, vector + Vector2Int.up);
    }

    private void avoidRepetitions(List<Vector2Int> list, List<Vector2Int> rooms, Vector2Int vector)
    {
        bool contains = false;
        for (int i = 0; i < list.Count && !contains; i++)
        {
            contains = list[i] == vector;
        }

        for (int i = 0; i < rooms.Count && !contains; i++)
        {
            contains = rooms[i] == vector;
        }

        if (!contains) list.Add(vector);
    }

    private Room[] instantiateRooms(List<Vector2Int> roomList)
    {
        Dictionary<Vector2Int, Room> dic = new Dictionary<Vector2Int, Room>();
        Room[] rooms = new Room[roomList.Count];

        for (int i = 0; i < roomList.Count; i++)
        {
            Vector2Int v = roomList[i];
            var room = Instantiate(prefab, (Vector2)v, Quaternion.identity, scenery).GetComponent<Room>();
            rooms[i] = room;
            dic.Add(v, room);

            for (int c = 0; c < roomList.Count; c++)
            {
                Vector2Int p = v + Vector2Int.right;
                if(dic.ContainsKey(p))
                {
                    room.right = dic[p];
                    dic[p].left = room;
                }
                p = v + Vector2Int.up;
                if(dic.ContainsKey(p))
                {
                    room.bottom = dic[p];
                    dic[p].up = room;
                }
                p = v + Vector2Int.left;
                if (dic.ContainsKey(p))
                {
                    room.left = dic[p];
                    dic[p].right = room;
                }
                p = v + Vector2Int.down;
                if (dic.ContainsKey(p))
                {
                    room.bottom = dic[p];
                    dic[p].up = room;
                }
            }
        }
        return rooms;
    }
    
    //float t = 0;
        //t += Time.deltaTime;

        //if (t > 1)
        //{
        //    for (int i = 0; i < scenery.childCount; i++) { Destroy(scenery.GetChild(i).gameObject); }
        //    instantiateMap(createMap());
        //    t = 0;
        //}
}
