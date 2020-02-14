using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Star : MonoBehaviour
{
    private ProjectileManager playerProMG;

    public float exsitTime = 7f;
    public float continueTime = 7f;

    public AudioClip pickUpAudio;
    private GameObject borders;
    private AudioSource audioSource;
    private void Start()
    {
        Destroy(gameObject, exsitTime);
        borders = GameObject.Find("Borders");
        audioSource = borders.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        audioSource.PlayOneShot(pickUpAudio);

        playerProMG = collision.GetComponent<ProjectileManager>();
        ++playerProMG.maxProjetiles;

        playerProMG.Invoke("DeProjectileNum", continueTime);

        Destroy(gameObject);
    }
}