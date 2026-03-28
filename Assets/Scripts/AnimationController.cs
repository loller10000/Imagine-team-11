using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator.SetBool("inMainMenu", true);
    }
}
