using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : Singleton<DialogManager> {

    [SerializeField] GameObject textObject;

    private TMP_Text text;

    private string to_print;
    private string printed;

    float speed;
    bool has_printed;

    public void Start() {
        text = textObject.GetComponent<TMP_Text>();
        textObject.transform.parent.gameObject.SetActive(false);

        Debug.Log("Printing");

        ShowDialog("Test! Test!Test!Test!Test!Test!Test!Test!Test!Test!Test!Test!Test!Test!Test!Test!Test!Test!Test!Test!Test!Test!", 3);
    }

    public void ShowDialog(string s, float t) {
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
        textObject.transform.parent.gameObject.SetActive(false);
    }
}
