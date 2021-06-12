using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    public Text hpText = null;
    public Text dodgeTimerText = null;
    
    public void Refresh(int playerHP, float dodgeCooldownTimer)
    {
        DisplayHP(playerHP);
        DisplayDodgeCooldown(dodgeCooldownTimer);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void DisplayHP(int playerHP)
    {
        hpText.text = "HP : " + playerHP.ToString();
    }

    private void DisplayDodgeCooldown(float dodgeCooldownTimer)
    {
        if (dodgeCooldownTimer > 0)
        {
            dodgeTimerText.text = "Dodge (" + ((int)dodgeCooldownTimer).ToString() + "s)";
            dodgeTimerText.color = Color.red;
        }
        else
        {
            dodgeTimerText.text = "Dodge (space)";
            dodgeTimerText.color = Color.black;
        }
            
    }
}
