using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithJoystick : MonoBehaviour
{
    [SerializeField]
    private VirtualJoystick virtualJoystick;
    public float moveSpeedX = 10;
    public float moveSpeedY = 10;
    
    public float Xmin, Xmax, Ymin, Ymax;
    public float mx, my;

    private PlayerBehaviour player;
    private Rigidbody2D rigidbody2;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerBehaviour>();
        rigidbody2 = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = virtualJoystick.Horizontal();
        float y = virtualJoystick.Vertical();

        if (x == 0 && y == 0)
        {
            y = Input.GetAxis("Vertical");
            x = Input.GetAxis("Horizontal");

        }

        if (Input.GetKeyDown("b")) {
            //player.Bombing(x, y);
            player.Bombing2();
        }

        /*
        if (x < 0) player.playerSideDir = -1;
         else if (x > 0) player.playerSideDir = +1;
        */

        if (x != 0 || y != 0)
        {

            Vector3 v = new Vector3(x * moveSpeedX, y * moveSpeedY, 0) * Time.deltaTime;

            //transform.position += v;
            rigidbody2.AddForce(v);
            
            BoundaryCheck();
        }
    }

    void BoundaryCheck()
    {
        // boundary
        mx = transform.position.x;
        my = transform.position.y;

        mx = (mx < Xmin) ? Xmin : mx;
        mx = (mx > Xmax) ? Xmax : mx;

        my = (my < Ymin) ? Ymin : my;
        my = (my > Ymax) ? Ymax : my;

        transform.position = new Vector3(mx, my, 0);
    }

    public float Horizontal()
    {
        return virtualJoystick.Horizontal();
    }

    public float Vertical()
    {
        return virtualJoystick.Vertical();
    }

}
