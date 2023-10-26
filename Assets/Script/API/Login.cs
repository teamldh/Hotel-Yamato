using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private GameObject loginButton;
    [SerializeField] private GameObject logoutButton;
    [SerializeField] private TMP_Text nameAccount;

    [Header("Status")]
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Button lanjutButton;

    const string continueText = "\n(Press To Continue...)";
    private bool isLogged = false;
    string loginTime;

    // private void Awake() 
    // {
    //     TryAutoLogin();
    // }

    void OnApplicationQuit() 
    {    
        if(isLogged)
        {
            //post game log when application is closed without logging out
            StartCoroutine(PostGameLog());
        }
    }

    // private void TryAutoLogin()
    // {
    //     //Check if the player has logged in before through previousAccessToken validation
    //     if(APIManager.Instance.previousAccessToken != null)
    //     {
    //         try
    //         {
    //             StartCoroutine(PostLoginRequest(AutoLoginRequest()));
    //         }
    //         catch (System.Exception)
    //         {
    //             // Load local save data guest without any login
    //             SaveManager.instance.LoadSceneDataFromLocal();
    //         }
    //     }
    //     else
    //     {
    //         // Load local save data guest without any login
    //         SaveManager.instance.LoadSceneDataFromLocal();
    //         // Debug.Log("No previous access token found.");
    //     }
    // }

    /// <summary>
    /// Called when the login button is clicked and proccess the login form
    /// </summary>
    public void OnClickBtnLogin()
    {
        statusText.text = "Logging in...";
        lanjutButton.interactable = false;
        StartCoroutine(PostLoginRequest(LoginRequest()));
    }

    private UnityWebRequest LoginRequest()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormDataSection("username", usernameInput.text));

        // Convert password to MD5 hash
        byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(passwordInput.text);
        MD5 md5 = MD5.Create();
        byte[] hashBytes = md5.ComputeHash(passwordBytes);
        string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        formData.Add(new MultipartFormDataSection("password", hash));

        UnityWebRequest www = UnityWebRequest.Post(APIManager.baseURL + "login", formData);

        return www;
    }

    private UnityWebRequest AutoLoginRequest()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormDataSection("accessToken", APIManager.Instance.previousAccessToken));

        UnityWebRequest www = UnityWebRequest.Post(APIManager.baseURL + "login", formData);

        return www;
    }

    /// <summary>
    /// Post the login request to the server and wait for the response
    /// </summary>
    /// <param name="www">Form request to POST</param>
    private IEnumerator PostLoginRequest(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            statusText.text = "<size=56>" + www.error;
            statusText.text += continueText;
            lanjutButton.interactable = true;
            //dummy
            Account account = new Account("dummy", "dummy", "dummy");
            APIManager.Instance.SetAccount(account);

            // Try to read save data specific to the account (locally)
            SaveManager.instance.LoadSceneDataFromAccount(account.id);
            SaveManager.instance.currentAccountID = account.id;
            //MainMenuManager.instance.CheckProgress(); //Refresh Main menu continue progress button
            isLogged = true;
            SwitchButton();

            // Show username in the game to indicate that the player has logged in
            nameAccount.text = "Halo " + account.id;

            // Track login time for 'waktu_mulai' in game_log
            loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            throw new System.Exception(www.error);
        }
        else
        {
            string json = www.downloadHandler.text; //get the response as JSON string

            //Convert JSON string to Account object
            Account account = JsonUtility.FromJson<Account>(json);
            APIManager.Instance.SetAccount(account);

            statusText.text = "Login success!";
            statusText.text += continueText;
            lanjutButton.interactable = true;

            // Try to read save data specific to the account (locally)
            SaveManager.instance.LoadSceneDataFromAccount(account.id);
            SaveManager.instance.currentAccountID = account.id;
            //MainMenuManager.Instance.CheckProgress(); //Refresh Main menu continue progress button
            isLogged = true;
            SwitchButton();

            // Show username in the game to indicate that the player has logged in
            nameAccount.text = "Halo " + account.id;

            // Track login time for 'waktu_mulai' in game_log
            loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            yield return new WaitForSeconds(1f);
        }
    }

    #region Logout
    /// <summary>
    /// Called when the logout button is clicked and proccess the logout.
    /// (p.s: Add a new event to Activate the status message modal GameObject in the Button's OnClick() event)
    /// </summary>
    public void OnLogoutButtonClicked()
    {
        StartCoroutine(PostGameLog());
        isLogged = false;
        SwitchButton();

        // At this point the status message modal should be active
        statusText.text = "Logout success!";
        statusText.text += continueText;
        lanjutButton.interactable = true;
        nameAccount.text = "";
        APIManager.Instance.SetAccount(null);

        // Load local save data guest that has been saved if there is any
        SaveManager.instance.currentAccountID = "";
        SaveManager.instance.LoadSceneDataFromLocal();
        //MainMenuManager.Instance.CheckProgress(); //Refresh Main menu continue progress button
    }

    /// <summary>
    /// Post the game log to the server when the player logs out
    /// </summary>
    /// <returns></returns>
    private IEnumerator PostGameLog()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("id_game", APIManager.ID_GAME.ToString()));
        formData.Add(new MultipartFormDataSection("id_player", APIManager.Instance.account.id));
        formData.Add(new MultipartFormDataSection("waktu_mulai", loginTime));
        formData.Add(new MultipartFormDataSection("waktu_entry", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

        UnityWebRequest www = UnityWebRequest.Post(APIManager.baseURL + "game_log", formData);

        yield return www.SendWebRequest();

        // If the request is successful, read the response as JSON string
        // Here we assume that the server will return a log_id as a response that will be used to post the game event log
        // SUBJECT TO CHANGE, Sementara kurang lebih seperti ini
        // if(www.result == UnityWebRequest.Result.Success)
        // {
        //     string json = www.downloadHandler.text; //get the response as JSON string

        //     //Convert JSON string to GameLog object
        //     GameLog gameLog = JsonUtility.FromJson<GameLog>(json);

        //     // Post the game event log to the server
        //     StartCoroutine(GameEventLogRequest(SaveManager.Instance.eventLogs, gameLog.id_log));
        // }
        // else
        // {
        //     Debug.Log(www.error);
        // }
    }

    /// <summary>
    /// Post the game event log to the server when the player logs out
    /// </summary>
    /// <param name="eventLogs"></param>
    /// <returns></returns>
    /// SUBJECT TO CHANGE, Sementara kurang lebih seperti ini
    /// Iki gk efisien dadi haruse diubah, tapi haruse gk digae tiap request dipost dewe-dewe koyok ngene. Iki meloki ferry
    private IEnumerator GameEventLogRequest(List<EventLog> eventLogs, int id_log)
    {
        List<UnityWebRequest> wwws = new List<UnityWebRequest>();
        foreach(EventLog log in eventLogs)
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("id_game", APIManager.ID_GAME.ToString()));
            formData.Add(new MultipartFormDataSection("id_log", id_log.ToString()));
            formData.Add(new MultipartFormDataSection("no_event", log.no_event.ToString()));
            formData.Add(new MultipartFormDataSection("status_event", log.status.ToString()));

            UnityWebRequest www = UnityWebRequest.Post(APIManager.baseURL + "game_event_log", formData);
        }

        foreach(UnityWebRequest www in wwws)
        {
            yield return www.SendWebRequest();
        }
    }
    #endregion

    /// <summary>
    /// Switch the login and logout button active state
    /// </summary>
    private void SwitchButton()
    {
        loginButton.SetActive(!isLogged);
        logoutButton.SetActive(isLogged);
    }
}
