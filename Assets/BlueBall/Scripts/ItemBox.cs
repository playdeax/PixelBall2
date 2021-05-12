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
    BALL_ROLL_TYPE lastTypeBall = BALL_ROLL_TYPE.NONE;

    // Update is called once per frame
    void Update()
    {
        typeBall = BALL_ROLL_TYPE.NONE;
        if (isCollisionBall)
        {
            if (_playerMovement.isMoveRight)
            {
                if (_playerMovement.playerRigidbody2D.velocity.x < 10f && Mathf.Abs(_playerMovement.playerRigidbody2D.velocity.y) < 0.2f)
                {
                    if (transform.position.y < _playerMovement.transform.position.y)
                    {
                        boxRigidBody2D.velocity = new Vector2(-1.2f, 0f);
                    }
                    typeBall = BALL_ROLL_TYPE.RIGHT;
                }
            }
            else if (_playerMovement.isMoveLeft)
            {
                if (_playerMovement.playerRigidbody2D.velocity.x > -10f && Mathf.Abs(_playerMovement.playerRigidbody2D.velocity.y) < 0.2f)
                {
                    if (transform.position.y < _playerMovement.transform.position.y)
                    {
                        boxRigidBody2D.velocity = new Vector2(1.2f, 0f);
                        
                    }
                    typeBall = BALL_ROLL_TYPE.LEFT;
                }
            }


        }

        //UpdateBallType();
    }

    bool isCollisionBall = false;

    public void UpdateBallType() {
        if (lastTypeBall != typeBall) {
            lastTypeBall = typeBall;
            Debug.Log("UpdateBallTypeUpdateBallTypeUpdateBallTypeUpdateBallType");
            Debug.Log(lastTypeBall);
            if (typeBall == BALL_ROLL_TYPE.LEFT)
            {
                _playerAnimation.SetAutoRotate(false);
            }
            else if (typeBall == BALL_ROLL_TYPE.RIGHT)
            {
                _playerAnimation.SetAutoRotate(true);
            }
            else {
                _playerAnimation.StopAutoRotate();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("OnCollisionEnter2D BALLLLLLLLLLLLLLL");
            isCollisionBall = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("OnCollisionExit2D BALLLLLLLLLLLLLLL");
            isCollisionBall = false;
        }
    }
}
