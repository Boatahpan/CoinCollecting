using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public float hpDecreaseAmount = 10f;
    public float invulnerabilityDuration = 2f;
    public float lastHitTime;
    // Start is called before the first frame update
    void Start()
    {
        lastHitTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //chk collider obj is player
        if (other.gameObject.CompareTag("Player")){
            //if the colliding obj is Plajyer, then ... damage player
            TryDamagePlayer();
        }
    }

    private void TryDamagePlayer()
    {
        //when try to damage player we have to chk first when the last damage was applied
        if(Time.time >= lastHitTime + invulnerabilityDuration) {
            //Damage player
            Debug.Log("damge applie!");
            lastHitTime = Time.time;
        }
    }
}
