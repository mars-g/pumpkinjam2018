using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDControls : MonoBehaviour {


    public float moveSpeed = 5f;
    public float jumpSpeed = 8f;
    public float jumpTime = 2f;
    public float gravityScalar = 1f;
    public float wallPush = 3f;
    public float pushModifier = 0f;
    public float wallJumpTime = 1f;
    public float slideSpeed = 3f;
    public float slideCD = 2f;
    public float slideTime = 1f;
    public float attackCD = .2f;
    private bool canatk = true;
    private float slideTimer = 0f;
    private float slideMove = 0f;
    private bool slideJumped = false;

    //stores direction from the wall to walljump
    private float wallDirection;
    //jump state stores the number of jumps character can use before hitting the ground
    public int jumpState = 2;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private GameObject slide;
    private GameObject swing;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        slide = transform.GetChild(0).gameObject;
        swing = transform.GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        //Get left right movement
        float moveHor = Input.GetAxis("Horizontal") * moveSpeed;
        //set default vertical speed
        float moveVert = rb.velocity.y;

        //check for walljump
        if (wallDirection != 0 && Input.GetButtonDown("Jump") && jumpState != 2)
        {
            moveVert = jumpSpeed * 1.3f;
            StartCoroutine(WallJump());
        }

        //check for able to normal jump
        else if (jumpState > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpState--;
                moveVert = jumpSpeed;
            }
        }

        //CHECK FOR SLIDE
        if (jumpState == 2 && Input.GetAxis("Vertical") < 0 && slideTimer + slideCD < Time.time && Mathf.Abs(moveHor) > 1.5)
        {
            slideTimer = Time.time + slideTime;
            slideMove = Mathf.Sign(moveHor) * slideSpeed;
        }

        //end slide because a wall is hit
        if (wallDirection != 0) {
            slideTimer = 0;
        }

        //implement slide
        if (slideTimer > Time.time)
        {
            moveHor = slideMove;

            if (jumpState < 2)
            {
                slideJumped = true;
            }

            if (!slideJumped)
            {
                slide.SetActive(true);
            }                
            else slide.SetActive(false);
        }
        else
        {
            slideJumped = false;
            slide.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            if(!slide.activeSelf && canatk)
            {

                StartCoroutine("Swing");
            }
        }
        
        //SETS VELOCITY
        rb.velocity = new Vector2(moveHor + pushModifier, moveVert);

        //checks for and applies advanced jump physics
        if (jumpState < 2 && Mathf.Abs(rb.velocity.y) < 0.5)
        {
            StartCoroutine(AdvancedJumpPhysics());
        }

        //animation states
        if (rb.velocity.x < 0)
        {
            sr.flipX = true;

        }
        else if(rb.velocity.x > 0)
        {
            sr.flipX = false;

        }

        if (sr.flipX)
        {
            swing.transform.localPosition = new Vector3(-5.25f, 0, 0);
            slide.transform.localPosition = new Vector3(-10f, 0, 0);
        }
        else
        {
            swing.transform.localPosition = new Vector3(5.25f, 0, 0);
            slide.transform.localPosition = new Vector3(10f, 0, 0);
        }

        //set animation states
        anim.SetBool("Running", (moveHor != 0) && (jumpState == 2));
        anim.SetBool("Jumping", (jumpState < 2) && (Input.GetButton("Jump")) && pushModifier == 0);
        anim.SetBool("Walljumping", (wallDirection != 0 && Input.GetButton("Jump") && jumpState != 2) && pushModifier !=0);
        anim.SetBool("Freefalling", rb.velocity.y<=0 && jumpState<2);
        anim.SetBool("Sliding",slideTimer>Time.time && !slideJumped);
        anim.SetBool("Swinging", !canatk && swing.activeSelf);
    }

    IEnumerator Swing()
    {
        canatk = false;
        swing.SetActive(true);
        yield return new WaitForSeconds(.15f);
        swing.SetActive(false);
        yield return new WaitForSeconds(attackCD);
        canatk = true;

    }



    ///<summary>
    /// Applies advanced jump physics to player
    /// 
    /// When player reaches the apex of their jump, applies an extra downward force so the jump doesn't feel floaty
    /// </summary>
    /// <returns>Returns async null at every point in loop</returns>
    private IEnumerator AdvancedJumpPhysics() {
        int currentJumpState = jumpState;
        for (float t = 0; t < jumpTime; t += Time.deltaTime) {
            if (jumpState < currentJumpState || jumpState == 2) {
                break;
            }
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - gravityScalar);
            yield return null;
        }
        yield return null;
    }

    /// <summary>
    /// Applies Async walljump force in opposite direction of the player
    /// </summary>
    /// <returns>Async null</returns>
    private IEnumerator WallJump()
    {
        float wallDir = wallDirection;
        for (float t = 0; t < wallJumpTime; t += Time.deltaTime)
        {
            pushModifier = Mathf.Abs(wallPush - t * 2) * wallDir;
            yield return null;
        }
        pushModifier = 0;
        yield return null;
    }

    //checks for collision with ground to refresh jump ability
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            //grounded = true;
            jumpState = 2;
            wallDirection = 0;
        }
        

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Wall>() && GetComponent<Transform>().position.y < collision.gameObject.transform.position.y + collision.gameObject.transform.localScale.y / 2)
        {
            
            wallDirection = Mathf.Sign(gameObject.transform.position.x - collision.gameObject.transform.position.x);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.GetComponent<Ground>()) && jumpState == 2)
        {
            //grounded = false;
            jumpState--;
        }
        else if (collision.gameObject.GetComponent<Wall>()) {
            wallDirection = 0;
        }
    }
}
