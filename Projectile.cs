using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1f;
    [HideInInspector]
    public ProjectileManager projectileMG;
    public GameObject projectileExplosion;

    private GameObject borders;
    private AudioSource audioSource;
    public AudioClip hitAudio;
    public AudioClip explosionAudio;

    void Start()
    {
        borders = GameObject.Find("Borders");

        audioSource = borders.GetComponent<AudioSource>();

        Destroy(gameObject, 5f);//子弹存在时间超过5s即销毁
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (audioSource == null) Debug.LogError("audio null");
        if (collision.gameObject.CompareTag("FragileBox"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            --player.health;
            if (player.health <= 0)
            {
                audioSource.PlayOneShot(explosionAudio);
                Destroy(player.gameObject);
            }
            else
            {
                audioSource.PlayOneShot(hitAudio);
            }
        }
        else if (collision.gameObject.CompareTag("Enermy"))
        {
            var enermy = collision.gameObject.GetComponent<Enermy>();
            --enermy.health;
            if (enermy.health <= 0)
            {
                audioSource.PlayOneShot(explosionAudio);
                Destroy(enermy.gameObject);
            }
            else
            {
                audioSource.PlayOneShot(hitAudio);
            }
        }
        else if (collision.gameObject.CompareTag("Home"))
        {
            //游戏结束
            //
            TankCreator.tankCreator.GameOver();

            SpriteRenderer homeSR = collision.GetComponent<SpriteRenderer>();
            homeSR .sprite = collision.GetComponent<Home>().homeDie;
            collision.enabled = false;
        }
        if (!collision.gameObject.CompareTag("Prop"))
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        --projectileMG.curProjectiles;
        Instantiate(projectileExplosion, transform.position, transform.rotation);
    }
}