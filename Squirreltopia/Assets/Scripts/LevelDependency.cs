using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDependency : MonoBehaviour {
    [SerializeField] int[] levels;
    public void SendLevelUpdate(int level){
        for(int i = 0; i < levels.Length; i++){
            if(level >= levels[i]){
                transform.GetChild(i).gameObject.SetActive(true);
            }else{
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
