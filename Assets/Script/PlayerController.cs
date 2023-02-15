using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class PlayerController : Singleton<PlayerController>
{
    Vector3 moveDirection;
    public Vector2 turn;
    public Rigidbody rb;
    public float playerSpeed;
    public float jumpForce;
    public GameObject playerObject;
    const string strMoving = "IsMoving"; // gereksiz yere bilgisayarý yormadan diret hedef gösteriyor.
    const string strHorizontal = "Horizontal";
    const string strVertical = "Vertical";
    public float sensitivity = .5f;
    [SerializeField] float characterSpeed;
    public AudioSource audiosource;
    float jumpTimer;




    void Start()
    {
        audiosource.Play();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
    }
    private void LateUpdate()
    {
        RotateCamera();
    }
    void Update()
    {
        FootStep();
        characterSpeed = rb.velocity.magnitude;
        if (rb.velocity.magnitude > 2)
        {
            AttackSystem.Instance.animator.SetBool(strMoving, true);
        }
        else
        {
            AttackSystem.Instance.animator.SetBool(strMoving, false);

        }

    }
    private void Move()
    {
        if (HealthSystem.Instance.IsAlive == true)
        {
            moveDirection = new Vector3(Input.GetAxis(strHorizontal) * playerSpeed, rb.velocity.y, Input.GetAxis(strVertical) * playerSpeed);
            rb.velocity = transform.TransformDirection(moveDirection);
            rb.isKinematic = false;

        }
        else
        {
            rb.isKinematic = true;
        }
    }
    void RotateCamera()
    {
        if (Input.GetMouseButton(1))
        {
            turn.x += Input.GetAxis("Mouse X") * sensitivity;
        }
        transform.rotation = Quaternion.Euler(-turn.y, turn.x, 0);
        //Quaternion rotation = playerObject.transform.rotation;
        //rotation.eulerAngles = new Vector3(0, turn.x, 0);
        //playerObject.transform.rotation = rotation;
    }
    void Jump()
    {
        jumpTimer += 1 * Time.deltaTime;
        if (jumpTimer >= 1.6f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector3(0, jumpForce, 0);
                AttackSystem.Instance.animator.Play("Jump");
                jumpTimer = 0;
            }
        }
    }
    void FootStep()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            audiosource.enabled = true;
            //audiosource.Play();
        }
        else
        {
            audiosource.enabled = false;
        }
    }
}
