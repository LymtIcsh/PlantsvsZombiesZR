using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiImagePlant : Plant
{
    public override void cold()
    {
        if (state == PlantState.Normal)
        {
            state = PlantState.Cold;
            AudioManager.Instance.PlaySoundEffect(24);

            SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                if (spriteRenderer.gameObject.tag == "Plant")
                {
                    spriteRenderer.color = new Color(0.33f, 0.54f, 1f);
                }
            }

            GetComponent<Animator>().speed = 0.5f;
            Invoke("coldHurt", 1f);
        }
    }

    public override void warm()
    {
        warmSource++;
        if (warmSource == 1)
        {
            state = PlantState.Warm;
            SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                if (spriteRenderer.gameObject.tag == "Plant")
                {
                    spriteRenderer.color = Color.white;
                }
            }
            GetComponent<Animator>().speed = 1f;
        }
    }

    public override void normal()
    {
        state = PlantState.Normal;
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            if (spriteRenderer.gameObject.tag == "Plant")
            {
                spriteRenderer.color = Color.white;
            }
        }
        GetComponent<Animator>().speed = 1f;
    }


}
