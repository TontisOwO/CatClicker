using TMPro;
using UnityEngine;

public enum CatMount
{
    floor, floored,
    leftWall, leftWalled,
    rightWall, rightWalled,
    ceiling, ceilinged,
    air,
    total
}

public class ButtonScript : MonoBehaviour
{
    //OBS:This code does *not* work on UI elements!!! >:(
    //This is an amalgamation of code by Anton and Azure
    //Autoclick things scripted by Alva

    [SerializeField] Sprite[] catSprites;

    [SerializeField] TextMeshProUGUI Counter;
    [SerializeField] Counting counting;
    [SerializeField] ParticleSystem heartRelease;

    SpriteRenderer sprite;
    Transform position;
    Rigidbody2D myRigidbody2D;

    Color originalColor;
    Color slightlyDarker = new Color(0.1f, 0.1f, 0.1f, 0f);

    [SerializeField] float squishness = 0.1f;
    [SerializeField] float squishValue = 0.1f;
    [SerializeField] float pickUpDelay = 0.2f;
    [SerializeField] float yeet = 2.0f;
    [SerializeField] float time;
    [SerializeField] float movementSpeed;
    [SerializeField] bool autoclickactive;

    Vector2 originalSize;
    Vector2 currentsize;
    Vector2 mousePos;
    bool over;
    bool clicking;

    float autoclickdelay;

    Vector2 alteredPos;
    [SerializeField] CatMount catState;
    [SerializeField] CatMount catStatePrevious;
    float catTime;
    float waitTime;
    float dropTime;
    float originalGravityScale;
    int decision;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        position = GetComponent<Transform>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

