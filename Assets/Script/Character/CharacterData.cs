using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class CharacterData 
{
    public string characterID;
    public string characterName;
    public Sprite avatarSprite;

    public CharacterData(string characterID, string characterName, Sprite avatarSprite)
    {
        this.characterID = characterID;
        this.characterName = characterName;
        this.avatarSprite = avatarSprite;
    }
}
