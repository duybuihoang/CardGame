using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private string startNodeID;
    [SerializeField] private string dialogFileName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            LoadDialogData();
        }
    }

    public void TriggerDialog()
    {
        LoadDialogData();
    }

    private void LoadDialogData()
    {
        // Load dialog data from JSON and initialize the dialog system
        TextAsset dialogJson = Resources.Load<TextAsset>($"Dialogs/{dialogFileName}");
        if (dialogJson != null)
        {
            DialogData dialogData = JsonUtility.FromJson<DialogData>(dialogJson.text);
            DialogManager.Instance.InitializeDialog(dialogData.nodes, dialogData.characters);
            DialogManager.Instance.StartDialog(startNodeID);
        }
        else
        {
            Debug.LogError($"Dialog file {dialogFileName} not found!");
        }
    }
}
