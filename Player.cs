using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public TankCreator tankCreator;

    [HideInInspector]
    public int health = 1;

    [HideInInspector]
    public int tempHealth;

    public int playerNumber = 1;

    public float moveSpeed = 10f;

    private Rigidbody2D rg2D;

    private int DOWN = 180, UP = 0, RIGHT = 270, LEFT = 90;
    private int curDirection;
    private int nexDirection;

    public float fireRate = 2f;
    private float lastFireTime = 0f;

    private string xMovementAxisName;
    private string yMovementAxisName;
    private string fireAxitName;

    private ProjectileManager projectileMG;

    private Collider2D[] coll;
    private bool isInit;

    public GameObject invincibalAnimator;
    private GameObject inAnimator;
    private Animator animator;

    private bool initInvincibal = false;

    void Start()
    {
        Vector3 inTransform = transform.position;
        inTransform.x -= 0.031f;
        inTransform.y -= 0.05f;

        inAnimator = Instantiate(invincibalAnimator, inTransform, transform.rotation);

        inAnimator.transform.SetParent(transform);

        animator = inAnimator.GetComponent<Animator>();
        //Init is not end
        coll = gameObject.GetComponents<Collider2D>();
        foreach (Collider2D item in coll)
        {
            item.enabled = false;
        }
        isInit = false;

        curDirection = UP;

        xMovementAxisName = "Horizontal" + playerNumber;
        yMovementAxisName = "Vertical" + playerNumber;
        fireAxitName = "Fire" + playerNumber;

        rg2D = GetComponent<Rigidbody2D>();
        projectileMG = GetComponent<ProjectileManager>();
    }
    void FixedUpdate()
    {
        if (!isInit) return;
        if (!initInvincibal)
        {
            initInvincibal = true;

            animator.Play("Invincibal");
            Invincibal();
            Invoke("EndInvincibal", 3.0f);
        }
        Move();
        Turn();
        Fire();
    }
    private void Move() 
    {
        float xSpeed = Input.GetAxisRaw(xMovementAxisName);
        if (Mathf.Abs(xSpeed) > Mathf.Epsilon)
        {
            rg2D.velocity = new Vector2(xSpeed, 0) * moveSpeed;
            nexDirection = xSpeed > 0 ? RIGHT : LEFT;
            return;
        }

        float ySpeed = Input.GetAxisRaw(yMovementAxisName);
        if (Mathf.Abs(ySpeed) > Mathf.Epsilon)
        {
            rg2D.velocity = new Vector2(0, ySpeed) * moveSpeed;
            nexDirection = ySpeed > 0 ? UP : DOWN;
            return;
        }

        rg2D.velocity = new Vector2(0, 0);
    }
    private void Turn()
    {
        transform.Rotate(0, 0, nexDirection - curDirection);
        curDirection = nexDirection;
    }
    private void Fire()
    {
        if(Input.GetButton(fireAxitName))
        {
            if (Time.time - lastFireTime >= 1.0f / fireRate)
            {
                if (projectileMG.AbleToShoot())
                {
                    lastFireTime = Time.time;
                }
            }
        }
    }
    private void OnDestroy()
    {
        --tankCreator.playerLifes[playerNumber - 1];
    }
    public void EndInvincibal()
    {
        this.health = tempHealth;
        inAnimator.SetActive(false);
    }
    private void Init()
    {
        foreach (Collider2D item in coll)
        {
            item.enabled = true;
        }
        isInit = true;
    }
    public void Invincibal()
    {
        tempHealth = health;
        health = 999;
        inAnimator.SetActive(true);
    }
}
