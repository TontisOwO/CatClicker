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
            Squish(squishness);
        }
        else 
        {
            Squish(0);

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
            Squish(0);
        }

        if (counting.displayedNumber != Mathf.FloorToInt(counting.count))
        {
            counting.displayedNumber = Mathf.FloorToInt(counting.count);
            Counter.text = "Pumpkins and Hearts: " + counting.displayedNumber;

        }
            
    }
    void Squish(float squishModifier)
    {
        currentsize.x = Mathf.Lerp(currentsize.x, originalSize.x + squishModifier, squishValue);
        currentsize.y = Mathf.Lerp(currentsize.y, originalSize.y - squishModifier, squishValue);
    }
}
