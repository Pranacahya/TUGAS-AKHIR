using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
public class FileManager : MonoBehaviour
{
    public void WriteString(string name)
    {
        string path = "Assets/Resources/Leaderboard.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        string userRecord = name + ",";
        writer.WriteLine(userRecord);
        writer.Close();
        
        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path);
        //TextAsset asset = (TextAsset) Resources.Load("test");

        //Print the text from the file
        //Debug.Log(asset.text);
    }

    public string ReadString()
    {
        string path = "Assets/Resources/Leaderboard.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string dataText = reader.ReadToEnd();
        reader.Close();
        return dataText;
    }

}

