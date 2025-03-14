using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private List<StoryVariant> storyVariants;
    private Dictionary<int, StoryVariant> variantMap = new Dictionary<int, StoryVariant>();
    private HashSet<int> activeVariants = new HashSet<int>();

    private void Awake()
    {
        foreach (var variant in storyVariants)
        {
            variantMap[variant.variantID] = variant;
        }

        DialogManager.Instance.OnDialogComplete += UpdateStoryVariants;
    }


    private void UpdateStoryVariants(HashSet<int> flags)
    {
        foreach (var flag in flags)
        {
            activeVariants.Add(flag);
        }

        CheckForEnding();
    }

    private void CheckForEnding()
    {
        // Example logic for determining endings
        if (activeVariants.Contains(1) && activeVariants.Contains(3))
        {
            TriggerEnding("GoodEnding");
        }
        else if (activeVariants.Contains(2) && activeVariants.Contains(4))
        {
            TriggerEnding("BadEnding");
        }
        else if (activeVariants.Contains(5))
        {
            TriggerEnding("SecretEnding");
        }
    }

    private void TriggerEnding(string endingName)
    {
        // Logic to trigger the appropriate ending
        Debug.Log($"Triggering ending: {endingName}");
        // This could load a new scene, play a cutscene, etc.
    }

}
