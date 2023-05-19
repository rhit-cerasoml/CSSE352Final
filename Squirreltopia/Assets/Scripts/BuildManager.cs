using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildManager : Singleton<BuildManager> {

    [SerializeField] GameObject trunkRoom;
    [SerializeField] GameObject treePrefab;
    [SerializeField] GameObject[] roomPrefabs;

    [SerializeField] Color validPreview;
    [SerializeField] Color invalidPreview;
    
    GameObject _tree;

    private int buildTop = 3;

    // Start is called before the first frame update
    void Start() {
        
    }

    public void Setup(){
        
        _tree = Instantiate(treePrefab);
        _tree.transform.position = new Vector3(0, 0, 0);
        
        RoomTemplate room_template = trunkRoom.transform.GetComponent<RoomTemplate>();
        room_template.Stamp(0, 0, _tree.transform.GetChild(0));
        room_template.Stamp(0, 3, _tree.transform.GetChild(0));
        room_template.Stamp(0, 6, _tree.transform.GetChild(0));

        ShowPreview(2);
    }
    
    public void Cleanup() {
        Destroy(_tree);
    }

    // Update is called once per frame
    void Update() {

        if(preview_active){
            if(build_room_id == -1){
                HidePreview();
            }
            SnapPreview();
            ColorPreview();
        }

        if(!WorldManager.Instance.paused && Input.GetMouseButtonDown(0)) {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(worldPosition);
        }
    }

    public float GetMaxCameraHeight(){
        return 5 * buildTop;
    }
    public float GetMaxCameraLeft(){
        return -15;
    }
    public float GetMaxCameraRight(){
        return 15;
    }

    private bool preview_active = true;
    private int build_room_id = -1;
    private int px, py;
    public void ShowPreview(int id){
        build_room_id = id;
        ShowPreview();
    }
    public void ShowPreview(){
        if(build_room_id != -1){
            transform.GetChild(0).gameObject.SetActive(true);
            preview_active = true;
        }
    }
    public void HidePreview(){
        transform.GetChild(0).gameObject.SetActive(false);
        preview_active = false;
    }
    private void SnapPreview(){
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = (int)Mathf.Floor(worldPosition.x);
        int y = (int)Mathf.Floor(worldPosition.y / 3.0f) * 3;
        px = x;
        py = y;
        transform.GetChild(0).position = new Vector3(x + 0.5f, y + 0.5f, -3);
        RoomTemplate RT = roomPrefabs[build_room_id].GetComponent<RoomTemplate>();
        transform.GetChild(0).GetChild(1).localPosition = new Vector3Int(RT.width - 1, 0, 0);
        transform.GetChild(0).GetChild(2).localPosition = new Vector3Int(0, RT.height - 1, 0);
        transform.GetChild(0).GetChild(3).localPosition = new Vector3Int(RT.width - 1, RT.height - 1, 0);
    }
    private void ColorPreview(){
        if(CanPlaceOnPreview()){
            transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = validPreview;
            transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color = validPreview;
            transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().color = validPreview;
            transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().color = validPreview;
        }else{
            transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = invalidPreview;
            transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color = invalidPreview;
            transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().color = invalidPreview;
            transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().color = invalidPreview;
        }
    }
    private bool CanPlaceOnPreview(){
        RoomTemplate RT = roomPrefabs[build_room_id].GetComponent<RoomTemplate>();
        Tilemap map = _tree.transform.GetChild(0).GetChild(1).GetComponent<Tilemap>();
        bool valid_neighbor =   map.HasTile(new Vector3Int(px - 1, py, 0)) || 
                                map.HasTile(new Vector3Int(px - 1, py + 1, 0)) || 
                                map.HasTile(new Vector3Int(px - 1, py + 2, 0)) ||
                              map.HasTile(new Vector3Int(px + RT.width, py, 0)) || 
                              map.HasTile(new Vector3Int(px + RT.width, py + 1, 0)) || 
                              map.HasTile(new Vector3Int(px + RT.width, py + 2, 0));
        return  valid_neighbor && RT.CanStamp(px, py, _tree.transform.GetChild(0)) && py / 3 >= 2 && py / 3 < buildTop;
    }
    

}
