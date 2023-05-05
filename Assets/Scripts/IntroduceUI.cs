using UnityEngine;

public class IntroduceUI : MonoBehaviour
{
    private const string CloseAnimationName = "IntroduceUIClose";

    [SerializeField]
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Close()
    {
        animator.Play(Animator.StringToHash(CloseAnimationName));
    }

    // Called by animation
    public void Disable()
    { 
        gameObject.SetActive(false);
    }
}
