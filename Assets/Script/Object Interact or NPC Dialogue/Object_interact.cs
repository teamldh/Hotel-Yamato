using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Object_interact : MonoBehaviour, interactable
{
    public bool IsInteractable { get; set; } = true;
    public TextAsset inkJSON;

    public void interact()
    {
        Talk();
    }
    private void Talk()
    {
        DialogueManager.GetInstance().BindExternalFunction("PindahScene", (sceneName) => PindahScene(sceneName));
        // DialogueManager.GetInstance().BindExternalFunction("TidakMauInteraksi", (interactable) => {
        //     IsInteractable = false;
        // });

        DialogueManager.GetInstance().EnterDialogueMode(inkJSON)
        .OnDialogueDone((variable) => 
        {
            // You can do something here when the dialogue is done
        });
    }

    public void PindahScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
