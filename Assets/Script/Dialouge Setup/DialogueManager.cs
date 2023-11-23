using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;

    [Header("Dialogue Choice UI")]
    [SerializeField] private GameObject[] choices;

    [Header("Load Globals JSON")]
    // For load and save variables on ink
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Typing Speed")]
    [SerializeField] private float typingSpeed;
    
    private TextMeshProUGUI[] choicesText;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private Coroutine displayLineCoroutine;

    private bool canContinueToNextLine = false;
    private bool hasChosenChoice = false;
    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";

    private DialogueVariables dialogueVariables;
    private InkExternalFunctions inkExternalFunctions;

    // public event Action OnDialogueStarted;
    public event Action OnDialogueLineDisplay;
    public event Action OnDialogueEnded;

    private Dictionary<string, Action<string>> externalFunctions = new Dictionary<string, Action<string>>();
    
    private void Awake() {
        if(instance != null){
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        inkExternalFunctions = new InkExternalFunctions();
    }

    public static DialogueManager GetInstance() 
    {
        return instance;
    }

    private void Start() {

        //Awal script jalan (game awal mulai)
        dialoguePanel.SetActive(false);
        continueIcon.SetActive(false);
        dialogueIsPlaying = false;

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
        if(!dialogueIsPlaying){
            return;
        }

        if(canContinueToNextLine 
            && currentStory.currentChoices.Count == 0 
            && PlayerControllerInputSystem.GetInstance().GetSubmitPress())
        {
            continueStory();
        }
    }

    public DialogueManager EnterDialogueMode(TextAsset inkJSON){
        if(dialogueIsPlaying || inkJSON == null)
        {
            Debug.LogWarning("Dialogue is already playing or inkJSON is null");
            return null;
        }

        PlayerControllerInputSystem.GetInstance().SetEnableInputUI(true);
        PlayerControllerInputSystem.GetInstance().SetEnableInputMovement(false);

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        continueIcon.SetActive(true);

        dialogueVariables.StartListening(currentStory);
        // if(emoteAnimator != null)
        // {
        //     inkExternalFunctions.Bind(currentStory, emoteAnimator);
        // }
        foreach(var externalFunction in externalFunctions)
        {
            currentStory.BindExternalFunction(externalFunction.Key, externalFunction.Value);
        }

        // reset portrait, layout, and speaker as default
        displayNameText.text = "???";

        //OnDialogueStarted?.Invoke();
        continueStory();

        return instance;
    }

    public DialogueManager EnterDialogueModeManually(TextAsset inkJSON){
        if(dialogueIsPlaying || inkJSON == null)
        {
            Debug.LogWarning("Dialogue is already playing or inkJSON is null");
            return null;
        }

        PlayerControllerInputSystem.GetInstance().SetEnableInputUI(true);
        PlayerControllerInputSystem.GetInstance().SetEnableInputMovement(false);

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        continueIcon.SetActive(true);

        dialogueVariables.StartListening(currentStory);
        // if(emoteAnimator != null)
        // {
        //     inkExternalFunctions.Bind(currentStory, emoteAnimator);
        // }
        foreach(var externalFunction in externalFunctions)
        {
            currentStory.BindExternalFunction(externalFunction.Key, externalFunction.Value);
        }

        // reset portrait, layout, and speaker as default
        displayNameText.text = "???";

        //OnDialogueStarted?.Invoke();
        continueStory();

        return instance;
    }

    /// <summary>
    /// Explicitly call this function to start the dialogue after calling EnterDialogueModeManually<br />
    /// (Example: DialogueManager.GetInstance().EnterDialogueModeManually(inkJSON).StartDialogue();<br />
    /// </summary>
    /// <returns></returns>
    public DialogueManager StartDialogue()
    {
        if(currentStory == null)
        {
            Debug.LogWarning("Cannot start dialogue because currentStory is null");
            return null;
        }
        continueStory();
        return instance;
    }

    /// <summary>
    /// Register external function to be used in ink
    /// Call this function before calling EnterDialogueMode
    /// </summary>
    /// <returns>Chainable DialogueManager</returns>
    public DialogueManager BindExternalFunction(string functionName, Action<string> function)
    {
        if(function != null)
        {
            externalFunctions.Add(functionName, function);
        }
        return instance;
    }

    public DialogueManager OnDialogueDone(Action<DialogueVariables> callback)
    {
        if(callback != null)
        {
            OnDialogueEnded += () => callback(dialogueVariables);
        }
        return instance;
    }

    private void continueStory(){
        if (currentStory.canContinue) 
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null) 
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue();
            // handle case where the last line is an external function
            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                StartCoroutine(ExitDialogueMode());
            }
            // otherwise, handle the normal case for continuing the story
            else 
            {
                // handle tags
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
        }
        else 
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line){
        // set the text to the full line, but set the visible characters to 0
        dialogueText.text = line;
        OnDialogueLineDisplay?.Invoke();
        dialogueText.maxVisibleCharacters = 0;
        // hide items while text is typing
        continueIcon.SetActive(false);
        HideChoice();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // if the submit button is pressed, finish up displaying the line right away
            if (PlayerControllerInputSystem.GetInstance().GetSubmitPress()) 
            {
                if(hasChosenChoice)
                {
                    hasChosenChoice = false;
                    continue;
                }
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag) 
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else 
            {
                //PlayDialogueSound(dialogueText.maxVisibleCharacters, dialogueText.text[dialogueText.maxVisibleCharacters]);
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSecondsRealtime(typingSpeed);
            }
        }

        // actions to take after the entire line has finished displaying
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

    private IEnumerator ExitDialogueMode(){
        yield return new WaitForSecondsRealtime(0.2f);

        dialogueVariables.StopListening(currentStory);
        inkExternalFunctions.Unbind(currentStory);

        foreach(var externalFunction in externalFunctions)
        {
            if(currentStory.TryGetExternalFunction(externalFunction.Key, out var ext))
            {
                currentStory.UnbindExternalFunction(externalFunction.Key);
            }
        }
        externalFunctions.Clear();

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        dialoguePanel.SetActive(false);
        continueIcon.SetActive(false);
        dialogueIsPlaying = false;
        dialogueText.text = "";

        OnDialogueEnded?.Invoke();

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
            else if (currentChoices.Count > 0) 
            {
                continueIcon.SetActive(false);
            }

            int index = 0;
            // enable and initialize the choices up to the amount of choices for this line of dialogue
            foreach(Choice choice in currentChoices) 
            {
                choices[index].gameObject.SetActive(true);
                choicesText[index].text = choice.text;
                // Rescale width to fit the text
                //choices[index].GetComponent<RectTransform>().sizeDelta = new Vector2(choicesText[index].preferredWidth + 100, choices[index].GetComponent<RectTransform>().sizeDelta.y);

                index++;
            }
            // go through the remaining choices the UI supports and make sure they're hidden
            for (int i = index; i < choices.Length; i++) 
            {
                choices[i].gameObject.SetActive(false);
            }

            EventSystem.current.SetSelectedGameObject(null);
            //StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice() 
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex) 
    {
        Debug.Log("Choice made: " + choiceIndex);
        if (canContinueToNextLine) 
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            // NOTE: The below two lines were added to fix a bug after the Youtube video was made
            PlayerControllerInputSystem.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script
            hasChosenChoice = true;
            continueStory();
        }
    }

    /// <summary>
    /// Get the current state of a variable in ink<br/>
    /// Useful for checking a variable before preparing for dialogue
    /// </summary>
    /// <param name="variableName">The name of the variable to get</param>
    /// <returns>A variable from ink story. <br/>
    /// To compare the value, use ToInt(), ToFloat(), ToString(), or ToBool() <br/>
    /// </returns>
    public Ink.Runtime.Object GetVariableState(string variableName) 
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null) 
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    /// <summary>
    /// Set the a variable state of a variable in ink <br />
    /// Usage: GetInstance().SetVariableState()["variableName"] = value;
    /// </summary>
    /// <returns>VariableState, This function cant be chained</returns>
    public VariablesState SetVariableState() 
    {
        return currentStory.state.variablesState;
    }

    // This method will get called anytime the application exits.
    // Depending on your game, you may want to save variable state in other places.
    public void OnApplicationQuit() 
    {
        dialogueVariables.SaveVariables();
    }

}
