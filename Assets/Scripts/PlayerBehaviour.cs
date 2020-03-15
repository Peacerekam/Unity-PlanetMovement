using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject cameraHandler;
    public GameObject currentPlanet;
    public GameObject playerSprite;

    [SerializeField]
    private float playerSpeed;

    private Rigidbody rb;
    private RaycastHit hit;
    private bool grounded = false;
    private int planetLayer;

    //private Vector3 torque;
    private Vector3 dir;
    private bool momentumFlag = false;


    public void KillPlayer()
    {
        Destroy(gameObject, 0f);
    }

    void Start()
    {
        planetLayer = LayerMask.GetMask("Planet");
        cameraHandler = Camera.main.transform.gameObject.transform.parent.gameObject;
        //torque = new Vector3(0f, 0f, 0f);
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        HandleRotation();
        UpdateCamera();

        HandleJumping();
    }

    void FixedUpdate()
    {
        CheckGrounded();
        MovePlayer();
        HandleGravity();
    }

    private void CheckGrounded()
    {
        if (Physics.Raycast(transform.position, dir, 0.5f, planetLayer, QueryTriggerInteraction.Ignore))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void UpdateCamera()
    {
        cameraHandler.transform.Translate((currentPlanet.transform.position - cameraHandler.transform.position) * Time.deltaTime * 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // zmiana planety
        if (other.gameObject.layer == 9) {
            currentPlanet = other.gameObject;
            transform.parent = currentPlanet.transform;

            if (!momentumFlag)
            {
                StartCoroutine("MomentumCooldown");
            }
        }
    }

    IEnumerator MomentumCooldown() {
        momentumFlag = true;

        momentum = -momentum;
        yield return new WaitForSecondsRealtime(0.2f);

        momentumFlag = false;
    }

    private bool pull = true;
    private void HandleGravity()
    {
        // ogarniecie grawitacji
        dir = Vector3.Normalize(currentPlanet.transform.position - playerSprite.transform.position);

        rb.AddForce(Time.fixedDeltaTime * dir * 500f);
        
    }

    private void HandleRotation()
    {
        // ogarniecie rotacji sprite
        playerSprite.transform.up = transform.position - currentPlanet.transform.position;
        Debug.DrawLine(transform.position, currentPlanet.transform.position, Color.red);
    }

    //float currentJumpHeight = 1f;
    //float distanceFromPlanet;
    //float playerRot;
    //Vector3 newPlayerPos;

    float momentum;

    private void HandleJumping()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space)) {
            grounded = false;
            //currentJumpHeight += 1f;
            //Debug.Log("jumped");
            rb.AddForce(-dir * 250f);
        }
    }

    private void MovePlayer()
    {
        // ogarniecie sterowania
        //torque.z = -Input.GetAxis("Horizontal");
        //rb.AddTorque(torque * playerSpeed);
        //rb.AddForce(Time.fixedDeltaTime * playerSprite.transform.right * Input.GetAxis("Horizontal") * playerSpeed);

        if (grounded)
        {
            momentum = Input.GetAxis("Horizontal") * playerSpeed;
        }

        //rb.velocity = (playerSprite.transform.right * momentum * Time.fixedDeltaTime * playerSpeed) * 10f;

        rb.MovePosition(rb.transform.position + (playerSprite.transform.right * momentum * Time.fixedDeltaTime));
        //rb.AddForce(moveTowards);

        /*
                                            // wielkosc planety     // gzone    //half of player
        distanceFromPlanet = (currentPlanet.transform.localScale.x / 3.5f) + 1.2f;

        if (currentJumpHeight < 0) currentJumpHeight = 0f;
        
        playerRot -= Input.GetAxis("Horizontal") * Time.fixedDeltaTime * playerSpeed;
        newPlayerPos = Quaternion.Euler(0f, 0f, playerRot) * Vector3.up;
        //newPlayerPos = playerSprite.transform.up;
        //rb.transform.position = currentPlanet.transform.position + (newPlayerPos * (distanceFromPlanet + currentJumpHeight)); 
        rb.transform.position = Vector3.MoveTowards(rb.transform.position, currentPlanet.transform.position + (newPlayerPos * (distanceFromPlanet + currentJumpHeight)), playerSpeed * Time.fixedDeltaTime);
        //rb.position =
        */
        
        
        if (rb.velocity.magnitude > 5f) {
            rb.velocity = rb.velocity.normalized * 5f;
            //Debug.Log("limited speed");
        }
        
        
    }
    
}
