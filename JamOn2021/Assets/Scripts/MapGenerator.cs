using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Provisional")]
    public GameObject prefab;

    public int numberOfRooms = 5;
    public Transform initial_position;

    public Transform scenery;

    private void Start()
    {
        instantiateMap(createMap());
    }

    private List<Vector2> createMap()
    {
        List<Vector2> possibilities = new List<Vector2>();
        List<Vector2> rooms = new List<Vector2>();

        addAdjacent(possibilities, rooms, Vector2.zero);

        for (int i = 0; i < numberOfRooms; i++)
        {
            int r = Random.Range(0, possibilities.Count);

            rooms.Add(possibilities[r]);
            addAdjacent(possibilities, rooms, possibilities[r]);
            possibilities.RemoveAt(r);
        }

        return rooms;
    }

    private void addAdjacent(List<Vector2> list, List<Vector2> rooms, Vector2 vector)
    {
        avoidRepetitions(list, rooms, vector + Vector2.right);
        avoidRepetitions(list, rooms, vector + Vector2.left);
        avoidRepetitions(list, rooms, vector + Vector2.down);
        avoidRepetitions(list, rooms, vector + Vector2.up);
    }

    private void avoidRepetitions(List<Vector2> list, List<Vector2> rooms, Vector2 vector)
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

    private void instantiateMap(List<Vector2> roomList)
    {
        Dictionary<Vector2, Room> dic = new Dictionary<Vector2, Room>();

        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(prefab, roomList[i], Quaternion.identity, scenery);
        }
    }

    float t = 0;
    private void Update()
    {
        t += Time.deltaTime;

        if (t > 1)
        {
            for (int i = 0; i < scenery.childCount; i++) { Destroy(scenery.GetChild(i).gameObject); }
            instantiateMap(createMap());
            t = 0;
        }
    }
}