        originalColor = sprite.color;
        originalSize = position.localScale;
        currentsize = position.localScale;
        originalGravityScale = myRigidbody2D.gravityScale;
    }

    private void OnMouseOver()
    {
        over = true;
        sprite.color = originalColor - slightlyDarker;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            sprite.sprite = catSprites[1];
            Squish(squishness);
            time += Time.deltaTime;
            if (time > pickUpDelay)
            {
                clicking = true;
                catState = CatMount.air;
            }
        }
        else
        {
            Squish(0);

        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            EmitHeart();
        }
    }
    private void OnMouseExit()
    {
        over = false;
        sprite.color = originalColor;
    }

    private void EmitHeart()
    {
        counting.count += 1 * counting.multipierValue;
        heartRelease.Emit((int)(1 * counting.multipierValue));
    }

    private void Update()
    {
        position.localScale = currentsize;
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            sprite.sprite = catSprites[0];
            if (clicking)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                myRigidbody2D.linearVelocity = new Vector2((mousePos.x - position.position.x) * yeet, (mousePos.y - position.position.y) * yeet);
            }
            clicking = false;
            time = 0;
        }
        if (!over)
        {
            Squish(0);
        }
        if (clicking)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.position = mousePos;
            myRigidbody2D.linearVelocity = Vector2.zero;
            dropTime = 0;
        }
        else
        {
            dropTime += Time.deltaTime;
        }

        if (counting.displayedNumber != Mathf.FloorToInt(counting.count))
        {
            counting.displayedNumber = Mathf.FloorToInt(counting.count);
            Counter.text = "Pumpkins and Hearts: " + counting.displayedNumber;

        }
        if (position.position.y <= -10)
        {
            position.position = new Vector2(transform.position.x, 10);
        }
        if (position.position.x >= 10)
        {
            position.position = new Vector2(-8.7f, position.position.y);
        }
        if (position.position.x <= -10)
        {
            position.position = new Vector2(8.7f, position.position.y);
        }

        autoclickdelay += Time.deltaTime;

        if (counting.autoclicking)
        {
            if (autoclickdelay > 0.5)
            {
                EmitHeart();
                autoclickdelay = 0;
            }
        }

        catTime += Time.deltaTime;
        if (catTime >= waitTime && !clicking && dropTime >= 1)
        {
            decision = Random.Range(0, 3);
            WaitTimeRandom();
            catTime = 0;
        }
        else if (catTime >= waitTime)
        {
            catTime = 0;
        }
        if (catState == CatMount.ceiling || catState == CatMount.floor || 
            catState == CatMount.rightWall || catState == CatMount.leftWall)
        {
            waitTime += Time.deltaTime;
        }

        if (catState != CatMount.air)
        {
            Action();
        }

        if (position.position.x < -7.777f && (catState == CatMount.floored || catState == CatMount.ceilinged))
        {
            catStatePrevious = catState;
            catState = CatMount.leftWall;
        }
        else if (position.position.x > 7.777f && (catState == CatMount.floored || catState == CatMount.ceilinged))
        {
            catStatePrevious = catState;
            catState = CatMount.rightWall;
        }
        else if (position.position.y > 3.891 && (catState == CatMount.leftWalled || catState == CatMount.rightWalled))
        {
            catStatePrevious = catState;
            catState = CatMount.ceiling;
        }
        else if (position.position.y < -3.88f && (catState == CatMount.leftWalled || catState == CatMount.rightWalled))
        {
            catStatePrevious = catState;
            catState = CatMount.floor;
        }
        else
        {
            position.rotation = Quaternion.Euler(0, position.rotation.y, 0);
            myRigidbody2D.gravityScale = originalGravityScale;
        }

        if (catState == CatMount.leftWall || catState == CatMount.leftWalled
            || catState == CatMount.rightWall || catState == CatMount.rightWalled)
        {
            position.rotation = Quaternion.Euler(0, position.rotation.y, 90);
            myRigidbody2D.gravityScale = 0;
        }
        if (catState == CatMount.floor || catState == CatMount.floored)
        {
            position.rotation = Quaternion.Euler(0, position.rotation.y, 0);
            myRigidbody2D.gravityScale = originalGravityScale;
        }
        if (catState == CatMount.ceiling || catState == CatMount.ceilinged)
        {
            position.rotation = Quaternion.Euler(0, position.rotation.y, 180);
            myRigidbody2D.gravityScale = 0;
        }

        if ((position.position.y < 3.887f && position.position.y > -3.887f) && catState == CatMount.leftWall)
        {
            catState = CatMount.leftWalled;
        }
        if ((position.position.y < 3.887f && position.position.y > -3.887f) && catState == CatMount.rightWall)
        {
            catState = CatMount.rightWalled;
        }
        if ((position.position.y < -4.422 && catState == CatMount.air)
            || (position.position.x < 7.777f || position.position.x > -7.777f) && catState == CatMount.floor)
        {
            catState = CatMount.floored;
        }
        if (position.position.x < 7.768f && position.position.x > -7.74f && catState == CatMount.ceiling)
        {
            catState = CatMount.ceilinged;
        }
    }
    void Squish(float squishModifier)
    {
        currentsize.x = Mathf.Lerp(currentsize.x, originalSize.x + squishModifier, squishValue);
        currentsize.y = Mathf.Lerp(currentsize.y, originalSize.y - squishModifier, squishValue);
    }
    void WaitTimeRandom()
    {
        waitTime = Random.Range(1f, 2f);
    }

    void Action()
    {
        switch (decision)
        {
            case 0:
                break;

            case 1:
                alteredPos = new Vector2(position.position.x - movementSpeed * (waitTime - catTime), position.position.y);
                sprite.flipX = true;
                sprite.flipY = false;
                if (catStatePrevious == CatMount.ceilinged &&(catState == CatMount.rightWall || catState == CatMount.rightWalled))
                {
                    alteredPos = new Vector2(position.position.x + movementSpeed * (waitTime - catTime), position.position.y);
                    sprite.flipX = true;
                    sprite.flipY = false;
                }
                if ((position.position.x < -8.2 && (catState == CatMount.leftWall || catState == CatMount.leftWalled)) 
                    || (catState == CatMount.ceiling && position.position.y < 4.41f))
                {
                    alteredPos = new Vector2(position.position.x, position.position.y + movementSpeed * (waitTime - catTime));
                    sprite.flipX = false;
                    sprite.flipY = true;
                }
                if (position.position.x > 8.3 && (catState == CatMount.rightWall || catState == CatMount.rightWalled))
                {
                    alteredPos = new Vector2(position.position.x, position.position.y - movementSpeed * (waitTime - catTime));
                    sprite.flipX = true;
                    sprite.flipY = false;
                }
                if (position.position.y > 4.41f && (catState == CatMount.ceiling || catState == CatMount.ceilinged))
                {
                    alteredPos = new Vector2(position.position.x + movementSpeed * (waitTime - catTime), position.position.y);
                    sprite.flipX = true;
                    sprite.flipY = false;
                }
                position.position = Vector2.MoveTowards(position.position, alteredPos, movementSpeed * Time.deltaTime);
                break;

            case 2:
                alteredPos = new Vector2(position.position.x + movementSpeed * (waitTime - catTime), position.position.y);
                sprite.flipX = false;
                sprite.flipY = false;
                if (catStatePrevious == CatMount.ceilinged && (catState == CatMount.leftWall ||catState == CatMount.leftWalled))
                {
                    alteredPos = new Vector2(position.position.x - movementSpeed * (waitTime - catTime), position.position.y);
                    sprite.flipX = false;
                    sprite.flipY = false;
                }
                if (position.position.x < -8.2 && (catState == CatMount.leftWall || catState == CatMount.leftWalled))
                {
                    alteredPos = new Vector2(position.position.x, position.position.y - movementSpeed * (waitTime - catTime));
                    sprite.flipX = true;
                    sprite.flipY = true;
                }
                if ((position.position.x > 8.3 && (catState == CatMount.rightWall || catState == CatMount.rightWalled))
                    || (catState == CatMount.ceiling && position.position.y < 4.41f))
                {
                    alteredPos = new Vector2(position.position.x, position.position.y + movementSpeed * (waitTime - catTime));
                    sprite.flipX = false;
                    sprite.flipY = false;
                }
                if (position.position.y > 4.41f && (catState == CatMount.ceiling || catState == CatMount.ceilinged))
                {
                    alteredPos = new Vector2(position.position.x - movementSpeed * (waitTime - catTime), position.position.y);
                    sprite.flipX = false;
                    sprite.flipY = false;
                }
                position.position = Vector2.MoveTowards(position.position, alteredPos, movementSpeed * Time.deltaTime);
                break;
        }
    }
}