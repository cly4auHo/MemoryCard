using UnityEngine;

public class Button : MonoBehaviour
{
    public Color highlightColor = Color.cyan;

    public void OnMouseEnter()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite)
        {
            sprite.color = highlightColor;
        }
    }
    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (sprite)
        {
            sprite.color = Color.white;
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1f); //button will be bigger, after press
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
