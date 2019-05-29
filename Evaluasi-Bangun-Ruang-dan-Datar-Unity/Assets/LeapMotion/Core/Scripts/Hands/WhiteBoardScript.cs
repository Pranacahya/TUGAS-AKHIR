using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WhiteBoardScript : MonoBehaviour
{
    public GameObject crosshair;
    private GameObject myCrosshair;
    private Collision handCollision;
    private bool touched = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(touched)
        {
            myCrosshair.transform.position = handCollision.gameObject.transform.position;
        }
    }
    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.name == "PinchPos")
        {
            handCollision = collision;
            touched = true;
            myCrosshair = Instantiate(crosshair, new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y, this.transform.position.z), Quaternion.identity);
            //Debug.Log(collision.gameObject.name);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name == "PinchPos")
        {
            touched = false;
            //Debug.Log("exit");
            Destroy(myCrosshair);
        }
    }
}
