using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : Job {
    public Collector(int x, int y, int w) : base(x, y, w) {
        
    }

    public override IEnumerator GetTask(SquirrelAI owner){
        taken = true;
        // TODO
        IEnumerator walk = WalkTo(owner, sx + width / 2, sy);
        while(walk.Current == null){
            walk.MoveNext();
            yield return null;
        }
        Debug.Log("working!");
        yield return new WaitForSeconds(10.0f);
        Debug.Log("done working!");
        WorldManager.Instance.SafeAddNuts((int)Mathf.Floor(Random.Range(3,6)));
        taken = false;
        owner.FinishJob();
    }

    public override bool IsAvailable(){
        return !taken && !WorldManager.Instance.FullBank();
    }
}
