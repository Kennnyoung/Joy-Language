using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

//probably using the following json format
//{
//    'userName':{
//        'password':'pwd',
//        'email':'mail'
//    },
//    'userName2':{
//        'password':'pwd',
//        'email':'mail'
//    }
//}


public class UserInfo {
    public string userName;
    public string password;
    public string email;
}

public class UserLogin : MonoBehaviour
{
    [SerializeField] Button login;
    [SerializeField] InputField userName;
    [SerializeField] InputField password;

    UserInfo myObject = new UserInfo();
    UserInfo myObject2 = new UserInfo();
    List<UserInfo> allUser = new List<UserInfo>();

    // Start is called before the first frame update
    void Start(){
        string input = File.ReadAllText(@"./Assets/Scripts/userInfo.json");
        //print(input);
        //myObject2 =  JsonUtility.FromJson<UserInfo>(input);
        //print(myObject2.userName);

        // add the login listener
        login.onClick.AddListener(buttonClick);

        JsonTextReader reader = new JsonTextReader(new StringReader(input));
        
        reader.Read();
        while (reader.Read()) {
            if (reader.Value == null) break;
            UserInfo temp = new UserInfo();
            // first is user name
            print("user name is " + reader.Value);
            temp.userName = reader.Value.ToString();

            // jump to next
            reader.Read();
            reader.Read();
            reader.Read();
            // this is user email
            print("user password is " + reader.Value);
            temp.password = reader.Value.ToString();

            // jump to next
            reader.Read();
            reader.Read();
            print("user email is " + reader.Value);
            temp.email = reader.Value.ToString();

            // out the obj
            reader.Read();
            allUser.Add(temp);
        }
    }

    void buttonClick() {
        print(userName.text);
        print(password.text);
        print(allUser.Count);
        for(int i = 0; i < allUser.Count; i++) {
            if (userName.text == allUser[i].userName && password.text == allUser[i].password) {
                loginSuccess();
            }
        }
    }

    void loginSuccess() {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update(){

    }
}
