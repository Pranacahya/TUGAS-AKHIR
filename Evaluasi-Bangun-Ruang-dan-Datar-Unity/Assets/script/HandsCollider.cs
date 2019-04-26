using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsCollider : MonoBehaviour
{
    private bool status = false;
    // Start is called before the first frame update
    void Start()
    {
   

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetStatus());
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name == "Contact Fingerbone" || col.gameObject.name == "Contact Palm Bone")
        {
            //Debug.Log(col.gameObject.name);
            //SetStatus(true);
            //Debug.Log("Set to true");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //SetStatus(false);
        //Debug.Log("Out : "+collision.gameObject.name);
    }

    public void SetStatus(bool status)
    {
        this.status = status;
    }

    public bool GetStatus()
    {
        return this.status;
    }

}
