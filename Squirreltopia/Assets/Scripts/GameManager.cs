using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    [SerializeField] public float lengthOfDay;
    [SerializeField] public Color dayColor;
    [SerializeField] public Color nightColor;
    private float time_of_day = 0;
    
    [SerializeField] public GameObject nutCountText;
    [SerializeField] public GameObject capacityText;
    

    private int nut_count;
    private int squirrel_count;
    private int squirrel_max_count;
    public int nuts {
        get{
            return nut_count;
        }
        set{
            nut_count = value;
            nutCountText.GetComponent<TMP_Text>().text = nut_count.ToString();    
        }
    }
    public int squirrels {
        get{
            return squirrel_count;
        }
        set{
            squirrel_count = value;
            capacityText.GetComponent<TMP_Text>().text = squirrel_count.ToString() + "/" + squirrel_max_count.ToString();    
        }
    }
    public int capacity {
        get{
            return squirrel_max_count;
        }
        set{
            squirrel_max_count = value;
            capacityText.GetComponent<TMP_Text>().text = squirrel_count.ToString() + "/" + squirrel_max_count.ToString();    
        }
    }



    // Start is called before the first frame update
    void Start() {
        nuts = 10;
        squirrels = 5;
        capacity = 10;
    }

    // Update is called once per frame
    void Update() {
        time_of_day += Time.deltaTime / lengthOfDay;
        time_of_day %= 1;
        Camera.main.backgroundColor = Color.Lerp(nightColor, dayColor, Cycle(time_of_day));
    }

    private float Cycle(float input){
        return (Mathf.Sin(Mathf.PI * 2.0f * input) + 1.0f) / 2.0f;
    }


}
