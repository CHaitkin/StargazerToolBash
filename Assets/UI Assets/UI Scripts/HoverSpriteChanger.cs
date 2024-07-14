using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class HoverSpriteChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image; // Reference to the UI Image component
    public Sprite[] sprites; // Array of sprites to cycle through
    public float changeInterval = 0.5f; // Time interval for changing sprites

    private int currentSpriteIndex = 0;
    private bool isHovering = false;
    private Coroutine spriteChangeCoroutine;

    void Start()
    {
        if (image == null || sprites.Length == 0)
        {
            Debug.LogError("Image component or sprites array is not assigned.");
            return;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        spriteChangeCoroutine = StartCoroutine(ChangeSpriteRoutine());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if (spriteChangeCoroutine != null)
        {
            StopCoroutine(spriteChangeCoroutine);
        }
    }

    IEnumerator ChangeSpriteRoutine()
    {
        while (isHovering)
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