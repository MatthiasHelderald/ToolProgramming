using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "weaponData", menuName = "Weapons/new Weapon Data", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public string displayName;
        public FireMode FireMode;
        public ShootType ShootType;

        public GameObject modelPrefab;
        public Sprite modelSprite;

        public float damage = 1;
        public float accuracy = 10;
        public float recoilForce = 10;
        public int maxAmmo = 10;
    }
}

public enum ShootType
{
    Hitscan,
    Projectile,
    Zone,
}

public enum FireMode
{
    Auto,
    SemiAuto,
    Manuel,
}