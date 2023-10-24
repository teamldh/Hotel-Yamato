using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class login : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public Button loginButton;
    public Button guest;
    // Start is called before the first frame update

    public void Login(){
        string user = username.text;
        string pass = password.text;
        if(user == "admin" && pass == "admin"){
            Debug.Log("Login Success");
            Debug.Log("Welcome " + user);
            Debug.Log("Password " + pass);
        }else{
            Debug.Log("Login Failed");
            Debug.Log("Welcome " + user);
            Debug.Log("Password " + pass);
        }
    }
    public void Guest(){
        Debug.Log("Guest " + generateGuest());
    }

    public string generateGuest(){
        string character = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        string guest_name = "guest_";

        for(int i = 0; i < 5; i++){
            guest_name += character[Random.Range(0, character.Length)];
        }
        return guest_name;
    }
}
