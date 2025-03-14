using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DialogNode 
{
    public string nodeID;
    public string characterID;
    public string dialogText;
    public List<DialogChoice> choices;
    public string nextNodeID;
    public bool isEndNode;


    public DialogNode(string id, string character, string text, List<DialogChoice> dialogChoices, bool endNode = false)
    {
        nodeID = id;
        characterID = character;
        dialogText = text;
        choices = dialogChoices;
        isEndNode = endNode;
    }

    public DialogNode(string nodeID, string characterID, string dialogText, string nextNodeID, bool isEndNode)
    {
        this.nodeID = nodeID;
        this.characterID = characterID;
        this.dialogText = dialogText;
        this.choices = new List<DialogChoice>();
        this.nextNodeID = nextNodeID;
        this.isEndNode = isEndNode;
    }
}
