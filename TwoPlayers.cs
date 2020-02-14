using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayers : MonoBehaviour
{
    private SpriteRenderer spriteR;
    public Sprite sampleSprite;
    public Sprite onMouseSprite;
    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }
    private void OnMouseEnter()
    {
        spriteR.sprite = onMouseSprite;
    }
    private void OnMouseExit()
    {
        spriteR.sprite = sampleSprite;
    }

    [System.Obsolete]
    private void OnMouseDown()
    {
        TankCreator.playerAmount = 2;
        Application.LoadLevel("SelectScene");
    }
}
