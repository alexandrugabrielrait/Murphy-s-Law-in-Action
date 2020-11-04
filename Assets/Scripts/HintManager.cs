using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public const int MAX_HINTS = 2;
    public TMP_Text[] hintTexts = new TMP_Text[MAX_HINTS];
    Hint[] hints = new Hint[MAX_HINTS];

    private void Start()
    {
        for (int i = 0; i < MAX_HINTS; ++i)
        {
            hints[i] = new Hint
            {
                text = "",
                key = KeyCode.None
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < MAX_HINTS; ++i)
        {
            if (!PauseMenu.gamePaused)
            {
                if (Input.GetKey(hints[i].key))
                {
                    hints[i].text = "";
                    hints[i].key = KeyCode.None;
                }
                hintTexts[i].text = hints[i].text;
            }
            else
                hintTexts[i].text = "";
        }
    }

    public void SetHint(int index, Hint hint)
    {
        hints[index] = hint;
    }

    public void Clear()
    {
        for (int i = 0; i < MAX_HINTS; ++i)
        {
            hints[i].text = "";
            hints[i].key = KeyCode.None;
        }
    }
}
