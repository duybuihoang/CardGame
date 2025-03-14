using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    #region Singleton Instance
    private static DialogManager _instance;
    public static DialogManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<DialogManager>();
                if(_instance == null)
                {
                    GameObject go = new GameObject("DialogManager");
                    go.AddComponent<DialogManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region UIElements
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Text dialogText;
    [SerializeField] private Text characterNameText;
    [SerializeField] private Image leftAvatarImage;
    [SerializeField] private Image righAvatarImage;
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private Button choiceButtonPrefab;
    #endregion

    #region Data
    private Dictionary<string, DialogNode> dialogNodes = new Dictionary<string, DialogNode>();
    private Dictionary<string, CharacterData> characters = new Dictionary<string, CharacterData>();
    private DialogNode currentNode;
    private bool isDialogActive = false;
    #endregion

    private HashSet<int> storyFlags = new HashSet<int>();

    public event Action<HashSet<int>> OnDialogComplete;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void InitializeDialog(List<DialogNode> nodes, List<CharacterData> characterList)
    {
        dialogNodes.Clear();
        foreach (var node in nodes)
        {
            dialogNodes[node.nodeID] = node;
        }

        characters.Clear();
        foreach (var character in characterList)
        {
            characters[character.characterID] = character;
        }
    }

    public void StartDialog(string startNodeID)
    {
        if(dialogNodes.TryGetValue(startNodeID, out currentNode))
        {
            isDialogActive = true;
            dialogPanel.SetActive(true);
            DisplayCurrentNode();
        }
        else
        {
            Debug.LogError($"Dialog node with ID {startNodeID} not found!");

        }
    }


    private void DisplayCurrentNode()
    {
        foreach (Transform child in choicesPanel.transform)
        {
            Destroy(child.gameObject);
        }

        dialogText.text = currentNode.dialogText;

        if(characters.TryGetValue(currentNode.characterID, out CharacterData character))
        {
            characterNameText.text = character.characterName;

            if(currentNode.characterID == "Player")
            {
                leftAvatarImage.sprite = character.avatarSprite;
                leftAvatarImage.color = Color.white;
                righAvatarImage.color = new Color(0.7f, 0.7f, 0.7f);
            }
            else
            {
                righAvatarImage.sprite = character.avatarSprite;
                righAvatarImage.color = Color.white;
                leftAvatarImage.color = new Color(0.7f, 0.7f, 0.7f);
            }
        }

        if(currentNode.choices == null && currentNode.choices.Count > 0)
        {
            choicesPanel.SetActive(true);

        }
    }

}
