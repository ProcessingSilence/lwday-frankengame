using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashing : MonoBehaviour
{
    private Text text;

    private bool onOff;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(FlashingText());
    }

    IEnumerator FlashingText()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        onOff = !onOff;
        text.enabled = onOff;
    }
}
