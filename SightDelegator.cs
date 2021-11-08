using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightDelegator : MonoBehaviour
{
    GroundUnitBehaviour gub;
    
    // Start is called before the first frame update
    void Start()
    {
        gub = GetComponentInParent<GroundUnitBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Engage
        if (gub.fighting == false)
            gub.OnSightEngage(collision);
    }
}
