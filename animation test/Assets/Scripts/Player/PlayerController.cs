using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15;
    private Vector3 moveDir;
    public Rigidbody rigid;
    public Transform body, planet;
    public GameObject top, bottom;
    // Start is called before the first frame update
    void Start()
    {
        body = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        
    }
    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        Vector3 topUp = (top.transform.position - planet.position).normalized;
        Vector3 botDown = (bottom.transform.position - planet.position).normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(topUp, -botDown) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);




    }
}
