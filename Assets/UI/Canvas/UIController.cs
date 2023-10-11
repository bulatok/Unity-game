using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI hpText; // Reference to the Text element for HP
    public TextMeshProUGUI bulletText; // Reference to the Text element for bullet number
    public PlayerController playerController; // Reference to your PlayerController script
    public Image hp;
    public Image stamina;

    void Update()
    {
        hp.fillAmount = playerController.GetCurrentHP() / playerController.maxHp;
        stamina.fillAmount = playerController.GetCurrentStamina() / playerController.maxStamina;
        // Update the Text elements with current HP and bullet number
        hpText.text = "HP: " + playerController.GetCurrentHP();
        bulletText.text = "Bullets: " + playerController.GetBulletNum().ToString();
    }
}
