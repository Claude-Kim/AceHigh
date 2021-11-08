using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject lockOn;
    private Transform target;
    private float turnRate = 150f;
    private float accuracy;
    private int power;

    public float thrust = 0.1f;
    public float burnOutTime = 10f;

    private Rigidbody2D rigidbody2;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody2 = gameObject.GetComponent<Rigidbody2D>();
        target = lockOn.transform;
    }


    void Start()
    {
        
        //rigidbody2.AddForce(Vector2.up);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        /*
        Vector3 dir = target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        */

        if (burnOutTime > 0)
        {
            Update_LookRotation();

            rigidbody2.AddRelativeForce(Vector2.up * thrust, ForceMode2D.Impulse);
            burnOutTime -= Time.deltaTime;
        }

        

    }

    private void Update_LookRotation()
    {
        
        Vector3 vectorToTarget = target.position - transform.position;
                
        Vector3 quaternionToTarget = Quaternion.Euler(0, 0, 0) * vectorToTarget;
               
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: quaternionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnRate * Time.deltaTime);
                
    }


}
