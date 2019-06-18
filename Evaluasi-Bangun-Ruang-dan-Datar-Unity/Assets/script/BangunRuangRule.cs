using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangunRuangRule : MonoBehaviour
{

    GameObject spawner;

    [SerializeField]
    GameObject geometry;
    private bool destroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject [] go = GameObject.FindGameObjectsWithTag("structure");
        //foreach(GameObject temp in go)
        //{
        //    //Physics.IgnoreCollision(temp.GetComponent<Collider>(), Collider);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name =="Floor")
        {
            spawner = GameObject.Find("BangunRuangSpawner");
            //reset rotation and past velocity
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.gameObject.transform.position = spawner.transform.position;
            this.gameObject.transform.rotation = Quaternion.identity;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

    }
}
