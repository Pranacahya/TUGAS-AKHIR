using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ExamDisplayer2 : MonoBehaviour
{
    [SerializeField]
    PointingBehavior pb;

    [SerializeField]
    TextMeshPro tm;
    SoalClass sc;
    public int jumlahSoal = 2;
    string soal;
    public GameObject ap;
    GameObject[] test;
    List<Transform> angle = new List<Transform>();
    List<Transform> edge = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        jumlahSoal += 1;
        Transform[] temp = ap.gameObject.transform.GetChild(0).GetComponentsInChildren<Transform>();
        foreach(Transform tr in temp)
        {
            angle.Add(tr);
        }
        angle.RemoveAt(0);

        Transform[] temp2 = ap.gameObject.transform.GetChild(2).GetComponentsInChildren<Transform>();
        foreach (Transform tr in temp2)
        {
            edge.Add(tr);
        }
        edge.RemoveAt(0);
        SoalGenerator();
    }

    public void SoalGenerator()
    {
        jumlahSoal--;
        Debug.Log(jumlahSoal);
        string temp = "";
        int rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0:
                rand = Random.Range(0, angle.Count);
                soal = angle[rand].transform.name;
                temp = "Tentukan manakan sudut " + soal;
                break;
            case 1:
                rand = Random.Range(0, edge.Count);
                soal = edge[rand].transform.name;
                temp = "Tentukan manakan rusuk " + soal;
                break;
            case 2:
                break;
        }
        tm.text = temp;
    }

    public string GetSoal()
    {
        return soal;
    }
    // Update is called once per frame
    public void Exit()
    {
        Debug.Log("exist");
        if(jumlahSoal < 0)
            SceneManager.LoadScene("MainMenu");
    }
}
