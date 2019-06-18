using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingBehavior : MonoBehaviour
{
    [SerializeField]
    ExamDisplayer2 exd2;

    [SerializeField]
    GameObject kubus;

    [SerializeField]
    GameObject cube;
    string realPointedObj;
    string pointedObj;
    string currentPointing;
    float timeLeft = 3f;
    bool objPointed = false;
    bool timer = false;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), kubus.GetComponent<Collider>());
        Physics.IgnoreCollision(this.GetComponent<Collider>(), cube.GetComponent<Collider>());
    }


    private void OnTriggerStay(Collider other)
    {
        currentPointing = other.gameObject.name;
        if (pointedObj == null || pointedObj != currentPointing)
        {
            timer = true;
            pointedObj = currentPointing;
            timeLeft = 3f;
        }
        else
        {
            //Debug.Log(pointedObj);
            timeLeft -= Time.deltaTime;

        }
        if (timeLeft <= 0)
        {
            Debug.Log(exd2.GetSoal());
            if (exd2.GetSoal() == pointedObj)
            {
                Debug.Log("true " + pointedObj);
                exd2.SoalGenerator();
            }
            else
                Debug.Log("WRONG!! " + pointedObj);
                timeLeft = 3f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timeLeft = 3f;
    }

    IEnumerator ChooseTimer(string name)
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            timer = true;
        }
    }
}
