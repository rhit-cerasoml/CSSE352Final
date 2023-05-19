using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelAI : MonoBehaviour {
    private Job current_job;
    void Start() {
        
    }

    void Update() {
        if(current_job == null){
            current_job = WorldManager.Instance.GrabJob();
            if(current_job != null){
                StartCoroutine(current_job.GetTask(this));
            }
        }
    }

    public void FinishJob(){
        current_job = null;
    }
}
