using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [Header("Provisional")]
    public GameObject prefab;

    [SerializeField] GameObject[] torches;
    [SerializeField] GameObject[] enemies;

    public int numberOfRooms = 5;
    public int numberOfSpecialRooms = 6;
    public Transform scenery;

    public int tilesPerRoom = 10;

    [Header("Tiles")]
    public Tilemap mainTileMap;
    public Tilemap walls;

    public Tile ground;
    public Tile wall;
    private void Start()
    {
        var tileMaps = new tileMaps { background = mainTileMap, walls = walls };
        var tileMap = new tileMap { ground = ground, wall = wall };
        var rooms = RoomManager.ManageRooms(instantiateRooms(createMap(), out Room initial_room), numberOfSpecialRooms, tilesPerRoom, tileMap, tileMaps);
        RoomManager.CreateInitialRoom(initial_room, tilesPerRoom, tileMap, tileMaps);

        for (int i = 0; i < numberOfSpecialRooms; i++)
        {
            Instantiate(enemies[2], rooms[i].transform.position, Quaternion.identity);
        }

        GameObject.FindGameObjectWithTag("Player").transform.position = initial_room.transform.position;

        GameObject.FindGameObjectWithTag("Boss").transform.position = initial_room.transform.position;
        GameObject.FindGameObjectWithTag("Boss").SetActive(false);

        torches[0].transform.position = new Vector2(initial_room.transform.position.x - 2, initial_room.transform.position.y);
        torches[1].transform.position = new Vector2(initial_room.transform.position.x - 2, initial_room.transform.position.y - 2);
        torches[2].transform.position = new Vector2(initial_room.transform.position.x - 2, initial_room.transform.position.y + 2);
        torches[3].transform.position = new Vector2(initial_room.transform.position.x + 2, initial_room.transform.position.y);
        torches[4].transform.position = new Vector2(initial_room.transform.position.x + 2, initial_room.transform.position.y - 2);
        torches[5].transform.position = new Vector2(initial_room.transform.position.x + 2, initial_room.transform.position.y + 2);
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
        if (vector == Vector2Int.zero) return;

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

    private Room[] instantiateRooms(List<Vector2Int> roomList, out Room initial_room)
    {
        Dictionary<Vector2Int, Room> dic = new Dictionary<Vector2Int, Room>();
        Room[] rooms = new Room[roomList.Count];

        initial_room = Instantiate(prefab, scenery).GetComponent<Room>();

        dic.Add(Vector2Int.zero, initial_room);
        for (int i = 0; i < roomList.Count; i++)
        {
            Vector2Int v = roomList[i];
            var room = Instantiate(prefab, (Vector2)v * tilesPerRoom, Quaternion.identity, scenery).GetComponent<Room>();
            rooms[i] = room;


            int numEnemies = Random.Range(1, 2);

            for (int j = 0; j < numEnemies && room != initial_room; j++)
            {
                int rnd = Random.Range(0, enemies.Length - 1);
                Instantiate(enemies[rnd], room.transform.position, Quaternion.identity);
            }


            dic.Add(v, room);
            for (int c = 0; c < roomList.Count; c++)
            {
                Vector2Int p = v + Vector2Int.right;

                if (dic.ContainsKey(p))
                {
                    room.right = dic[p];
                    dic[p].left = room;
                }
                p = v + Vector2Int.up;
                if (dic.ContainsKey(p))
                {
                    room.up = dic[p];
                    dic[p].bottom = room;
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

        //for (int i = 0; i < numberOfSpecialRooms; ++i)
        //{
        //    int rndRoom;
        //    do
        //    {
        //        rndRoom = Random.Range(0, rooms.Length);
        //    }
        //    while (rooms[rndRoom] == initial_room);

        //    Instantiate(enemies[2], rooms[rndRoom].transform.position, Quaternion.identity);
        //}

        return rooms;
    }

    /*float t = 0;
    private void Update()
    {
        t += Time.deltaTime;

        if (t > 1)
        {
            for (int i = 0; i < scenery.childCount; i++) { Destroy(scenery.GetChild(i).gameObject); }

            mainTileMap.ClearAllTiles();
            walls.ClearAllTiles();
            Camera.main.GetComponent<ColorPalette>().ChangeColorHue();
            Start();
            t = 0;
        }
    }*/
}

public struct tileMaps
{
    public Tilemap background;
    public Tilemap walls;

}

public struct tileMap
{
    public Tile ground;
    public Tile wall;
}