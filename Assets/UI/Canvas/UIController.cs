using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI hpText; // Reference to the Text element for HP
    public TextMeshProUGUI bulletText; // Reference to the Text element for bullet number
    public PlayerController playerController; // Reference to your PlayerController script

    void Update()
    {
        // Update the Text elements with current HP and bullet number
        hpText.text = "HP: " + playerController.GetCurrentHP().ToString();
        bulletText.text = "Bullets: " + playerController.GetBulletNum().ToString();
    }
}
