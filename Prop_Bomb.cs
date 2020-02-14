using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Bomb : MonoBehaviour
{
    public float exsitTime = 7f;
    private GameObject[] enermy;

    public AudioClip pickUpAudio;
    private GameObject borders;
    private AudioSource audioSource;
    private void Start()
    {
        borders = GameObject.Find("Borders");
        audioSource = borders.GetComponent<AudioSource>();
        Destroy(gameObject, exsitTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        audioSource.PlayOneShot(pickUpAudio);
        enermy = GameObject.FindGameObjectsWithTag("Enermy");
        foreach(GameObject item in enermy)
        {
            Destroy(item);
        }
        Destroy(gameObject);
    }
}
