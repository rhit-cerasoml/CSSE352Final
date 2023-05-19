using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : Singleton<Dialog>
{
    private string to_print;
    private string printed;
    float speed;
    bool has_printed;
    public void ShowDialog(string s, float t)
    {
        to_print = s;
        speed = t / s.Length;
        printed = "";
        has_printed = false;
        StartCoroutine(PrintChars());
    }

    IEnumerator PrintChars()
    {
        while (printed.Length != to_print.Length)
        {
            printed = to_print.Substring(0, printed.Length + 1);
            yield return new WaitForSeconds(speed);
        }
        has_printed = true;
    }

    public void SkipDialog()
    {
        printed = to_print;
        has_printed = true;
    } 
}
