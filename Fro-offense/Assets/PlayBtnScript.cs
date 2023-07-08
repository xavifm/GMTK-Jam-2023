using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBtnScript : MonoBehaviour
{


    public void Play()
    {
        GameManager.Instance.ChangeGameState(GameState.PLAY);
        AudioManager.Instance.Play_OST("Play_OST");
        AudioManager.Instance.Play_SFX("CarHorn_SFX");
        gameObject.SetActive(false);
    }

}
