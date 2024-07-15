using UnityEngine;
using System.Collections;

public class SpriteLooper : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Sprite[] sprites; // Array of sprites to cycle through
    public float changeInterval = 0.5f; // Time interval for changing sprites

    private int currentSpriteIndex = 0;

    void Start()
    {
        if (spriteRenderer == null || sprites.Length == 0)
        {
            Debug.LogError("SpriteRenderer component or sprites array is not assigned.");
            return;
        }

        // Start the coroutine to change sprites at regular intervals
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
        spriteRenderer.sprite = sprites[currentSpriteIndex];
    }
}