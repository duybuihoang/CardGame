using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StoryVariant", menuName = "Dialog System/Story Variant", order = 1)]
public class StoryVariant : ScriptableObject
{
    public string variantName;
    public int variantID;
    public List<DialogNode> additionalNodes;

    public void IntergrateIntoDialogTree(Dictionary<string, DialogNode> dialogNodes)
    {
        foreach (var node in additionalNodes)
        {
            dialogNodes[node.nodeID] = node;
        }
    }
}
