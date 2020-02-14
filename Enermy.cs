using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    public int health = 1;

    public float moveSpeed = 10f;

    private Rigidbody2D rg2D;

    private int DOWN = 180, UP = 0, RIGHT = 270, LEFT = 90;
    private int curDirection;
    private int nexDirection;

    private float lastFireTime = 0f;

    private string xMovementAxisName;
    private string yMovementAxisName;
    private string fireAxitName;

    private int xSpeed = 0, ySpeed = 0;

    public float minTimeBeforeNextRandMove = 0.2f;
    public float maxTimeBeforeNextRandMove = 1f;
    private float realTimeBeforeNextRandMove;
    private float lasRandMoveTime = 0f;

    public float minTimeBeforeNextFireTime = 1f;
    public float maxTimeBeforeNextFireTime = 3f;
    private float realTimeBeforeNextFireTime;

    private ProjectileManager projectileMG;
    [HideInInspector]
    public TankCreator tankCreator;

    private Collider2D[] coll;
    private bool isInit;

    private int initHealth;
    private Animator animator;
    void Start()
    {
        initHealth = health;

        animator = GetComponent<Animator>();

        //Init is not end
        coll = gameObject.GetComponents<Collider2D>();
        foreach (Collider2D item in coll)
        {
            item.enabled = false;
        }
        isInit = false;

        curDirection = DOWN;

        rg2D = GetComponent<Rigidbody2D>();
        projectileMG = GetComponent<ProjectileManager>();
        
        realTimeBeforeNextRandMove = 0f;
        realTimeBeforeNextFireTime = Random.Range(minTimeBeforeNextFireTime, maxTimeBeforeNextFireTime);
    }
    void FixedUpdate()
    {
        if(health < initHealth)
        {
            animator.Play("Enermy2_Injured");
        }

        if (!isInit) return;
        if (Time.time - lasRandMoveTime >= realTimeBeforeNextRandMove) 
        {
            RandMove();
        }
        Move();
        Turn();
        if (Time.time - lastFireTime >= realTimeBeforeNextFireTime)
        {
            Fire();
        }
    }
    private void RandMove()
    {
        xSpeed = 0;
        ySpeed = 0;
        int rand = Random.Range(0, 6);
        switch (rand)
        {
            case 0:
                xSpeed = 1; break;
            case 1:
                xSpeed = -1; break;
            case 2:
                ySpeed = 1; break;
            default:
                ySpeed = -1; break;
        }
        lasRandMoveTime = Time.time;
        realTimeBeforeNextRandMove = Random.Range(minTimeBeforeNextRandMove, maxTimeBeforeNextRandMove);
    }
    private void Move()
    {
        if (Mathf.Abs(xSpeed) > Mathf.Epsilon)
        {
            rg2D.velocity = new Vector2(xSpeed, 0) * moveSpeed;
            nexDirection = xSpeed > 0 ? RIGHT : LEFT;
            return;
        }

        if (Mathf.Abs(ySpeed) > Mathf.Epsilon)
        {
            rg2D.velocity = new Vector2(0, ySpeed) * moveSpeed;
            nexDirection = ySpeed > 0 ? UP : DOWN;
            return;
        }
    }
    private void Turn()
    {
        transform.Rotate(0, 0, nexDirection - curDirection);
        curDirection = nexDirection;
    }
    private void Fire()
    {
        if (projectileMG.AbleToShoot())
        {
            lastFireTime = Time.time;
        }
        lastFireTime = Time.time;
        realTimeBeforeNextFireTime = Random.Range(minTimeBeforeNextFireTime, maxTimeBeforeNextFireTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
            RandMove();
    }
    private void OnDestroy()
    {
        --tankCreator.curEnermyNum;
    }
    private void Init()
    {
        foreach (Collider2D item in coll)
        {
            item.enabled = true;
        }
        isInit = true;
    }
}
