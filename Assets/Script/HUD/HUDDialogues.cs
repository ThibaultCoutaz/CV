﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDDialogues : HUDElement {

    public Text textDialogue;
    public Toggle wayDisplay;
    public Button next;
    public GameObject textInfo;

    public float timeWriteNormal = 0.05f;
    public float timeWriteSpeed = 0.0025f;

    private float timeWriteCurrent;

    private ManageDialogue scriptDialogue;
    private string str;
    private string text;

    public void Init(ManageDialogue script)
    {
        scriptDialogue = script;
        timeWriteCurrent = timeWriteNormal;
    }

    public void SetText(string _text)
    {
        text = _text;

        next.interactable = false;

        if (wayDisplay.isOn)
            StartCoroutine(AnimateText(_text));
        else
        {
            textDialogue.text = _text;
            next.interactable = true;
        }
    }

    public void NextText()
    {
        scriptDialogue.NextSpeach();
    }

    void Update()
    {
        if (InputManager.Instance.IsPressingSpace)
            timeWriteCurrent = timeWriteSpeed;
        else
            timeWriteCurrent = timeWriteNormal;

    }
    
    IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        str = "";
        while (i < strComplete.Length)
        {
            str += strComplete[i++];
            textDialogue.text = str;

            if (!wayDisplay.isOn)
            {
                textDialogue.text = text;
                i = strComplete.Length;
            }

            yield return new WaitForSeconds(timeWriteCurrent);
        }

        next.interactable = true;
    }

    public void OnToggleChange()
    {
        if (wayDisplay.isOn)
            textInfo.SetActive(true);
        else
            textInfo.SetActive(false);
    }

}
