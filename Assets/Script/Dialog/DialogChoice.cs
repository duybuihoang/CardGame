using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogChoice 
{
    public string choiceText;
    public string nextNodeID;
    public int storyVariantFlag;

    public DialogChoice(string text, string nextNode, int flag = 0)
    {
        choiceText = text;
        nextNodeID = nextNode;
        storyVariantFlag = flag;
    }
}
