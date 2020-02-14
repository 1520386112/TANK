using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public Projectile projectilePF;
    private AudioSource shootAudio;

    [HideInInspector]
    public int curProjectiles = 0;
    public int maxProjetiles = 1;
    private void Start()
    {
        if(this.CompareTag("Player"))
            shootAudio = GetComponent<AudioSource>();   
    }
    public bool AbleToShoot()
    {
        if (curProjectiles < maxProjetiles)
        {
            var projectile = Instantiate(projectilePF, transform.position, transform.rotation);
            projectile.projectileMG = this;
            ++curProjectiles;

            if(shootAudio != null) shootAudio.PlayOneShot(shootAudio.clip);

            return true;
        }
        else
            return false;
    }
    public void DeProjectileNum()
    {
        --maxProjetiles;
    }
}
