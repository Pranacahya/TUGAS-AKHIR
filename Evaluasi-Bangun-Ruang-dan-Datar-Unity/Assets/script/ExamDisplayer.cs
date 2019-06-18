using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class ExamDisplayer : MonoBehaviour
{
    [SerializeField]
    Renderer myImage;

    [SerializeField]
    TMP_Text tm;

    [SerializeField]
    LineScoring ls;

    SoalClass presentSoal = new SoalClass();
    public int jumlahSoal = 2;
    List<SoalClass> soal = new List<SoalClass>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Texture2D text in Resources.LoadAll("Images/Soal/"))
        {
            SoalClass sC;
            if (text.name.Contains("Segitiga"))
            {
                sC = new SoalClass(text, "Segitiga");
                soal.Add(sC);
            }
            else if (text.name.Contains("PersegiPanjang"))
            {
                sC = new SoalClass(text, "PersegiPanjang");
                soal.Add(sC);
            }
            else
            {
                sC = new SoalClass(text, "Persegi");
                soal.Add(sC);
            }  
        }
        ShowExam();
    }

    public void ShowExam()
    {
        if (jumlahSoal > 0)
        {
            tm.text = jumlahSoal + "/10";
            int rand = Random.Range(0, soal.Count);
            presentSoal = soal[rand];
            myImage = this.GetComponent<Renderer>();
            myImage.material.mainTexture = presentSoal.GetShape();
        }
        else
        {
            tm.gameObject.SetActive(false);
        }
        jumlahSoal -= 1;
    }

    public string GetAnswer()
    {
        return presentSoal.GetAnswer();
    }

    public void Exit()
    {
        if(jumlahSoal <= 0 )
        {
            SceneManager.LoadScene("MainMenu");
        }
       
    }
}
