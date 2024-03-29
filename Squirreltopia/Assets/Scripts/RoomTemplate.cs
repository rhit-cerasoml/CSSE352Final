using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomTemplate : MonoBehaviour {

    [SerializeField] public int height;
    [SerializeField] public int width;
    [SerializeField] public int cost;

    [SerializeField] public int jobID;
    [SerializeField] public int housingBuff;
    [SerializeField] public int jobCount;
    [SerializeField] public int storageBuff;


    public Room MakeRoom(int px, int py){
        Room room = new Room(px, py, width, () => {
            WorldManager.Instance.AddBuff(storageBuff, housingBuff);
            if(jobID != -1){
                for(int i = 0; i < jobCount; i++){
                    // ADD JOBS
                    if(jobID == 0){
                        WorldManager.Instance.ListJob(new Recruiter(px, py, width));
                    }
                    if(jobID == 1){
                        WorldManager.Instance.ListJob(new Collector(px, py, width));
                    }
                }
            }

        });
        return room;
    }

    public void Stamp(int x_offset, int y_offset, Transform destination){
        Transform grid = transform.GetChild(0);
        Tilemap foreground = grid.GetChild(0).gameObject.GetComponent<Tilemap>();
        Tilemap background = grid.GetChild(1).gameObject.GetComponent<Tilemap>();
        
        Tilemap D_foreground = destination.GetChild(0).gameObject.GetComponent<Tilemap>();
        Tilemap D_background = destination.GetChild(1).gameObject.GetComponent<Tilemap>();

        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                Vector3Int pos = new Vector3Int(x, y, 0);
                Vector3Int D_pos = new Vector3Int(x + x_offset, y + y_offset, 0);
                TileBase tile = foreground.GetTile(pos);
                if(tile != null){
                    D_foreground.SetTile(D_pos, tile);
                }
                tile = background.GetTile(pos);
                if(tile != null){
                    D_background.SetTile(D_pos, tile);
                }
            }
        }
    }

    public bool CanStamp(int x_offset, int y_offset, Transform destination){
        Tilemap D_foreground = destination.GetChild(0).gameObject.GetComponent<Tilemap>();
        Tilemap D_background = destination.GetChild(1).gameObject.GetComponent<Tilemap>();

        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                Vector3Int pos = new Vector3Int(x + x_offset, y + y_offset, 0);
                TileBase tile = D_background.GetTile(pos);
                if(tile != null){
                    return false;    
                }
                tile = D_foreground.GetTile(pos);
                if(tile != null){
                    return false;
                }
            }
        }
        return true;
    }
}
