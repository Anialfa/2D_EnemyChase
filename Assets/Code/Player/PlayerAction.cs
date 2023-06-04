using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    private Rigidbody2D playerRB;
    private Collider2D playerCollider;
    private Animator playerAnimator;
    private Transform playerTransform;
    public LayerMask Ground;
    [Header("移动速度")]
    public float PlayerMoveSpeed = 10;
    [Header("起跳力度")]
    public float PlayerJumpSpeed = 5;
    [Header("跳跃次数")]
    public  int PlayerJumpCount = 1;
    private bool isGround = false;
    private bool isJump = false;
    private bool isPressJump = false;


    void Start()
    {
        playerRB = transform.GetComponent<Rigidbody2D>();
        playerCollider = transform.GetComponent<Collider2D>();
        playerAnimator = transform.GetComponent<Animator>();
        playerTransform = transform.GetChild(0);

    }

    void Update()
    {
        isPressJump = Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        PlayerMove();
        PlayerJump();
        isGround = Physics2D.OverlapCircle(playerTransform.position, 0.1f, Ground);
    }

    void PlayerMove()
    {
        float horizentalNum = Input.GetAxis("Horizontal");
        float faceNum = Input.GetAxisRaw("Horizontal");

        //Debug.Log($"horizentalNum={horizentalNum}");
        //Debug.Log($"faceNum={faceNum}");

        playerRB.velocity = new Vector2(PlayerMoveSpeed * horizentalNum, playerRB.velocity.y);

        playerAnimator.SetFloat("MoveSpeed", Mathf.Abs( PlayerMoveSpeed * horizentalNum));
        if (faceNum != 0)
        {
            transform.localScale = new Vector3(-faceNum, transform.localScale.y, transform.localScale.z);
        }
    }

    void PlayerJump()
    {
        if (!isPressJump)
        {
            return;
        }
        if (PlayerJumpCount<0)
        {
            return;
        }
        if (isGround)
        {
            isGround = false;
        }

        playerRB.velocity = new Vector2(playerRB.velocity.x, PlayerJumpSpeed);
        playerAnimator.SetBool("IsJump", true);

        PlayerJumpCount--;





    }


}
