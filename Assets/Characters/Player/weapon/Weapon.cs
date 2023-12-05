using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public virtual int BuletNeeded()
    {
        return 0;
    }
    
    public virtual string Name()
    {
        return "";
    }

    public virtual void Shoot(Vector3 pos) {
        Debug.Log(123);
    }
}

public class Rifle : Weapon
{
    public GameObject BulletPrefab { get; set; }
    public AudioSource ShotSound { get; set; }

    public Rifle(GameObject pref, AudioSource sound)
    {
        BulletPrefab = pref;
        ShotSound = sound;
    }

    public override int BuletNeeded()
    {
        return 1;
    }
    
    public override string Name()
    {
        return "pistol";
    }


    public override void Shoot(Vector3 pos)
    {        
        ShotSound.Play(0);
        UnityEngine.Object.Instantiate(BulletPrefab, pos, Quaternion.identity);
    }
}

public class ShotGun : Weapon
{
    public GameObject BulletPrefab { get; set; }
    public AudioSource ShotSound { get; set; }


    public ShotGun(GameObject pref, AudioSource sound)
    {
        BulletPrefab = pref;
        ShotSound = sound;
    }
    
    public override string Name()
    {
        return "shotgun";
    }
    
    public override int BuletNeeded()
    {
        return 3;
    }
    
    public override void Shoot(Vector3 pos)
    {
        Vector3 upperPos = pos + new Vector3(0, 0.6f, 0);  // A bit upper
        Vector3 straightPos = pos;                         // Straight position (same as the original position)
        Vector3 downerPos = pos - new Vector3(0, 0.6f, 0); // A bit downer

        UnityEngine.Object.Instantiate(BulletPrefab, upperPos, Quaternion.identity);
        UnityEngine.Object.Instantiate(BulletPrefab, straightPos, Quaternion.identity);
        UnityEngine.Object.Instantiate(BulletPrefab, downerPos, Quaternion.identity);
        ShotSound.Play(0);
    }
}


public class Bluster : Weapon
{
    public GameObject BulletPrefab { get; set; }
    public AudioSource ShotSound { get; set; }

    public Bluster(GameObject pref, AudioSource sound)
    {
        BulletPrefab = pref;
        ShotSound = sound;
    }

    public override int BuletNeeded()
    {
        return 2;
    }
    
    public override string Name()
    {
        return "bluster";
    }

    public override void Shoot(Vector3 pos)
    {
        ShotSound.Play(0);

        // Instantiate the first bullet with a small offset above the initial position
        Vector3 firstBulletPosition = pos + new Vector3(0f, 0.1f, 0f);
        UnityEngine.Object.Instantiate(BulletPrefab, firstBulletPosition, Quaternion.identity);

        // Instantiate the second bullet with a small offset below the initial position
        Vector3 secondBulletPosition = pos - new Vector3(0f, 0.1f, 0f);
        UnityEngine.Object.Instantiate(BulletPrefab, secondBulletPosition, Quaternion.identity);
    }

}