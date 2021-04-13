using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomManager.RoomTypes type;

    public Room left;
    public Room right;
    public Room bottom;
    public Room up;
}
