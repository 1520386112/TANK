using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel2 : MonoBehaviour
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
        Application.LoadLevel("LevelScene2");
    }
}
