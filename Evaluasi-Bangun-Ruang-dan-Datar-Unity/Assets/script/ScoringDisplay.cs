using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringDisplay : MonoBehaviour
{
    [SerializeField]
    TextMeshPro tmScore;
    string text;
    // Start is called before the first frame update
    void Start()
    {
    }


    public void ShowScore(string text)
    {
        this.text = text;
        tmScore.gameObject.SetActive(true);
        StartCoroutine("PopUpTimer");
    }

    IEnumerator PopUpTimer()
    {
        float duration = 5f;
        float normalizedTime = 0f;
        while(normalizedTime <= 1f )
        {
            tmScore.text = text;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        tmScore.gameObject.SetActive(false);
    }
}
