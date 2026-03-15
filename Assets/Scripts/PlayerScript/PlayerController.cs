using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float smoothTime;

    private float verticalLookRotation;
    private Vector3 smoothMove;
    private Vector3 moveAmount;
    private Rigidbody rb;
    private PhotonView pnView;

    public bool isGrounded;
    
    private void Awake()
    {
        pnView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        if(!pnView.IsMine)
        {
            Destroy(playerCamera);
            Destroy(rb);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!pnView.IsMine)
        {
            return;
        }

        Look();
        Movement();

        Jump();
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }


    public void GroundState(bool isGrounded)
    {
        this.isGrounded = isGrounded;
    }
    private void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") *mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -80f, 90f);

        //playerCamera.transform.localEulerAngles = Vector3.left * verticalLookRotation;
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    private void Movement()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount,
                                                    moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed :walkSpeed),
                                                    ref smoothMove, smoothTime);
    }
    private void FixedUpdate()
    {
        if(!pnView.IsMine)
        {
            return;
        }
    rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }


}
