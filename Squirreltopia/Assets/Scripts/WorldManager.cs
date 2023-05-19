using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldManager : Singleton<WorldManager> {

    [SerializeField] public float lengthOfDay;
    [SerializeField] public Color dayColor;
    [SerializeField] public Color nightColor;
    private float time_of_day = 0;

    [SerializeField] public GameObject squirrelPrefab;
    private List<SquirrelAI> squirrelControllers;
    private List<Job> jobs;
    
    [SerializeField] public GameObject nutCountText;
    [SerializeField] public GameObject housingText;

    [SerializeField] public GameObject[] levelDependents;

    private int nut_count;
    private int nut_cap;
    private int squirrel_count;
    private int squirrel_cap;
    public int nuts {
        get{
            return nut_count;
        }
        set{
            nut_count = value;
            nutCountText.GetComponent<TMP_Text>().text = nut_count.ToString() + "/" + nut_cap.ToString();    
        }
    }
    public int nutCap {
        get{
            return nut_cap;
        }
        set{
            nut_cap = value;
            nutCountText.GetComponent<TMP_Text>().text = nut_count.ToString() + "/" + nut_cap.ToString();    
        }
    }
    public int squirrels {
        get{
            return squirrel_count;
        }
        set{
            squirrel_count = value;
            housingText.GetComponent<TMP_Text>().text = squirrel_count.ToString() + "/" + squirrel_cap.ToString();    
        }
    }
    public int squirrelCap {
        get{
            return squirrel_cap;
        }
        set{
            squirrel_cap = value;
            housingText.GetComponent<TMP_Text>().text = squirrel_count.ToString() + "/" + squirrel_cap.ToString();    
        }
    }


    public float recruitmentCredit = 1.0f;
    
    private void ApplyRecruit() {
        while(recruitmentCredit >= 1 && squirrel_count < squirrel_cap){
            GameObject new_squirrel = Instantiate(squirrelPrefab);
            squirrelControllers.Add(new_squirrel.GetComponent<SquirrelAI>());
            squirrels++;
            recruitmentCredit--;
        }
    }

    public void SafeAddNuts(int amt){
        int old = nut_count;
        if(nut_count + amt > nut_cap){
            nuts = Mathf.Max(nuts, nut_cap);
        }else{
            nuts += amt;
        }
    }

    public void AddRecruitCredit(float amt){
        recruitmentCredit+=amt;
    }

    public int level;
    public void SetLevel(int new_level){
        level = new_level;
        for(int i = 0; i < levelDependents.Length; i++){
            levelDependents[i].GetComponent<LevelDependency>().SendLevelUpdate(level);
        }
    }

    public void ListJob(Job j){
        jobs.Add(j);
    }

    public void AddBuff(int storageBuff, int housingBuff){
        Debug.Log("buff " + housingBuff);
        nutCap += storageBuff;
        squirrelCap += housingBuff;
    }

    // Start is called before the first frame update
    void Start() {
        nut_count = 40;
        nutCap = 0;
        squirrels = 0;
        squirrelCap = 0;
        SetLevel(0);
        squirrelControllers = new List<SquirrelAI>();
        jobs = new List<Job>();
    }

    // Update is called once per frame
    void Update() {
        if(!paused){
            time_of_day += Time.deltaTime / lengthOfDay;
            if(time_of_day > 1){
                time_of_day %= 1;
                ApplyRecruit();
            }
            Camera.main.backgroundColor = Color.Lerp(nightColor, dayColor, Cycle(time_of_day));
        }
    }

    public bool FullHousing(){
        return squirrel_count == squirrel_cap;
    }

    public bool FullBank(){
        return nut_count >= nut_cap;
    }

    public Job GrabJob(){
        foreach(Job job in jobs) {
            Debug.Log(FullHousing() + " " + job.IsAvailable());
            if(job.IsAvailable()) {
                return job;
            }
        }
        return null;
    }

    private float Cycle(float input){
        return (Mathf.Sin(Mathf.PI * 2.0f * input) + 1.0f) / 2.0f;
    }

    public bool paused = false;
    public void PauseGame(){
        paused = true;
    }

    public void ResumeGame(){
        paused = false;
    }

    public bool TrySpendNuts(int value){
        if(nuts >= value){
            nuts -= value;
            return true;
        }
        return false;
    }

}
