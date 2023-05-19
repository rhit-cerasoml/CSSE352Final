using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Job {
    public int sx, sy;
    public bool taken;
    public Job(int x, int y){
        sx = x;
        sy = y;
        taken = false;
    }
    public abstract IEnumerator GetTask(SquirrelAI owner);
}
