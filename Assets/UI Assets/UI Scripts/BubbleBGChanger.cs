using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleBGChanger : MonoBehaviour
{
    public Image image; // Reference to the UI Image component
    public Sprite[] sprites; // Array of sprites to cycle through
    public float changeInterval = 0.5f; // Time interval for changing sprites

    private int currentSpriteIndex = 0;

    void Start()
    {
        if (image == null || sprites.Length == 0)
        {
            Debug.LogError("Image component or sprites array is not assigned.");
            return;
        }

        StartCoroutine(ChangeSpriteRoutine());
    }

    IEnumerator ChangeSpriteRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeInterval);
            ChangeSprite();
        }
    }

    void ChangeSprite()
    {
        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
        image.sprite = sprites[currentSpriteIndex];
    }
}
