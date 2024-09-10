using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoution : MonoBehaviour
{
    public float slowMoveValue = 0.03f;
    public float returnToNormal = 5f;
    public GameObject slowMove, noDie;

    private bool isSlow = false, isNotDie = false;
    private void Update()
    {
        Time.timeScale = Math.Clamp(Time.timeScale, 0.0f, 1f);
        Time.fixedDeltaTime = Math.Clamp(Time.fixedDeltaTime, 0.0f, 0.02f);
        Time.timeScale += (1f / returnToNormal) * Time.unscaledDeltaTime;
        Time.fixedDeltaTime += (0.02f / returnToNormal) * Time.unscaledDeltaTime;
    }

    public void SlowMotionMove()
    {
            if (!isSlow)
            {
                Time.timeScale = slowMoveValue;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                isSlow = true;
                Destroy(slowMove);
            }
    }

    public void NoTimeToDie()
    {
            if (!isNotDie)
            {
                isNotDie = true;
                if (!PlayerHelth.isGoods)
                {
                    PlayerHelth.isGoods = true;
                    StartCoroutine(StopDie());
                }
                if (PlayerHelth.isGoods)
                    StopCoroutine(StopDie());

                Destroy(noDie);
            }
    }   

    IEnumerator StopDie()
    {
        yield return new WaitForSeconds(3);
        PlayerHelth.isGoods = false;
    }
}
