using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;

    [Header("Dialogue Choice UI")]
    [SerializeField] private GameObject[] choices;
    
    private TextMeshProUGUI[] choicesText;
    private Story currentStory;
    private bool dialogIsPlaying;
    private Coroutine displayLineCoroutine;

    private bool canContinueToNextLine = false;
    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";

    
    private void Awake() {
        if(instance != null){
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;
    }

    public static DialogueManager GetInstance() 
    {
        return instance;
    }

    private void Start() {

        //Awal script jalan (game awal mulai)
        dialoguePanel.SetActive(false);
        continueIcon.SetActive(false);
        dialogIsPlaying = false;

        // get all of the choices text (mengambil semua text choice)
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices) 
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }
    
    private void Update() {
        if(!dialogIsPlaying){
            return;
        }

        if(canContinueToNextLine 
            && currentStory.currentChoices.Count == 0 
            && PlayerControllerInputSystem.GetInstance().GetSubmitPress())
        {
            continueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON){
        currentStory = new Story(inkJSON.text);
        dialogIsPlaying = true;
        dialoguePanel.SetActive(true);
        continueIcon.SetActive(true);

        continueStory();

        PlayerControllerInputSystem.GetInstance().SetEnableInputUI(true);
        PlayerControllerInputSystem.GetInstance().SetEnableInputMovement(false);
    }

    private void continueStory(){
        if(currentStory.canContinue){

            if (displayLineCoroutine != null){
                StopCoroutine(displayLineCoroutine);
            }
            //dialogueText.text = currentStory.Continue();
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            HandleTags(currentStory.currentTags);     
        }
        else{
            ExitDialogueMode();
        }
    }

    private IEnumerator DisplayLine(string line){
        dialogueText.text = "";

        continueIcon.SetActive(false);
        HideChoice();
        canContinueToNextLine = false;

        foreach(char letter in line.ToCharArray()){

            if(PlayerControllerInputSystem.GetInstance().GetSubmitPress()){
                dialogueText.text = line;
                break;
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }

        continueIcon.SetActive(true);
        DisplayChoices();
        canContinueToNextLine = true;
    }

    private void HideChoice(){
        foreach(GameObject choice in choices){
            choice.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags){
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags) 
        {
            string[]splitTag = tag.Split(" ");
            List<string> newSplitTag = new List<string>(splitTag.Length);

            for(int i = 0; i < splitTag.Length; i++)
            {
                //Check if curreent subString is one of defined tag, if yes continue
                if(splitTag[i].Contains(SPEAKER_TAG) || splitTag[i].Contains(PORTRAIT_TAG) || splitTag[i].Contains(LAYOUT_TAG) || splitTag[i].Contains(AUDIO_TAG))
                {
                    newSplitTag.Add(splitTag[i]);
                    continue;
                }
                else
                {
                    //Merge the tag with the previous sub split tag in the list
                    newSplitTag[newSplitTag.Count - 1] += " " + splitTag[i];
                }
            }

            foreach(string remainingTag in newSplitTag)
            {
                ProcessTag(remainingTag.Split(':'));
            }
        }

        void ProcessTag(string[] splitTag)
        {
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            
            // handle the tag
            switch (tagKey) 
            {
                case SPEAKER_TAG:
                    if(tagValue.Equals("null"))
                    {
                        displayNameText.transform.parent.gameObject.SetActive(false);
                    }
                    else
                    {
                        displayNameText.transform.parent.gameObject.SetActive(true);
                        displayNameText.text = tagValue;
                    }
                    break;
                // case PORTRAIT_TAG:
                //     if(tagValue.Equals("null"))
                //     {
                //         portraitAnimator.transform.parent.gameObject.SetActive(false);
                //     }
                //     else
                //     {
                //         portraitAnimator.transform.parent.gameObject.SetActive(true);
                //         portraitAnimator.Play(tagValue);
                //     }
                //     break;
                // case LAYOUT_TAG:
                //     layoutAnimator.Play(tagValue);
                //     break;
                // case AUDIO_TAG: 
                //     if(tagValue.Equals("null"))
                //     {
                //         playAudioNextLine = false;
                //     }
                //     else
                //     {
                //         playAudioNextLine = true;
                //         SetCurrentAudioInfo(tagValue);
                //     }
                //     break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void ExitDialogueMode(){
        dialoguePanel.SetActive(false);
        continueIcon.SetActive(false);
        dialogIsPlaying = false;
        dialogueText.text = "";

        PlayerControllerInputSystem.GetInstance().SetEnableInputUI(false);
        PlayerControllerInputSystem.GetInstance().SetEnableInputMovement(true);
    }

    private void DisplayChoices() 
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices) 
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++) 
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice() 
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceindex) 
    {
        if(canContinueToNextLine){
            currentStory.ChooseChoiceIndex(choiceindex);
            PlayerControllerInputSystem.GetInstance().RegisterSubmitPressed();
            continueStory();
        }
    }

}
