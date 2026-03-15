using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    [SerializeField] Camera myCam;

    public override void Use()
    {
        Shoot();
    }
    private void Shoot()
    {
        Ray ray = myCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = myCam.transform.position;

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfoScript)itemInfo).Damage);
        }
    
    }
}