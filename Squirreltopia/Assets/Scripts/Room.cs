using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Room : MonoBehaviour {
    public int x, y;
    public Action apply;
    public Room(int px, int py, Action apply_source){
        x = px;
        y = py;
        apply = apply_source;
    }
}
