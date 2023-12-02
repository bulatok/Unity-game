using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public int activeSlot;
    
    public int bulletNum = 30;
    
    public GameObject defaultBulletPrefab;
    public GameObject shotgunBulletPrefab;

    // For for shooting in a clamp
    public GameObject blasterBulletPrefab;
    
    public AudioSource gunSound;
    
    public AudioSource shotgunSound;
    
    private List<Weapon> weapons;
    
    private void Start() {
        weapons = new List<Weapon>();
        
        // Default weapons
        weapons.Add(new Rifle(defaultBulletPrefab, gunSound));
        weapons.Add(new ShotGun(shotgunBulletPrefab, shotgunSound));
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
    
    void Update()
    {
        int totalWeapons = weapons.Count;
        if(Input.GetAxis("Mouse ScrollWheel") != 0){
            if(Input.GetAxis("Mouse ScrollWheel") > 0){
                activeSlot = (activeSlot + 1) % totalWeapons;
                
            }
            if(Input.GetAxis("Mouse ScrollWheel") < 0){
                activeSlot -= 1;
                if (activeSlot <= 0) {
                    activeSlot *= -1;
                }
            }
        }
        
        // if (Input.GetKeyDown(KeyCode.Alpha1)) {
        //     inventory.activeSlot = Math.Min(inventory.weapons.Count, 1) - 1;
        // } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
        //     inventory.activeSlot = Math.Min(inventory.weapons.Count, 2) - 1;
        // } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
        //     inventory.activeSlot = Math.Min(inventory.weapons.Count, 3) - 1;
        // }
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
}
