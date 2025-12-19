using UnityEngine;

public class WalkSFX : MonoBehaviour
{
    [SerializeField] private AudioClip _stepClip;
    public void PlayStepSFX()
    {
        SfxManagar.Instance.PlaySFX(_stepClip);
    }
}
