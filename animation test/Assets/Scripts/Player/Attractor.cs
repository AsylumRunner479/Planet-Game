using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public static List<Attractor> Attractors;
    const float G = 0.001f;
    public Rigidbody rb;
    public bool Living, top;
    public Transform body;
    public float jumpForce = 1f;
    public float jumptimer = 1f;
    
    private void Start()
    {
        body = transform;
    }
    private void FixedUpdate()
    {
        
        foreach (Attractor attractor in Attractors)
        {
            if (attractor != this)
            {
                if (attractor.tag != "top")
                {
                    Attract(attractor, true);
                }
                else
                {
                    Attract(attractor, false);
                }
            }
            
        }
    }
    void OnEnable()
    {
        if(Attractors == null)
        {
            Attractors = new List<Attractor>();
        }
        Attractors.Add(this);    
    }
    private void OnDisable()
    {
        Attractors.Remove(this);
    }
    void Attract(Attractor objToAttract, bool positive)
    {
        Rigidbody rbToAttract = objToAttract.rb;
        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;
        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        
      
        Vector3 force = direction.normalized * forceMagnitude;
        if (!Living && Input.GetButtonDown("Jump"))
        {
            rbToAttract.AddForce(-direction);
            
        }
        if (positive)
        {
            rbToAttract.AddForce(force);
        }
        else
        {
            rbToAttract.AddForce(-force);
        }    
        
        
        
    }
}
