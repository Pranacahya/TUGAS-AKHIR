using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject kubus;

    [SerializeField]
    GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), kubus.GetComponent<Collider>());
        Physics.IgnoreCollision(this.GetComponent<Collider>(), cube.GetComponent<Collider>());
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Sphere")
        {
            this.GetComponent<CapsuleCollider>().radius = 0.22f;
        }

      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Sphere")
        {
            this.GetComponent<CapsuleCollider>().radius = 0.14f;
        }
    }
}
