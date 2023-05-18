using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] public GameObject nutCountText;

    private int nut_count;
    public int nuts {
        get{
            return nut_count;
        }
        set{
            nut_count = value;
            nutCountText.GetComponent<Text>().text = nut_count.ToString();    
        }
    }

    // Start is called before the first frame update
    void Start() {
        nuts = 10;
    }

    // Update is called once per frame
    void Update() {
        
    }


}
