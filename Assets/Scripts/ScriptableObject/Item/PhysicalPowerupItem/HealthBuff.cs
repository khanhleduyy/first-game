using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PhysicalPowerupItem/HealthBuff")]
public class HealthBuff : PhysicalPowerupItem
{
    public float amount;
    private float playerHealth;


    public override void Apply(GameObject player)
    {
        if((player.GetComponent<Player2>().health += amount) > 100f)
        {
            player.GetComponent<Player2>().health = 100f;
        }
        else
        {
            player.GetComponent<Player2>().health += amount;
        }
        
        
    }
}


