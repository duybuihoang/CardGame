using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogEditorWindow : EditorWindow
{
    private DialogData currentDialogData;
    private string dialogFileName = "newDialog";

    [MenuItem("Tools/Dialog System/Dialog Editor")]
    public static void showWindow()
    {
        GetWindow<DialogEditorWindow>("Dialog Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Dialog System Editor", EditorStyles.boldLabel);
        dialogFileName = EditorGUILayout.TextField("Dialog File Name", dialogFileName);

        if(GUILayout.Button("New Dialog"))
        {
            currentDialogData = new DialogData
            {
                nodes = new List<DialogNode>(),
                characters = new List<CharacterData>()
            };
        }

        if (GUILayout.Button("Save Dialog"))
        {
            if (currentDialogData != null)
            {
                string json = DialogParser.ConvertToJson(currentDialogData);
                System.IO.Directory.CreateDirectory("Assets/Resources/Dialogs");
                System.IO.File.WriteAllText($"Assets/Resources/Dialogs/{dialogFileName}.json", json);
                AssetDatabase.Refresh();
            }
        }

        if (GUILayout.Button("Load Dialog"))
        {
            TextAsset dialogJson = Resources.Load<TextAsset>($"Dialogs/{dialogFileName}");
            if (dialogJson != null)
            {
                currentDialogData = DialogParser.ParseFromJson(dialogJson.text);
            }
        }
    }
}
