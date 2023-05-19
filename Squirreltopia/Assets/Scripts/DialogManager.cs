using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogManager : Singleton<DialogManager> {

    [SerializeField] GameObject textObject;
    [SerializeField] GameObject[] talkSprites;

    private TMP_Text text;

    private string to_print;
    private string printed;
    private int current_talkSprite;

    float speed;
    bool has_printed;

    public void Start() {
        text = textObject.GetComponent<TMP_Text>();
        textObject.transform.parent.gameObject.SetActive(false);

        ShowDialog("Hello! Welcome to SquirrelTopia, the new home for every squirrel!", 2, 0, () => {
            ShowDialog("We hope you'll help us build a great treehouse!", 1.5f, 0, () => {
                ShowDialog("We've given you a few nuts so why don't you start by building a house and a recruitment center?", 2.5f, 0, () => {
                
            });
            });
            });
    }

    public void Update() {
        if (Input.GetMouseButtonDown(0) && !has_printed) {
            SkipDialog();
        } else if (Input.GetMouseButtonDown(0) && has_printed) {
            CloseDialog();
        }
    }

    private Action last_callback;
    public void ShowDialog(string s, float t, int talkSprite, Action callback) {
        last_callback = callback;
        WorldManager.Instance.PauseGame();
        MenuManager.Instance.HideMenu();

        current_talkSprite = talkSprite;
        talkSprites[current_talkSprite].SetActive(true);

        to_print = s;
        speed = t / s.Length;
        printed = "";
        has_printed = false;
        StartCoroutine(PrintChars());
        textObject.transform.parent.gameObject.SetActive(true);
    }

    IEnumerator PrintChars() {
        while (printed.Length != to_print.Length) {
            printed = to_print.Substring(0, printed.Length + 1);
            text.text = printed;
            yield return new WaitForSeconds(speed);
        }
        has_printed = true;
    }

    public void SkipDialog() {
        printed = to_print;
        text.text = printed;
        has_printed = true;
    } 

    public void CloseDialog() {
        WorldManager.Instance.ResumeGame();
        MenuManager.Instance.ShowMenu();

        talkSprites[current_talkSprite].SetActive(false);
        textObject.transform.parent.gameObject.SetActive(false);
        last_callback();
    }
}
