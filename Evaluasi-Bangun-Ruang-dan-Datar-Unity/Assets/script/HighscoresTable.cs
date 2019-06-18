using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoresTable : MonoBehaviour
{
    private Transform highScoreEntry;
    private Transform entryTemplate;
    private Transform entryContainer;
    private Transform test;
    private int currentPages;
    private List<Transform> entryPages;
    private List<UserClass> userclass;
    private void Awake()
    {
        currentPages = 0;
        entryPages = new List<Transform>();
        highScoreEntry = transform.Find("HighscoreTable");
        //Container tiap tabel
        entryContainer = highScoreEntry.Find("HighscoreEntryContainer");
        //Template header tabel
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        entryContainer.gameObject.SetActive(false);
        float templateHeight = 25f;
        int i = 0;
        int j = 0;
        foreach (UserClass u in ListUser.users)
        {
            if (i == 0 || i == 15)
            {
                test = Instantiate(entryContainer, highScoreEntry);
                test.gameObject.name = "HighscoreEntryContainer"+j;
                test.gameObject.SetActive(true);
                entryPages.Add(test);
                i = 0;
                j++;
            }
            Transform entryTransform = Instantiate(entryTemplate, test);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.Find("Number").GetComponent<Text>().text = ((i + 1) + (15 * (j-1))).ToString();
            entryTransform.Find("Name").GetComponent<Text>().text = u.nama;
            entryTransform.Find("2DScore").GetComponent<Text>().text = u.bangunDatarScore.ToString();
            entryTransform.Find("3DScore").GetComponent<Text>().text = u.bangunRuangScore.ToString();
            entryTransform.gameObject.SetActive(true);
            i++;
            for (int k = 1; k < entryPages.Count; k++)
            {
                entryPages[k].gameObject.SetActive(false);
            }
        }
    }

    public void Next()
    {
        Debug.Log("next");
        if (currentPages < entryPages.Count - 1)
        {
            currentPages++;
            entryPages[currentPages - 1].gameObject.SetActive(false);
            entryPages[currentPages].gameObject.SetActive(true);
        }
    }

    public void previous()
    {
        Debug.Log("prev");
        if (currentPages > 0)
        {
            currentPages--;
            entryPages[currentPages + 1].gameObject.SetActive(false);
            entryPages[currentPages].gameObject.SetActive(true);
        }
    }

    public void exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
 