using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ExamDisplayer : MonoBehaviour
{
    [SerializeField]
    Renderer myImage;

    SoalClass presentSoal = new SoalClass();

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
        //presentSoal = gameObject.GetComponent<SoalClass>();
        ShowExam();
        //Texture examTexture = Resources.Load("Images/Soal/"+imageUrl) as Texture;
        //myImage.texture = examTexture;
        //myText = myTex.gameObject.GetComponent<Texture>();
        //myText.

    }

    public void ShowExam()
    {
        //Debug.Log("test");
        int rand = Random.Range(0, soal.Count);
        //Debug.Log(soal.Count);
        presentSoal = soal[rand];
        //Debug.Log(presentSoal.GetAnswer());
        myImage = this.GetComponent<Renderer>();
        //Debug.Log("test4");
        myImage.material.mainTexture = presentSoal.GetShape();
        //Debug.Log("test5");
        //jawabanText.text = temp.GetAnswer();
        //jawabanText.fontSize = 12;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public string GetAnswer()
    {
        return presentSoal.GetAnswer();
    }
}
