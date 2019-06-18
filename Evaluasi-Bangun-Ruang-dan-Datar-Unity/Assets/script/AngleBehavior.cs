using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject kubus;

    [SerializeField]
    GameObject cube;
    // Start is called before the first frame update
    private void Start()
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), kubus.GetComponent<Collider>());
        Physics.IgnoreCollision(this.GetComponent<Collider>(), cube.GetComponent<Collider>());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Sphere")
        {
            this.GetComponent<SphereCollider>().radius = 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Sphere")
        {
            this.GetComponent<SphereCollider>().radius = 0.2f;
        }
    }
}
