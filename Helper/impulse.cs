using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impulse : MonoBehaviour
{
    public Vector3 impulseForce;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(impulseForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
