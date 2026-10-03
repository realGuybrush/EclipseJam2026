using UnityEngine;

public class Ambal : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float chance;

    private void Update()
    {
        if(Random.Range(0,100) < chance)
            animator.SetTrigger("Action");
    }
}
