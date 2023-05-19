using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruiter : Job {
    public Recruiter(int x, int y, int w) : base(x, y, w) {
        
    }

    public override IEnumerator GetTask(SquirrelAI owner){
        taken = true;
        // TODO
        Debug.Log("working!");
        yield return new WaitForSeconds(10.0f);
        Debug.Log("done working!");
        WorldManager.Instance.AddRecruitCredit(0.5f + Random.Range(0, 1));
        taken = false;
        owner.FinishJob();
    }

    public override bool IsAvailable(){
        return !taken && !WorldManager.Instance.FullHousing();
    }
}
