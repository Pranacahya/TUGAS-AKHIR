using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoalClass : MonoBehaviour
{
    Texture2D shape;
    string answer;

    List<GameObject> edge = new List<GameObject>();
    // Start is called before the first frame update
    public SoalClass()
    {

    }
    public SoalClass(Texture2D text, string ans)
    {
        this.shape = text;
        this.answer = ans;
    }
 
    public Texture2D GetShape()
    {
        return this.shape;
    }

    public string GetAnswer()
    {
        return this.answer;
    }

    public void SetAnswer(string answer)
    {
        this.answer = answer;
    }

    //public GameObject[] GetGameObject()
    //{
    //    SetChild();
    //    return edge;
    //}
}
