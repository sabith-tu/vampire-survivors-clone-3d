using UnityEngine;

public class PlayerFootStepSound : MonoBehaviour
{
    [SerializeField] private AudioSource footSteps;

    [SerializeField] private AudioClip footStep1;
    [SerializeField] private AudioClip footStep2;
    [SerializeField] private AudioClip footStep3;
    [SerializeField] private AudioClip footStep4;
    [SerializeField] private AudioClip footStep5;

    

    public void OnFootStep()
    {
        PlayFootStep();
    }

    public void PlayFootStep()
    {
        int randomNumber = Random.Range(1, 6);
        AudioClip clipToPlay = footStep1;

        switch (randomNumber)
        {
            case 1:
                clipToPlay = footStep1;
                break;
            case 2:
                clipToPlay = footStep2;
                break;
            case 3:
                clipToPlay = footStep3;
                break;
            case 4:
                clipToPlay = footStep4;
                break;
            case 5:
                clipToPlay = footStep5;
                break;
        }

        footSteps.PlayOneShot(clipToPlay);
    }
}