using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSideway: MonoBehaviour
{

    public int sideDir;
    public float speed = 3f;
    public float lifeTime = 1f;
    public bool immortal = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(sideDir, 0, 0) * Time.deltaTime * speed);

        if (!immortal)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0)
                Destroy(gameObject);
        }
    }
}
