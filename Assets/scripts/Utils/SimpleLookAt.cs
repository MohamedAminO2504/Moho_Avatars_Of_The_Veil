using UnityEngine;

public class SimpleLookAt : MonoBehaviour
{
    public Transform target;
    public Animator animator;

    [Range(0,1)]
    public float weight = 1f;


    void OnAnimatorIK(int layerIndex)
    {
        if (animator && target != null)
        {
            animator.SetLookAtWeight(weight, 0.3f, 0.6f, 1f, 0.5f);
            animator.SetLookAtPosition(target.position);
        }
    }
}
