using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TriggerTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image ButtonImage;
    public  PlayerBehaviour Player;

    private void Start()
    {
        ButtonImage = GetComponent<Image>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Player.MachineGunOn();
        }

        if (Input.GetKeyUp("space"))
        {
            Player.MachineGunOff();
        }
    }
 
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Touch Begin : " + eventData);
        Player.MachineGunOn();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Touch Ended : " + eventData);
        Player.MachineGunOff();
    }


}
