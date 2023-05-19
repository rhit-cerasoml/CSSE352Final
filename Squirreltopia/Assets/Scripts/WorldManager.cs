using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldManager : Singleton<WorldManager> {

    [SerializeField] public float lengthOfDay;
    [SerializeField] public Color dayColor;
    [SerializeField] public Color nightColor;
    private float time_of_day = 0;
    
    [SerializeField] public GameObject nutCountText;
    [SerializeField] public GameObject housingText;
    

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



    // Start is called before the first frame update
    void Start() {
        nuts = 30;
        nutCap = 30;
        squirrels = 0;
        squirrelCap = 0;
    }

    // Update is called once per frame
    void Update() {
        if(!paused){
            time_of_day += Time.deltaTime / lengthOfDay;
            time_of_day %= 1;
            Camera.main.backgroundColor = Color.Lerp(nightColor, dayColor, Cycle(time_of_day));
        }
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
