using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public Sound sound1 = Sound.BubbleExplore;
    public Sound sound2 = Sound.Sleeping;
    
    public void PlaySound()
    {
        AudioManager.Instance.PlayAudio(sound1);
    }

    public void PlaySoundSecond()
    {
        AudioManager.Instance.PlayAudio(sound2);
    }
}
