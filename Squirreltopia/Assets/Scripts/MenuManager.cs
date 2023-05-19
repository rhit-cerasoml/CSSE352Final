using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager> {

    [SerializeField] public GameObject[] menus;
    [SerializeField] public GameObject buttonGroup;
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
            activeMenu = id;
        }else{
            activeMenu = -1;
        }
    }

    public void HideMenu(){
        ChangeMenu(-1);
        buttonGroup.SetActive(false);
    }

    public void ShowMenu(){
        buttonGroup.SetActive(true);
    }
}
