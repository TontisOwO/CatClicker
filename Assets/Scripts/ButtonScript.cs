using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    //OBS:This code does *not* work on UI elements!!! >:(

    [SerializeField] TextMeshProUGUI Counter;
    [SerializeField] Counting counting;
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            counting.count += 1 * counting.multipierValue;
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

        if (counting.displayedNumber != Mathf.FloorToInt(counting.count))
        {
            counting.displayedNumber = Mathf.FloorToInt(counting.count);
            Counter.text = "Pumpkins and Hearts: " + counting.displayedNumber;

        }
            
    }
}
