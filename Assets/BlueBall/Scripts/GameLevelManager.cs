﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Ins;

    private void Awake()
    {
        Ins = this;
    }

    public PlayerMovement playerMovement;
    [Header("JUMP EFX")]
    public Transform efxTranform;
    public ParticleSystem jumpEfxPrefab;

    public int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        Config.currGameState = Config.GAMESTATE.PLAYING;

        GamePlayManager.Ins.InitStarGroup(maxStar);
        Config.currIDBallRescue = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }


    #region MOVE
    public void SetMoveLeft(bool _isMoveLeft) {
        if (Config.currGameState == Config.GAMESTATE.PLAYING)
        {
            playerMovement.SetBallMoveLeft(_isMoveLeft);
        }
    }


    public void SetMoveRight(bool _isMoveRight) {
        if (Config.currGameState == Config.GAMESTATE.PLAYING)
        {
            playerMovement.SetBallMoveRight(_isMoveRight);
        }
    }

    public void SetJump() {
        if (Config.currGameState == Config.GAMESTATE.PLAYING)
        {
            playerMovement.SetBallJump();
        }
    }
    #endregion

    private Vector3 efxJump_Offset = new Vector3(0f, -0.55f, 0f);
    public void ShowEfx_JumpStart() {
        ParticleSystem efxJump = Instantiate(jumpEfxPrefab, efxTranform);
        efxJump.transform.localPosition = playerMovement.transform.position + efxJump_Offset;
        efxJump.Play();
    }

    public void ShowEfx_JumpEnd() {
        ParticleSystem efxJump = Instantiate(jumpEfxPrefab, efxTranform);
        efxJump.transform.localPosition = playerMovement.transform.position + efxJump_Offset;
        efxJump.Play();
    }


    #region FINSIH
    public FinishPoint finishPoint;

    public void SetGameFinish() {
        if (Config.currGameState != Config.GAMESTATE.FINISH)
        {
            Config.currGameState = Config.GAMESTATE.FINISH;

            playerMovement.SetMoveFinish(finishPoint.gameObject.transform.position);
        }

    }


    public void ShowEffectFinish_Finished() {
        //Debug.Log("ShowEffectFinish_Finished");
       Config.currGameState = Config.GAMESTATE.FINISH;
       GamePlayManager.Ins.ShowWinPopup();
    }
    #endregion



    #region STAR
    [Header("STAR")]
    public int maxStar;
    public int currStar = 0;
    public void SetAddStar(ItemStar _itemStar) {
        _itemStar.SetMoveToFinished(GamePlayManager.Ins.starGroup.iconStars[GamePlayManager.Ins.starGroup.countStar].transform.position);
        GamePlayManager.Ins.SetAddStar();
        currStar++;
        if (currStar >= maxStar) {
            finishPoint.SetOpenDoor();
        }
    }
    #endregion


    #region COIN
    //[Header("COIN")]
    public int countAddCoin = 0;
    public void SetAddCoin() {
        Debug.Log("SetAddCoin");
        countAddCoin += Config.COIN_REWARD;
    }
    #endregion


    #region SPIKE
    public void SetBallSpike(ItemSpike _itemSpike) {
        SetBallBeHit(_itemSpike.speedAddBall, 1);
    }
    #endregion

    #region SPRING
    public void SetBallSpring(ItemSpring _itemSpring)
    {
        playerMovement.SetBallSpring(_itemSpring.speedAddBall);
    }

    #endregion


    #region BEHIT
    public bool isPlayerBehit = false;
    public void SetBallBeHit(Vector2 _speedAddBall, int damage) {
        if (Config.currGameState == Config.GAMESTATE.PLAYING)
        {
            playerMovement.SetBallBeHit(_speedAddBall);
            if (!isPlayerBehit)
            {
                isPlayerBehit = true;
                GamePlayManager.Ins.SetBeHit(damage);
                StartCoroutine(BallBeHit_Sheild());
            }
        }
    }

    public void SetBallDead() {
        Config.currGameState = Config.GAMESTATE.GAMEOVER;

        if (Config.GetHeart() > 0 && currCheckPoint != null)
        {
            Debug.Log("SetBallDeadSetBallDeadSetBallDead");

            playerMovement.SetBallDead();

            StartCoroutine(BallRevive_IEnumerator());
        }
        else {
            StartCoroutine(Wait(1.5f));
            GamePlayManager.Ins.ShowLosePopup();
        }
    }
    public IEnumerator Wait(float _time)
    {
        yield return new WaitForSeconds(_time);
    }
    public IEnumerator BallRevive_IEnumerator() {
        yield return new WaitForSeconds(2f);
        Debug.Log("BallRevive_IEnumeratorBallRevive_IEnumeratorBallRevive_IEnumerator");
        playerMovement.SetBallRevive_Pre(currCheckPoint.transform.position + new Vector3(0f, 3f, 0f));
        yield return new WaitForSeconds(1f);
        GameObject heartReviveEfx = Instantiate<GameObject>(Resources.Load<GameObject>("heartRevive"), efxTranform);
        heartReviveEfx.gameObject.transform.position = GamePlayManager.Ins.heartGroup.transform.position;
        heartReviveEfx.gameObject.transform.DOMove(currCheckPoint.transform.position, 0.8f).SetEase(Ease.OutQuad);
        heartReviveEfx.gameObject.GetComponent<SpriteRenderer>().DOFade(0f, 1f).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(1f);
        Destroy(heartReviveEfx);
        Config.currGameState = Config.GAMESTATE.PLAYING;
        playerMovement.SetBallRevive(currCheckPoint.transform.position + new Vector3(0f, 3f, 0f));
        Config.SetHeart(Config.GetHeart() - 1);
        GamePlayManager.Ins.SetRevive();

        isPlayerBehit = true;
        StartCoroutine(BallBeHit_Sheild());
    }


    public IEnumerator BallBeHit_Sheild(){
        yield return new WaitForSeconds(2f);
        isPlayerBehit = false;
        playerMovement.gameObject.GetComponent<PlayerAnimation>().SetEfxBeHit_End();
    }
    #endregion

    #region KILLENEMY
    public void SetBall_KillEnemy(Vector2 _speedAddBall)
    {
        playerMovement.SetBall_KillEnemy(_speedAddBall);
    }

    #endregion

    #region CHECKPOINT
    [Header("CHECK POINT")]
    public CheckPoint currCheckPoint = null;

    public void SetCheckPoint(CheckPoint _checkPoint) {
        currCheckPoint = _checkPoint;
    }

    #endregion

}
