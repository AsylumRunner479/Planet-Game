using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    public GravityAttractor attractor;
    private Transform mytransform;
    public Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid.constraints = RigidbodyConstraints.FreezeRotation;
        rigid.useGravity = false;
        mytransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        attractor.Attract(mytransform, rigid);
    }
}
