using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager> {

    [SerializeField] public GameObject[] menus;
    private int activeMenu = -1;

    void Start() {
        ChangeMenu(-1);
    }

    void Update() {
        
    }

    public void ChangeMenu(int id){
        for(int i = 0; i < menus.Length; i++){
            menus[i].SetActive(false);
        }
        if(id >= 0 && id != activeMenu){
            menus[id].SetActive(true);
        }
        activeMenu = id;
    }
}
