using UnityEngine;

public class SmokeDetail : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 0.01f;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sprite.color = GetNewColor();
        if (sprite.color.a <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private Color GetNewColor()
    {
        float r = sprite.color.r - speed;
        float g = sprite.color.g - speed;
        float b = sprite.color.b - speed;
        float a = sprite.color.a;
        if (r < 0f)
        {
            r = 0f;
        }

        if (g < 0f)
        {
            g = 0f;
        }

        if (b < 0f)
        {
            b = 0f;
        }

        if (r == 0f && g == 0f && b == 0f)
        {
            a -= speed;
        }

        if (a < 0f)
        {
            a = 0f;
        }
        
        return new Color(r,g,b,a);
    }
}
