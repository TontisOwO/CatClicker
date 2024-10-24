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
    Rigidbody2D myRigidbody2D;

    Color originalColor;
    Color slightlyDarker = new Color (0.1f, 0.1f, 0.1f, 0f);

    [SerializeField] float squishness = 0.1f;
    [SerializeField] float squishValue = 0.1f;
    [SerializeField] float pickUpDelay = 0.2f;
    [SerializeField] float yeet = 2.0f;
    [SerializeField]float time;

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
            time += Time.deltaTime;
            if (time > pickUpDelay)
            {
                clicking = true;
            }
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
            if (clicking)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                myRigidbody2D.linearVelocity = new Vector2((mousePos.x - position.position.x) * yeet, (mousePos.y - position.position.y) * yeet);
            }
            clicking = false;
            time = 0;
        }
        if ( !over )
        {
            Squish(0);
        }
        if( clicking )
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.position = mousePos;
            myRigidbody2D.linearVelocity = Vector2.zero;
        }

        if (counting.displayedNumber != Mathf.FloorToInt(counting.count))
        {
            counting.displayedNumber = Mathf.FloorToInt(counting.count);
            Counter.text = "Pumpkins and Hearts: " + counting.displayedNumber;

        }
        if (position.position.y <= -10)
        {
            position.position = new Vector2(transform.position.x,10);
        }
        if (position.position.x >= 10)
        {
            position.position = new Vector2(-8.7f, position.position.y);
        }
        if (position.position.x <= -10)
        {
            position.position = new Vector2(8.7f, position.position.y);
        }
            
    }
    void Squish(float squishModifier)
    {
        currentsize.x = Mathf.Lerp(currentsize.x, originalSize.x + squishModifier, squishValue);
        currentsize.y = Mathf.Lerp(currentsize.y, originalSize.y - squishModifier, squishValue);
    }
}