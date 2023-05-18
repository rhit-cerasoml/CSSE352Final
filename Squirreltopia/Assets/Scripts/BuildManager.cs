using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager> {

    [SerializeField] GameObject trunkRoom;
    [SerializeField] GameObject treePrefab;
    [SerializeField] GameObject[] roomPrefabs;
    
    GameObject _tree;

    // Start is called before the first frame update
    void Start() {
        
    }

    public void Setup(){
        
        _tree = Instantiate(treePrefab);
        _tree.transform.position = new Vector3(0, 0, 0);
        
        RoomTemplate room_template = trunkRoom.transform.GetComponent<RoomTemplate>();
        room_template.Stamp(0, 0, _tree.transform.GetChild(0));
    }
    
    public void Cleanup() {
        Destroy(_tree);
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(worldPosition);
        }
    }

    public float GetMaxCameraHeight(){
        return 15;
    }
    public float GetMaxCameraLeft(){
        return -15;
    }
    public float GetMaxCameraRight(){
        return 15;
    }
}
