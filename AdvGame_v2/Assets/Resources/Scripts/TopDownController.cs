using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{

    Rigidbody2D body;
    SpriteRenderer spriteRenderer;


    // Animation Setup

    public float baseSpeed;
    Vector2 direction;
    float speed;
    float dx;
    float dy;

    public int currentSortingOrder;

    // Box Movement
    public bool boxGrabEnabled = false;
    public GameObject boxToMove;
    public float dragSpeed;
    bool holdingBox = false;

    // Button Handling
    public bool buttonPressEnabled = false;
    public GameObject buttonToPress;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        speed = baseSpeed;
        currentSortingOrder = spriteRenderer.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        // get direction of input
        if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            dx = Mathf.Cos(Mathf.PI / 6) * Input.GetAxis("Horizontal");
            dy = Mathf.Sin(Mathf.PI / 6) * Input.GetAxis("Vertical");            
        } else
        {
            dx = Input.GetAxis("Horizontal");
            dy = Input.GetAxis("Vertical");
        }

        direction = new Vector2(dx, dy).normalized;

        // set walk based on direction
        body.velocity = direction * speed;

        if (boxGrabEnabled || holdingBox) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (!holdingBox) {
                    Debug.Log("Grab Box");
                    holdingBox = true;
                    // Attach Box to Player
                    boxToMove.transform.parent = this.transform;
                    // Change Speed
                    speed = dragSpeed;
                    // Set Drag Anim Facing Box
                } else {
                    ReleaseBox();
                }
            }
        }

        if (buttonPressEnabled) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Debug.Log("Press Button");

                buttonToPress.GetComponentInParent<Button>().PressButton();
            }
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
        holdingBox = false;
        // Detach Box from Player
        this.transform.DetachChildren();
        // Change Speed
        speed = baseSpeed;
        // Resume Normal Animations
    }
}
