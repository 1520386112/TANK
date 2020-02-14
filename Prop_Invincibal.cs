using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Invincibal : MonoBehaviour
{
    public float exsitTime = 7f;
    public float continueTime = 4f;

    public AudioClip pickUpAudio;
    private GameObject borders;
    private AudioSource audioSource;

    private Player player;
    private void Start()
    {
        Destroy(gameObject, exsitTime);
        borders = GameObject.Find("Borders");
        audioSource = borders.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        audioSource.PlayOneShot(pickUpAudio);

        player = collision.GetComponent<Player>();

        player.Invincibal();

        player.Invoke("EndInvincibal", continueTime);

        Destroy(gameObject);
    }
}
