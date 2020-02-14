using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankCreator : MonoBehaviour
{
    [HideInInspector]
    public static TankCreator tankCreator;
    [HideInInspector]
    public static int playerAmount = 1;
    public int playerLife = 3;

    [HideInInspector]
    public int[] playerLifes = new int[2];
    private int[] lasPlayerLifes = new int[2];

    private bool[] playerIsDied = new bool[2];

    public Vector3[] enermyCreatePoints;
    public Vector3[] playerCreatePoints;

    private int randCreatePoint;
    private int randCreateEnermy;

    public Enermy[] enermy;
    public Player[] player;

    [HideInInspector]
    public int curEnermyNum = 0;
    public int setEnermyNum;
    public int[] totEnermyNum;

    public float intervalCreateTime = 3f;
    private float lasCreateTime = 0f;

    private bool haveRest = true;
    public GameObject gameOver;

    public int curLevel;

    private AudioSource audioSource;
    public AudioClip winAudio;
    public AudioClip gameOverClip;

    private bool gameOverIsTrue;
    private bool gameWinIsTrue;
    void Start()
    {
        gameOverIsTrue = false;
        gameWinIsTrue = false;

        audioSource = GetComponent<AudioSource>();

        tankCreator = this;

        playerIsDied[0] = false;
        playerIsDied[1] = true;

        playerLifes[0] = playerLife;
        lasPlayerLifes[0] = playerLife;
        playerLifes[1] = playerLife;
        lasPlayerLifes[1] = playerLife;

        CreatePlayer(0);

        if (playerAmount == 2)
        {
            CreatePlayer(1);
            playerIsDied[1] = false;
        }
        CreateEnermy(0);
        CreateEnermy(1);
        CreateEnermy(2);
    }
    void Update()
    {
        if (playerIsDied[0] && playerIsDied[1])
        {
            if(!gameOverIsTrue)
                GameOver();
        }
        if (Time.time - lasCreateTime >= intervalCreateTime)
        {
            randCreatePoint = Random.Range(0, enermyCreatePoints.Length);

            CreateEnermy(randCreatePoint);

            lasCreateTime = Time.time;
        }
        for(int i = 0;i <= 1; i++)
        {
            if(playerLifes[i] < lasPlayerLifes[i])
            {
                CreatePlayer(i);
                lasPlayerLifes[i] = playerLifes[i];
            }
        }
    }
    public void CreatePlayer(int x)
    {
        if (playerLifes[x] <= 0)
        {
            playerIsDied[x] = true;
            return;
        }

        var player_ = Instantiate(player[x], playerCreatePoints[x], transform.rotation);
        player_.tankCreator = this;
    }
    private void CreateEnermy(int CreatePoint)
    {
        if (curEnermyNum < setEnermyNum)
        {
            randCreateEnermy = Random.Range(0, enermy.Length);

            haveRest = false;
            foreach (int item in totEnermyNum)//判断是否所有种类敌人都已创建完
            {
                if (item > 0)
                {
                    haveRest = true;
                    break;
                }
            }
            if (!haveRest && curEnermyNum == 0)
            {
                //==========================================
                // 游戏胜利
                //==========================================
                if (gameWinIsTrue) return;

                gameWinIsTrue = true;

                audioSource.Stop();
                audioSource.volume = 1;
                audioSource.PlayOneShot(winAudio);

                Invoke("LoadNextLevel", 3.0f);
                return;
            }
            else if(!haveRest && curEnermyNum > 0)
            {
                return;
            }
            if (totEnermyNum[randCreateEnermy] > 0)
            {
                --totEnermyNum[randCreateEnermy];
                
                Quaternion _rotation = transform.rotation;
                _rotation.z += 180f;

                var thisEnermy = Instantiate(enermy[randCreateEnermy], enermyCreatePoints[CreatePoint], _rotation);
                thisEnermy.tankCreator = this;
                ++curEnermyNum;
            }
            else
            {
                CreateEnermy(CreatePoint);
            }
        }
    }
    private void LoadNextLevel()
    {
        if (curLevel != 3)
            SceneManager.LoadScene("LevelScene" + (curLevel + 1));
        else
            SceneManager.LoadScene("InitialScene");
    }
    public void GameOver()
    {
        gameOverIsTrue = true;
        //Game Over===============================
        gameOver.gameObject.SetActive(true);

        //Background Music set unenable
        audioSource.Stop();

        //Set Audio clip to GameOver
        audioSource.volume = 1;
        audioSource.PlayOneShot(gameOverClip);

        Invoke("GameOverTurn", 3.0f);
    }
    public void GameOverTurn()
    {
        SceneManager.LoadScene("InitialScene");
    }
}