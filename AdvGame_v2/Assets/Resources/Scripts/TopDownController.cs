using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{

    Rigidbody2D body;
    SpriteRenderer spriteRenderer;
    Animator anim;
    GameObject pauseMenu;


    // Animation Setup
    public bool movementEnabled = false;
    bool movementStateChange = true;
    float moveDisabledTimer = 0.0f;
    float moveDelay = 0.1f;
    public float baseSpeed;
    Vector2 direction;
    float speed;
    float dx;
    float dy;

    public int currentSortingOrder;

    // Box Movement
    public bool boxGrabEnabled = false;
    public GameObject boxToMove;
    GameObject heldBox;
    public float dragSpeed;
    bool holdingBox = false;

    // Button Handling
    public bool buttonPressEnabled = false;
    public GameObject buttonToPress;

    // Collectible Handling
    public bool itemGrabEnabled = false;
    public GameObject itemToGrab;
    int itemID = 0;
    int entryID = 0;
    public GameObject canvas;
    bool textDisplayed = false;

    // Objective Tracking
    public bool nextToDoor = false;
    public GameObject door;
    public GameObject[] objectiveTracker;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        pauseMenu = GameObject.Find("PauseMenu");

        speed = baseSpeed;
        currentSortingOrder = spriteRenderer.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementStateChange)
        {
            moveDisabledTimer += Time.deltaTime;
            if (moveDisabledTimer > moveDelay)
            {
                moveDisabledTimer = 0.0f;
                movementEnabled = true;
                movementStateChange = false;
            }
        }

        // get direction of input
        if (movementEnabled) {
            if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
            {
                dx = Mathf.Cos(Mathf.PI / 6) * Input.GetAxis("Horizontal");
                dy = Mathf.Sin(Mathf.PI / 6) * Input.GetAxis("Vertical");
            }
            else
            {
                dx = Input.GetAxis("Horizontal");
                dy = Input.GetAxis("Vertical");
            }
        } else
        {
            dx = 0.0f;
            dy = 0.0f;
        }

        direction = new Vector2(dx, dy).normalized;

        if (direction == Vector2.zero)
        {
            anim.SetBool("Moving", false);
        } else
        {
            anim.SetBool("Moving", true);
            if (direction.x > 0)
            {
                if (direction.y > 0)
                {
                    anim.SetInteger("FacingDirection", 1);
                } else if (direction.y < 0)
                {
                    anim.SetInteger("FacingDirection", 3);
                } else
                {
                    anim.SetInteger("FacingDirection", 2);
                }
            } else if (direction.x < 0)
            {
                if (direction.y > 0)
                {
                    anim.SetInteger("FacingDirection", 7);
                }
                else if (direction.y < 0)
                {
                    anim.SetInteger("FacingDirection", 5);
                }
                else
                {
                    anim.SetInteger("FacingDirection", 6);
                }
            } else
            {
                if (direction.y > 0)
                {
                    anim.SetInteger("FacingDirection", 8);
                }
                else if (direction.y < 0)
                {
                    anim.SetInteger("FacingDirection", 4);
                }
            }
        }

        // set walk based on direction
        body.velocity = direction * speed;
        

        // Grab Box
        if (boxGrabEnabled || holdingBox) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (!holdingBox) {
                    Debug.Log("Grab Box");
                    holdingBox = true;
                    // Attach Box to Player
                    heldBox = boxToMove;
                    boxToMove.transform.parent = this.transform;
                    boxToMove.GetComponentInParent<PushBoxes>().releaseText.SetActive(true);
                    // Change Speed
                    speed = dragSpeed;
                    // Set Drag Anim Facing Box
                } else {
                    ReleaseBox();
                }
            }
        } // Press Button 
        else if (buttonPressEnabled) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Debug.Log("Press Button");

                buttonToPress.GetComponentInParent<Button>().PressButton();
            }
        } // Read Collectible 
        else if (itemGrabEnabled) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Debug.Log("Collect Item");

                var item = itemToGrab.GetComponentInParent<Collectible>();

                itemID = item.textID;
                entryID = item.entryID;
                item.Collect();
                
                movementEnabled = false;

                canvas.SetActive(true);
                textDisplayed = true;
                canvas.GetComponentInParent<TextLibrary>().DisplayText(itemID);

                pauseMenu.GetComponentInParent<PauseMenu>().FoundEntry(entryID);
            }
        } // Collectible Next 
        else if (textDisplayed) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Debug.Log("Next Text");

                textDisplayed = canvas.GetComponentInParent<TextLibrary>().ProgressText();
                if (!textDisplayed)
                {
                    canvas.SetActive(false);
                    movementStateChange = true;
                }
            }
        } // Read Door
        else if (nextToDoor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Read Door");

                door.GetComponent<Door>().SetRead();
                objectiveTracker[door.GetComponent<Door>().id].SetActive(true);
            }
        }

        // Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.GetComponentInParent<PauseMenu>().PauseGame(this.gameObject);
            movementEnabled = false;
        }
    }

    public void ShiftLayer(int layer)
    {
        spriteRenderer.sortingOrder = layer;
        currentSortingOrder = layer;
    }

    public void ReleaseBox()
    {
        Debug.Log("Release Box");
        heldBox.GetComponentInParent<PushBoxes>().releaseText.SetActive(false);
        holdingBox = false;
        // Detach Box from Player
        this.transform.DetachChildren();
        // Change Speed
        speed = baseSpeed;
        // Resume Normal Animations
    }

    public void EnableMovement()
    {
        movementStateChange = true;
    }
}
