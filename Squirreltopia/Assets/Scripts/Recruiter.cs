using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruiter : Job {
    public Recruiter(int x, int y) : base(x, y) {
        
    }

    public override IEnumerator GetTask(SquirrelAI owner){
        // TODO
        yield return new WaitForSeconds(3.0f);
    }
}
