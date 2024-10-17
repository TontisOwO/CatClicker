using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    SpriteRenderer sprite;
    Color originalColor;
    Color slightlyDarker = new Color (0.1f, 0.1f, 0.1f, 0f);
    Transform position;
    [SerializeField] float squishness = 0.1f;
    Vector2 squish;
    Vector2 originalSize;
    Vector2 currentsize;
    [SerializeField] float squishValue = 0.1f;
    bool over;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        position = GetComponent<Transform>();
        
        originalColor = sprite.color;
        originalSize = position.localScale;
        currentsize = position.localScale;
    }
    private void OnMouseOver()
    {
        over = true;
        sprite.color = originalColor - slightlyDarker;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            currentsize.x = Mathf.Lerp(currentsize.x, originalSize.x * squish.x, squishValue);
            currentsize.y = Mathf.Lerp(currentsize.y, originalSize.y * squish.y, squishValue);
        }
        else 
        {
            currentsize.x = Mathf.Lerp(currentsize.x, originalSize.x, squishValue);
            currentsize.y = Mathf.Lerp (currentsize.y, originalSize.y, squishValue);

        }
    }
    private void OnMouseExit()
    {
        over = false;
        sprite.color = originalColor;
        
    }
    private void Update()
    {
        position.localScale = currentsize;
        if ( !over )
        {
            currentsize.x = Mathf.Lerp(currentsize.x, originalSize.x, squishValue);
            currentsize.y = Mathf.Lerp(currentsize.y, originalSize.y, squishValue);
        }
        squish = new Vector2(originalSize.x + squishness, originalSize.y - squishness);

    }
}
