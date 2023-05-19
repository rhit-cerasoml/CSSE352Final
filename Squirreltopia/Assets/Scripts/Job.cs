using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Job {
    public int sx, sy, width;
    public bool taken;
    public Job(int x, int y, int w){
        sx = x;
        sy = y;
        width = w;
        taken = false;
    }
    public abstract IEnumerator GetTask(SquirrelAI owner);
    public abstract bool IsAvailable();
}
