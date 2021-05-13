using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidbody2D;
    [Title("MOVE CONFIG")]
    public float MOVE_MAX_SPEED = 5f;
    public float MOVE_SPEED_INCREASE = 10f;
    public float MOVE_SPEED_DECREASE = 15f;

    [Title("JUMP CONFIG")]
    public float JUMP_SPEED = 10f;
    public float JUMPE_SPEED_UP = 1f;
    public float JUMPE_SPEED_DOWN = 1.5f;

    enum MOVE_DIRECTION {
        NONE,
        LEFT,
        RIGHT
    }

    private MOVE_DIRECTION moveDirection = MOVE_DIRECTION.NONE;


    public PlayerAnimation playerAnimation;

    // Start is called before the first frame update
    void Start()
    {

    }
    float speedX = 0;
    float speedY = 0;
    // Update is called once per frame
    void Update()
    {

        if (Config.currGameState == Config.GAMESTATE.PLAYING)
        {
            speedX = 0;
            speedY = playerRigidbody2D.velocity.y;
            //Debug.Log(speedY);
            if (isMoveRight)
            {

                //playerRigidbody2D.velocity = new Vector2(MOVE_MAX_SPEED, playerRigidbody2D.velocity.y);
                playerRigidbody2D.freezeRotation = false;
                speedX = playerRigidbody2D.velocity.x + MOVE_SPEED_INCREASE * Time.deltaTime;
                //Debug.Log("isMoveRight:" + speedX);
                if (speedX >= MOVE_MAX_SPEED)
                {
                    speedX = MOVE_MAX_SPEED;
                }

            }
            else if (isMoveLeft)
            {
                //playerRigidbody2D.velocity = new Vector2(-MOVE_MAX_SPEED, playerRigidbody2D.velocity.y);
                playerRigidbody2D.freezeRotation = false;

                speedX = playerRigidbody2D.velocity.x - MOVE_SPEED_INCREASE * Time.deltaTime;
                if (speedX <= -MOVE_MAX_SPEED)
                {
                    speedX = -MOVE_MAX_SPEED;
                }
            }
            else
            {
                //speedX = 0;
                //playerRigidbody2D.freezeRotation = true;
                if (moveDirection == MOVE_DIRECTION.RIGHT)
                {
                    speedX = playerRigidbody2D.velocity.x - MOVE_SPEED_DECREASE * Time.deltaTime;
                    if (speedX <= 0)
                    {
                        speedX = 0;
                        moveDirection = MOVE_DIRECTION.NONE;
                    }
                }
                else if (moveDirection == MOVE_DIRECTION.LEFT)
                {
                    speedX = playerRigidbody2D.velocity.x + MOVE_SPEED_DECREASE * Time.deltaTime;
                    if (speedX >= 0)
                    {
                        speedX = 0;
                        moveDirection = MOVE_DIRECTION.NONE;
                    }
                }
            }
            if (!isGrounded)
            {
                if (speedY > 0)
                {
                    speedY += -JUMPE_SPEED_UP * Time.deltaTime;
                }
                else
                {
                    speedY += -JUMPE_SPEED_DOWN * Time.deltaTime;
                }
            }

            if (isSpecialMoveX) {
                if (playerRigidbody2D.velocity.x < 0)
                {
                    speedX = playerRigidbody2D.velocity.x + SPECIAL_MOVE_SPEED_DECREASE * Time.deltaTime;

                    if (speedX > 0)
                    {
                        isSpecialMoveX = false;
                    }
                }
                else {
                    speedX = playerRigidbody2D.velocity.x - SPECIAL_MOVE_SPEED_DECREASE * Time.deltaTime;
                    if (speedX < 0)
                    {
                        isSpecialMoveX = false;
                    }
                }
            }
            //Debug.Log("SPEED Y:" + speedY);
            playerRigidbody2D.velocity = new Vector2(speedX, speedY);

            bool newIsGrounded = CheckIsGrounded();
            if (isGrounded != newIsGrounded)
            {
                isGrounded = newIsGrounded;
                if (isGrounded)
                {
                    //Vua cham dat
                    SetFallDown_Finished();
                }
            }
        }
        else {
            playerRigidbody2D.velocity = Vector2.zero;
        }


        typeBall = BALL_ROLL_TYPE.NONE;
        if (isMoveRight)
        {
            // if (playerRigidbody2D.velocity.x < MOVE_MAX_SPEED && Mathf.Abs(playerRigidbody2D.velocity.y) < 0.5f)
            {
                typeBall = BALL_ROLL_TYPE.RIGHT;
            }
        }
        else if (isMoveLeft)
        {
            // if (playerRigidbody2D.velocity.x > -MOVE_MAX_SPEED && Mathf.Abs(playerRigidbody2D.velocity.y) < 0.5f)
            {
                typeBall = BALL_ROLL_TYPE.LEFT;

            }
        }

        UpdateBallType();
    }


    enum BALL_ROLL_TYPE
    {
        LEFT,
        RIGHT,
        NONE
    }
    BALL_ROLL_TYPE typeBall = BALL_ROLL_TYPE.NONE;
    BALL_ROLL_TYPE lastTypeBall = BALL_ROLL_TYPE.NONE;

    public void UpdateBallType()
    {
        if (lastTypeBall != typeBall)
        {
            lastTypeBall = typeBall;

            if (typeBall == BALL_ROLL_TYPE.LEFT)
            {
                playerAnimation.SetAutoRotate(false);
            }
            else if (typeBall == BALL_ROLL_TYPE.RIGHT)
            {
                playerAnimation.SetAutoRotate(true);
            }
            else
            {
                playerAnimation.StopAutoRotate();
            }
        }
    }


    private void FixedUpdate()
    {

    }

    #region MOVE
    [Title("MOVE")]
    public bool isMoveRight = false;
    public bool isMoveLeft = false;
    public void SetBallMoveRight(bool _isMoveRight)
    {
        if (_isMoveRight)
        {
            moveDirection = MOVE_DIRECTION.RIGHT;
            transform.localScale = new Vector2(1, 1);
            isMoveRight = true;
            isMoveLeft = false;
            playerRigidbody2D.freezeRotation = true;
            playerRigidbody2D.velocity = new Vector2(0, playerRigidbody2D.velocity.y);
        }
        else
        {
            isMoveRight = false;
        }
    }

    public void SetBallMoveLeft(bool _isMoveLeft)
    {
        if (_isMoveLeft)
        {
            moveDirection = MOVE_DIRECTION.LEFT;
            transform.localScale = new Vector2(-1, 1);
            isMoveLeft = true;
            isMoveRight = false;
            playerRigidbody2D.freezeRotation = true;
            playerRigidbody2D.velocity = new Vector2(0, playerRigidbody2D.velocity.y);
        }
        else
        {
            isMoveLeft = false;
        }
    }
    #endregion


    public bool CheckMoveLeft() {
        if (playerRigidbody2D.velocity.x < 0) return true;
        return false;
    }

    #region JUMP
    public bool isJump = false;
    public void SetBallJump()
    {
        if (isGrounded)
        {
            Debug.Log("SetBallJumpSetBallJumpSetBallJumpSetBallJump");
            isJump = true;
            //playerRigidbody2D.AddForce(Vector2.up * FORCE_JUMP);
            //Debug.Log("111111:" + playerRigidbody2D.velocity);
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, JUMP_SPEED);
            //Debug.Log("22222:"+playerRigidbody2D.velocity);

            SoundManager.instance.SFX_Jump();

            GameLevelManager.Ins.ShowEfx_JumpStart();
        }
    }

    #endregion


    #region CHECK_GROUND
    public bool isGrounded = false;
    public LayerMask groundLayer;

    public bool CheckIsGrounded()
    {
        Vector2 boundSize = gameObject.GetComponent<CircleCollider2D>().bounds.size;
        boundSize = new Vector2(boundSize.x - 0.1f, boundSize.y);
        RaycastHit2D hit2D = Physics2D.BoxCast(gameObject.GetComponent<CircleCollider2D>().bounds.center, boundSize, 0, Vector2.down, 0.1f, groundLayer);

        return hit2D.collider != null;
    }

    #endregion


    #region FALL_DOWN
    public void SetFallDown_Finished()
    {
        GameLevelManager.Ins.ShowEfx_JumpEnd();
        isJump = false;
    }
    #endregion



    #region MOVE_FINISHED
    Sequence sequenceFinish;
    public void SetMoveFinish(Vector3 posFinish){
        sequenceFinish = DOTween.Sequence();
        sequenceFinish.Insert(0f, gameObject.transform.DOMove(posFinish, 1f).SetEase(Ease.OutQuad));
        sequenceFinish.Insert(0f, gameObject.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.OutQuad));
        sequenceFinish.InsertCallback(1.1f, () =>
        {
            SetMoveFinish_Finished();
        });
    }


    public void SetMoveFinish_Finished() {
        GameLevelManager.Ins.ShowEffectFinish_Finished();
    }
    #endregion




    bool isSpecialMoveX = false;
    public float SPECIAL_MOVE_SPEED_DECREASE = 20f;
    #region SPIKE
    public void SetBallSpike(Vector2 _speedSpike) {
        SoundManager.instance.SFX_Hurt();
        if (CheckMoveLeft())
        {
            playerRigidbody2D.velocity = new Vector2(-_speedSpike.x, _speedSpike.y);
        }
        else {
            playerRigidbody2D.velocity = new Vector2(_speedSpike.x, _speedSpike.y);
        }
        isSpecialMoveX = true;
        playerAnimation.SetAnimHit();
    }
    #endregion

    #region SPIKE
    public void SetBallSpring(Vector2 _speedSpike)
    {
        if (CheckMoveLeft())
        {
            playerRigidbody2D.velocity = new Vector2(-_speedSpike.x, _speedSpike.y);
        }
        else
        {
            playerRigidbody2D.velocity = new Vector2(_speedSpike.x, _speedSpike.y);
        }
    }
    #endregion


    #region BEHIT
    public void SetBallBeHit(Vector2 _speedAddBall)
    {
        Debug.Log("Player SetBallBeHit");
        
        SoundManager.instance.SFX_Hurt();
        if (CheckMoveLeft())
        {
            playerRigidbody2D.velocity = new Vector2(-_speedAddBall.x, _speedAddBall.y);
        }
        else
        {
            playerRigidbody2D.velocity = new Vector2(_speedAddBall.x, _speedAddBall.y);
        }
        isSpecialMoveX = true;
        playerAnimation.SetAnimHit();
    }
    #endregion


    #region DEAD
    public void SetBallDead() {
        SoundManager.instance.SFX_BallExplosion();
        playerRigidbody2D.velocity = Vector2.zero;
        playerRigidbody2D.gravityScale = 0;
        playerAnimation.SetBallDead();
    }

    public void SetBallRevive_Pre(Vector3 pos) {
        gameObject.transform.position = pos;
    }

    public void SetBallRevive(Vector3 pos) {
        playerRigidbody2D.gravityScale = 2;
        gameObject.transform.DORotate(Vector3.zero, 0.2f).SetEase(Ease.Linear);

        playerAnimation.SetBallRevive();


        isMoveRight = false;
        isMoveLeft = false;
    }
    #endregion

    #region KILLENEMY
    public void SetBall_KillEnemy(Vector2 _speedAddBall)
    {
        if (CheckMoveLeft())
        {
            playerRigidbody2D.velocity = new Vector2(-_speedAddBall.x, _speedAddBall.y);
        }
        else
        {
            playerRigidbody2D.velocity = new Vector2(_speedAddBall.x, _speedAddBall.y);
        }
        isSpecialMoveX = true;
    }
    #endregion

    public bool collisionGround;
    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.collider.gameObject.layer==8)
            {
                collisionGround = true;
                return;
            }
        }
        collisionGround = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer==8)
        {
            collisionGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer==8)
        {
            collisionGround = false;
        }
    }
}
