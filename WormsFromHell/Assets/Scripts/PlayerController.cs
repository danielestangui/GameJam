using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //private int layerMask = 1 << 8;


    [Header("Move values")]
    private float _speed = 60f; // La _speed es mejor dejarla así
    public float _maxSpeed = 5f;
    public float _jumpPower;

    [Header("Prefabs")]
    public GunController _gunController;
    public Transform _gunHolder;

    private Rigidbody rb;
    //private Animator anim;
    private float distanceToGround = .4f;
    private bool isGrounded;
    private bool jump;

    private int score;

    private bool isAlive;

    void Start()
    {
        score = 0;
        isAlive = true;
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
        _gunController = GetComponent<GunController>();
    }

    void Update()
    {
        float delta = Time.deltaTime;

        if (!isAlive) {
            return;
        }


        //anim.SetBool("isGrounded",isGrounded);

        MouseInput(delta);

        RaycastHit hit;
        isGrounded = false;

        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), transform.TransformDirection(-Vector3.up), out hit, distanceToGround))
        {
            isGrounded = true;

            // DEBUG: Visualización del Raycast desde el Player al Ground.
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * distanceToGround, Color.red);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {

        if (!isAlive)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        PlayerMove();

        //anim.SetFloat("speed", Mathf.Abs(horizontal));

    }

    /// <summary>
    /// PlayerMove se encarga del movimiento del Player.
    /// </summary>
    private void PlayerMove()
    {

        float horizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontal) > 0)
        {
            rb.AddForce(Vector3.right * _speed * horizontal);
            float limitedSpeed = Mathf.Clamp(rb.velocity.x, -_maxSpeed, _maxSpeed);
            rb.velocity = new Vector3(limitedSpeed, rb.velocity.y, 0);
        }
        // Evita que deslice cuando está en el suelo.
        else if (isGrounded)
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (jump)
        {
            rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            jump = false;
        }
    }

    /// <summary>
    /// Se encarga de los controles del Mouse.
    /// </summary>
    private void MouseInput(float delta)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = new Vector3(
            mousePosition.x - _gunHolder.position.x,
            mousePosition.y - _gunHolder.position.y,
            0);

        _gunHolder.right = direction;

        // Corrección del sprite del Player

        transform.rotation = Quaternion.identity;
        _gunHolder.localScale = Vector3.one;

        if (mousePosition.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _gunHolder.localScale = new Vector3(1,-1,1);
        }

        //Shoot 
        if (Input.GetMouseButton(0))
        {
            _gunController.Shoot();
        }
    }

    public void Die() {
        //Debug.Log("Game lovers");
        isAlive = false;
    }

    public void AddPoints(int points) {
        score += points;
    }

    public int GetScore() {
        return score;
    }
}
