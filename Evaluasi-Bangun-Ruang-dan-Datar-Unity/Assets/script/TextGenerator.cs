using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextGenerator : MonoBehaviour
{
    public TextMeshPro myName;
    public TextMeshPro readData;
    private int counter= 0;
    // Start is called before the first frame update
    string myText = "";
    // Update is called once per frame
    [SerializeField]
    FileManager fm;

    public void SetText(string name)
    {
        if (name == "Space")
        {
            myText = myText + " ";
        }
        else if(name == "Backspace")
        {
            if(myText.Length != 0)
                myText = myText.Substring(0, myText.Length - 1);
        }
        else if(name == "Enter")
        {
            fm.WriteString(myText);
            PresentUser.Name = myText;
        }
        else if(name == "Bangun Datar")
        {
            PresentUser.Name = myText;
            SceneManager.LoadScene("SoalBangunDatar");
        }
        else if(name == "Bangun Ruang")
        {
            PresentUser.Name = myText;
            SceneManager.LoadScene("SoalBangunRuang");
        }
        else if (name == "Papan Peringkat")
        {
            PresentUser.Name = myText;
            SceneManager.LoadScene("PapanPeringkat");
        }
        else if(name == "Keluar")
        {
            Application.Quit();
        }
        else
        {
            myText = myText + name;   
        }
        myName.text = myText;
    }

    public void BackSpace()
    {
        counter -= 1;
    }
}
