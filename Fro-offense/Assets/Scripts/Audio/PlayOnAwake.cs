using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnAwake : MonoBehaviour
{
    enum SoundType { OST, SFX };
    [SerializeField] SoundType soundType = SoundType.OST;
    [SerializeField] string audioName;


    // Start is called before the first frame update
    void Start()
    {
        switch (soundType)
        {
            case SoundType.OST:
                AudioManager.Instance.Play_OST(audioName);

                break;

            case SoundType.SFX:
                AudioManager.Instance.Play_SFX(audioName);

                break;

            default:
                break;
        }

    }


}
