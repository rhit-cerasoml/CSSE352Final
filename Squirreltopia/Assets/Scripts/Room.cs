using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Room {
    public int x, y, width;
    public Action apply;
    public Room(int px, int py, int width, Action apply_source){
        x = px;
        y = py;
        apply = apply_source;
    }
}
