using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    //OBS:This code does *not* work on UI elements!!! >:(
    //This is an amalgamation of code by Anton and Azure

    [SerializeField] TextMeshProUGUI Counter;
    [SerializeField] Counting counting;
    [SerializeField] ParticleSystem heartRelease;

    SpriteRenderer sprite;
    Transform position;

    Color originalColor;
    Color slightlyDarker = new Color (0.1f, 0.1f, 0.1f, 0f);
    Rigidbody2D myRigidbody2D;

    [SerializeField] float squishness = 0.1f;
    [SerializeField] float squishValue = 0.1f;

    Vector2 originalSize;
    Vector2 currentsize;
    Vector2 mousePos;
    bool over;
    bool clicking;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        position = GetComponent<Transform>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        
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
            clicking = true;
        }
        else 
        {
            Squish(0);

        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            counting.count += 1 * counting.multipierValue;
            heartRelease.Emit(1);
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
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            clicking = false;

        }
        if ( !over )
        {
            Squish(0);
        }
        if( clicking )
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.position = new Vector2(mousePos.x, mousePos.y);
            myRigidbody2D.linearVelocity = Vector2.zero;
        }

        if (counting.displayedNumber != Mathf.FloorToInt(counting.count))
        {
            counting.displayedNumber = Mathf.FloorToInt(counting.count);
            Counter.text = "Pumpkins and Hearts: " + counting.displayedNumber;

        }
        if (transform.position.y <= -10)
        {
            transform.position = new Vector2(transform.position.x,10);
        }
            
    }
    void Squish(float squishModifier)
    {
        currentsize.x = Mathf.Lerp(currentsize.x, originalSize.x + squishModifier, squishValue);
        currentsize.y = Mathf.Lerp(currentsize.y, originalSize.y - squishModifier, squishValue);
    }
}