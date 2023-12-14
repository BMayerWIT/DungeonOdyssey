using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotAnimation : MonoBehaviour
{
    public Image image;
    public Sprite[] spriteArray;
    public float animSpeed;
    Coroutine coroutineAnim;
    private int spriteIndex;
    private bool isDone;

    public void PlayUIAnim()
    {
        isDone = false;
        StartCoroutine(PlayAnimation());
    }

    public void StopUIAnim()
    {
        isDone=true;
        StopCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(animSpeed);
        if (spriteIndex >= spriteArray.Length)
        {
            spriteIndex = 0;
        }
        image.sprite = spriteArray[spriteIndex];
        spriteIndex++;
        if (isDone == false)
        {
            coroutineAnim = StartCoroutine(PlayAnimation());
        }
    }
}