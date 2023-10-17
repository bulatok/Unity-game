using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public virtual void Shoot(GameObject bulletPrefab, Vector3 pos) {
        Debug.Log(123);
    }
}

public class Rifle : Weapon {
    public override void Shoot(GameObject bulletPrefab, Vector3 pos) {
        Instantiate(bulletPrefab, pos, Quaternion.identity);
    }
}