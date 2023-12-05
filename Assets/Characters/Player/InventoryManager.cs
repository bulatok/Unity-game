using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public int activeSlot;
    
    public int bulletNum = 30;
    
    public GameObject gunBulletPrefab;
    public GameObject shotgunBulletPrefab;
    public GameObject blusterBulletPrefab;
    
    public AudioSource gunSound;
    
    public AudioSource shotgunSound;

    public AudioSource blusterSound;
    
    private List<Weapon> weapons;
	
	// Guns
    [SerializeField] private SpriteRenderer shotgunSprite;
	[SerializeField] private SpriteRenderer gunSprite;
    [SerializeField] private SpriteRenderer blusterSprite;
    
    private void Start() {
        weapons = new List<Weapon>();
        
        // Default weapons
        weapons.Add(new ShotGun(shotgunBulletPrefab, shotgunSound));
		weapons.Add(new Rifle(gunBulletPrefab, gunSound));
		weapons.Add(new Bluster(blusterBulletPrefab, blusterSound));
        activeSlot = 0;
    }

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }
    
    public int GetBulletNum() {
        return bulletNum;
    }
    
    public void ReloadAmmo(int delta) {
        bulletNum += delta;
    }

    public string GetGunName()
    {
        Weapon weapon = weapons[activeSlot];
        return weapon.Name();
    }

	public void flipWeapon(bool val) {
		if (activeSlot == 0) {
			shotgunSprite.flipX = val;
		} else if (activeSlot == 1) {
			gunSprite.flipX = val;		
		} else if (activeSlot == 2) {
			blusterSprite.flipX = val;
		}
		
	}
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
			activeSlot = 0;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			activeSlot = 1;      
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			activeSlot = 2;      
        }
		switchOffGuns();
		switchOnGun(activeSlot);  
    }

	private int getGunNumber() {
		return weapons.Count;
	}

	private void switchOnGun(int idx) {
		int index = 0;
        foreach (Transform child in transform)
        {
			if (index == idx) {
				child.gameObject.SetActive(true);
			}
            index++;
        }
	}


    private void switchOffGuns()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void Shoot(Vector3 pos)
    {
        Weapon weapon = weapons[activeSlot];
        
        if (bulletNum < 0 || bulletNum < weapon.BuletNeeded() )
        {
            return;
        }
        weapon.Shoot(pos);
        bulletNum -= weapon.BuletNeeded();
    }

	public float getPlayerSpeedCoef() {
		// shotgun	
		if (activeSlot == 0) {
			return 0.5f;
		}

		// gun
		if (activeSlot == 1) {
			return 1f;
		}
		
		// bluster 
		if (activeSlot == 2) {
			return 0.6f;
		}

		return 1f;
	}
}
