using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    Rigidbody2D boxRigidBody2D;


    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimation;
    private void Awake()
    {
        boxRigidBody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GameLevelManager.Ins.playerMovement;
        _playerAnimation = GameLevelManager.Ins.playerMovement.GetComponent<PlayerAnimation>();
    }

    enum BALL_ROLL_TYPE { 
        LEFT,
        RIGHT,
        NONE
    }
    BALL_ROLL_TYPE typeBall = BALL_ROLL_TYPE.NONE;

    private float countRunTime = 0f;
    private const float RUN_TIME_TO_PUSH_BOX = 0.0f;
    void Update()
    {
        if (isCollisionBall)
        {
           
            if (_playerMovement.isMoveRight)
            {
                if (typeBall != BALL_ROLL_TYPE.RIGHT)
                {
                    typeBall = BALL_ROLL_TYPE.RIGHT;
                    countRunTime = 0f;
                }
                countRunTime += Time.deltaTime;
                if(countRunTime<RUN_TIME_TO_PUSH_BOX) return;
                if (_playerMovement.playerRigidbody2D.velocity.x < 10f && Mathf.Abs(_playerMovement.playerRigidbody2D.velocity.y) < 0.2f)
                {
                    if (transform.position.y < _playerMovement.transform.position.y)
                    {
                        boxRigidBody2D.velocity = new Vector2(-5f, 0f);
                    }
                }
            }
            else if (_playerMovement.isMoveLeft)
            {
                if (typeBall != BALL_ROLL_TYPE.LEFT)
                {
                    typeBall = BALL_ROLL_TYPE.LEFT;
                    countRunTime = 0f;
                }
                countRunTime += Time.deltaTime;
                if(countRunTime<RUN_TIME_TO_PUSH_BOX) return;
                if (_playerMovement.playerRigidbody2D.velocity.x > -10f && Mathf.Abs(_playerMovement.playerRigidbody2D.velocity.y) < 0.2f)
                {
                    if (transform.position.y < _playerMovement.transform.position.y)
                    {
                        boxRigidBody2D.velocity = new Vector2(5f, 0f);
                        
                    }
                }
            }


        }

        //UpdateBallType();
    }

    bool isCollisionBall = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && collision.gameObject.transform.position.y> transform.position.y && _playerMovement.collisionGround)
        {
            isCollisionBall = true;
        }
        else
        {
            isCollisionBall = false;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            isCollisionBall = false;
            countRunTime = 0f;
        }
    }
}
