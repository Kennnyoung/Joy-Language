using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;

//probably using the following json format
//{
//    'userName':{
//        'password':'pwd',
//        'email':'mail'
//    },
//    'userName2':{
//        'password':'pwd',
//        'email':'mail'
//    },
//}


public class UserInfo {
    public string userName;
    public string password;
    public string email;
}

public class UserLogin : MonoBehaviour
{
    [SerializeField] Button login;

    UserInfo myObject = new UserInfo();
    UserInfo myObject2 = new UserInfo();
    

    // Start is called before the first frame update
    void Start()
    {
        //print("test..");
        //myObject.userName = "test";
        //myObject.password = "test";
        //myObject.email = "test@mail";
        //string json = JsonUtility.ToJson(myObject);
        //print(json);
        //File.WriteAllText(@"./Assets/Scripts/userInfo.json", json);
        string input = File.ReadAllText(@"./Assets/Scripts/userInfo.json");
        //print(input);
        //myObject2 =  JsonUtility.FromJson<UserInfo>(input);
        //print(myObject2.userName);

        JsonTextReader reader = new JsonTextReader(new StringReader(input));
        while (reader.Read()) {
            if (reader.Value != null) {
                print("key: " + reader.TokenType + " Value: " + reader.Value);
            }
            else {
                print("Key: " + reader.TokenType);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
