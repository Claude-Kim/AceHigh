using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //[SerializeField]    
    //private MovementWithJoystick MWJ;

    public GameObject mgShell;
    public GameObject bombShell;

    private Transform gun_pod;
    private Transform bomb_bay;

    public bool MachineGun;
    public float MGInterval=0.05f;
    private float MGDelay;
    public float BombInterval = 0.2f;
    private float BombDelay;

    public int playerSideDir; // left = -1, right = +1

    private Rigidbody2D rigidbody2;
    private Animator animator;
    //private playerPos;
    float vx, vy;

    public float mx, my;
    private MovementWithJoystick MWJ;

    private void Awake()
    {
        MWJ = GetComponent<MovementWithJoystick>();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = gameObject.GetComponent<Rigidbody2D>();                
        gun_pod = transform.Find("GunPod");
        bomb_bay = transform.Find("BombBay");
        animator = GetComponent<Animator>();

        animator.SetFloat("Move X", 0.1f); // Look right
        animator.SetFloat("Stick X", 0f);
        playerSideDir = 1;  // Look right
        MGDelay = 0;


    }

    // Update is called once per frame
    void Update()
    {
        WeaponDelayCheck();


        animator.SetFloat("Stick X", MWJ.Horizontal());
        
    }

    private void FixedUpdate()
    {
        mx = transform.position.x;
        my = transform.position.y;

        vx = rigidbody2.velocity.x;        
        if (vx != 0)
        {// for preventing idle flip
            animator.SetFloat("Move X", vx);
            playerSideDir = (vx > 0) ? 1 : -1;
        }
        if ( (my < MWJ.Ymin*0.9 && vy < 0) || (my > MWJ.Ymax*0.9 && vy > 0) )
        {
            rigidbody2.velocity = new Vector2(vx, 0f);
        } 

        vy = rigidbody2.velocity.y;
        if ( (mx < MWJ.Xmin*0.9 && vx < 0) || (mx > MWJ.Xmax*0.9 && vx > 0) )
        {
            rigidbody2.velocity = new Vector2(0f, vy);
        }
                
    }

    void WeaponDelayCheck()
    {
        if (MGDelay > 0)
            MGDelay -= Time.deltaTime;

        if (MachineGun && MGDelay <= 0)
        {
            MGDelay = MGInterval;
            ShootMG();
        }

        if (BombDelay > 0)
            BombDelay -= Time.deltaTime;
    }

    // ±â°üÃÑ ¹ß»ç
    private void ShootMG()
    {
        GameObject mg = Instantiate(mgShell, gun_pod.transform.position, mgShell.transform.rotation);
        MoveSideway sd = mg.GetComponent<MoveSideway>();
        sd.sideDir = playerSideDir;        
    }

    public void MachineGunOn()
    {
        //Debug.Log("MachineGun On");
        MachineGun = true;
    }

    public void MachineGunOff()
    {
        //Debug.Log("MachineGun Off");
        MachineGun = false;
    }

    public void Bombing(float vh, float vv)
    {

        //float vh = MWJ.Horizontal();
        //float vv = MWJ.Vertical();

        //Debug.Log("Bomb Bomb : " + vh + "  " + vv);
        if (BombDelay > 0) return;
              
        GameObject bm = Instantiate(bombShell, bomb_bay.transform.position, bombShell.transform.rotation);
        bm.GetComponent<Rigidbody2D>().AddForce(new Vector2(vh*200f,vv*200f));
        
        BombDelay = BombInterval;

        
    }

    public void Bombing2()
    {
        float vx = rigidbody2.velocity.x;
        float vy = rigidbody2.velocity.y;
        
        //Debug.Log("Bomb Bomb : " + vx + "  " + vy);
        if (BombDelay > 0) return;

        GameObject bm = Instantiate(bombShell, transform.position+(Vector3.down * 0.5f), bombShell.transform.rotation);
        bm.GetComponent<Rigidbody2D>().AddForce(new Vector2(vx * 50f, vy * 20f));

        BombDelay = BombInterval;


    }
}
