using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BattleRuler;

public class BaseBehaviour : MonoBehaviour
{
    public int maxHP = 1000;
    public int hitPoint;
    public FORCE_SIDE forceSide;

    private BattleRuler BR;

    public SpriteRenderer HP_bar;

    // Start is called before the first frame update
    void Start()
    {
        BR = FindObjectOfType<BattleRuler>();
        hitPoint = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ammo"))
        {
            //Debug.Log(transform + " : I'm damaged");
            FirePower fpw = collision.gameObject.GetComponent<FirePower>();
            if (fpw.enabled == true)
            {
                fpw.enabled = false;
                Damaged(fpw.power);
                //Destroy(gameObject); // self destruction                
            }
            Destroy(collision.gameObject);
        }
    }

    public void Damaged(int power)
    {
        hitPoint -= power;
         
        // Change HP bar
        float xscale = (float)hitPoint / (float)maxHP;        
        float px = (1f - xscale) * (-0.5f);
        HP_bar.transform.localScale = new Vector3( xscale, 1f, 1f);
        HP_bar.transform.localPosition = new Vector3(px, 0f, 0f);

        if (hitPoint <= 0)
        {        
            BR.BattleOver(forceSide);
        }
    }
}
