using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyThis : MonoBehaviour
{
    private FileManager fm;
    private UserClass tempUser;
    private void Awake()
    {
        fm = new FileManager();
        DontDestroyOnLoad(transform.gameObject);

        string totalUser;
        string[] splitArray;
        string[] trimmedTotalUser;
   
        totalUser = fm.ReadString();
        trimmedTotalUser = totalUser.Split('\n');
        foreach(string s in trimmedTotalUser)
        {
            tempUser = new UserClass();
            splitArray = s.Split(',');
            if(splitArray[0]!= "")
            {
                tempUser.nama = splitArray[0];
                tempUser.bangunDatarScore = int.Parse(splitArray[1]);
                tempUser.bangunRuangScore = int.Parse(splitArray[2]);
                ListUser.users.Add(tempUser);
            }     
        }
        //Debug.Log(user);
    }
}
