using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BattleRuler;

public class GroundUnitBehaviour : MonoBehaviour
{
   
    public FORCE_SIDE forceSide;
    public float speed = 3f;
    private float hitPoint = 100f;

    public float lifeTime = 1f;
    public bool immortal = false;

    private int sideDir;
    public bool fighting = false;

    public float shootInterval = 3f;
    private float shootDelay = 0f;

    public GameObject shell;

    private BattleRuler BR;
    private Animator animator;
    private Transform Aim;
    //private DamageController DMC;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        Aim = transform.Find("Aim");

        BR = FindObjectOfType<BattleRuler>();
        if (BR == null)
        {
            Debug.Log("Can't find BattleRuler.");
        }

        /*
        DMC = GetComponent<DamageController>();
        if (DMC == null)
        {
            Debug.Log("Can't find DMC.");
        }*/

        sideDir = (forceSide == FORCE_SIDE.BLUE) ? 1 : -1;

    }

    void Update()
    {
        // auto destroy
        if (!immortal)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0)
            {
                UnitDead();
                //Destroy(gameObject);
            }
        }

        // AI attack when stop
        if (fighting)
        {
            shootDelay -= Time.deltaTime;
            if (shootDelay <= 0)
            {
                ShootGun();
                shootDelay = shootInterval;
            }
                        
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!fighting) Advance();
    }

    void Advance()
    {
        transform.Translate(new Vector3(sideDir, 0, 0) * Time.deltaTime * speed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ammo"))
        {
            //Debug.Log(transform + " : I'm damaged");
            FirePower fpw = collision.gameObject.GetComponent<FirePower>();
            if (fpw.enabled == true)
            {
                fpw.enabled = false;
                Damaged(fpw.power);
            }
            Destroy(collision.gameObject);

        } 
        
    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("ammo"))
        {
            Debug.Log(transform + " OnTriggerExit2D : " + collision);
            fighting = false;
            CancelInvoke("CheckNearEnemy");
        }
    }
    */

    public void OnSightEngage(Collider2D collision)
    {
       
        if (fighting == false)
        {
            //Debug.Log(gameObject + " : Enemy Insight. Engaging : " + collision);
            fighting = true;
            animator.SetBool("engaging", true);
            InvokeRepeating("CheckNearEnemy", 1f, 1f);
        }
        
       //     Debug.Log("onSightEngage " + gameObject + " --- " + collision);        
    }

    void ShootGun()
    {
        GameObject mg = Instantiate(shell, Aim.position, shell.transform.rotation);
        MoveSideway sd = mg.GetComponent<MoveSideway>();
        sd.sideDir = (forceSide == FORCE_SIDE.BLUE) ? 1 : -1;
        mg.layer = (forceSide == FORCE_SIDE.BLUE) ? 12 : 13;
    }

    public void Damaged(int power)
    {
        hitPoint -= power;
        if (hitPoint <= 0)
        {
            UnitDead();
        }
    }

    // fighting 이 false에서 true가 될 때, invokerepeat 됨. OnTriggerStay2D
    private void CheckNearEnemy()
    {
        LayerMask layerMask;
        // layerMask는 9(RedForce), 8(BlueForce)

        if (forceSide == FORCE_SIDE.BLUE)
        {
            layerMask = LayerMask.GetMask("RedForce");
        } else
        {
            layerMask = LayerMask.GetMask("BlueForce");
        }

        // 거리 5는 sight의 콜리젼 반지름
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * sideDir, 6.0f, layerMask);
        if (hit.collider != null)
        {
            //Debug.Log(gameObject + "Somebodyhere " + hit.collider);
            
        } else
        {
            Debug.Log(gameObject + "Clear. Move Advance.");
            fighting = false;
            animator.SetBool("engaging", false);
            CancelInvoke("CheckNearEnemy");            
        }
    }
    
    private void UnitDead()
    {
        if (forceSide == FORCE_SIDE.BLUE)
        {
            BR.Blue.Remove(gameObject);
        } else
        {
            BR.Red.Remove(gameObject);
        }

        Destroy(gameObject);
    }

}
