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
    public IEnumerator WalkTo(SquirrelAI owner, int x, int y){
        float delta = owner.speed;
        if(owner.transform.position.x > 0){
            delta = -delta;
        }
        while(Mathf.Abs(owner.transform.position.x) > 0.1){
            owner.transform.position = new Vector3(
                owner.transform.position.x + delta * Time.deltaTime,
                owner.transform.position.y,
                owner.transform.position.z);
                yield return null;
        }
        delta = owner.speed;
        if(y < owner.transform.position.y){
            delta = -delta;
        }
        while(Mathf.Abs(y - owner.transform.position.y) > 0.1){
            owner.transform.position = new Vector3(
                owner.transform.position.x,
                owner.transform.position.y + delta * Time.deltaTime,
                owner.transform.position.z);
                yield return null;
        }
        delta = owner.speed;
        if(owner.transform.position.x - x > 0){
            delta = -delta;
        }
        while(Mathf.Abs(x - owner.transform.position.x) > 0.1){
            owner.transform.position = new Vector3(
                owner.transform.position.x + delta * Time.deltaTime,
                owner.transform.position.y,
                owner.transform.position.z);
                yield return null;
        }
        yield return true;
    }
    public abstract IEnumerator GetTask(SquirrelAI owner);
    public abstract bool IsAvailable();
}
