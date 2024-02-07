using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationRagdoll : MonoBehaviour
{
    [SerializeField] Collider myCollider;
    [SerializeField] float respawnTime = 30f;
    [SerializeField] bool hitCharacter;
    Rigidbody[] rigidbodies;
    bool isRagdoll = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (hitCharacter)
        {
            ToggleRagdoll(false);
            StartCoroutine(GetUp());
            hitCharacter = false;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (!isRagdoll && collision.gameObject.tag == "projectile")
    //    {
    //        ToggleRagdoll(false);
    //        StartCoroutine(GetUp());
    //    }
    //}

    private void ToggleRagdoll(bool isAnimating)
    {
        Debug.Log(isAnimating);
        isRagdoll = !isAnimating;

        myCollider.enabled = isAnimating;
        foreach (Rigidbody ragdollBone in rigidbodies)
        {
            ragdollBone.isKinematic = isAnimating;
        }
        GetComponent<Animator>().enabled = isAnimating;
        if (isAnimating)
        {
            RandomAnimation();
        }
    }

    private IEnumerator GetUp()
    {
        yield return new WaitForSeconds(respawnTime);
        ToggleRagdoll(true);
    }

    public void RandomAnimation()
    {
        int randomNum = Random.Range(0, 2);
        Debug.Log(randomNum);
        Animator animator = GetComponent<Animator>();
        if (randomNum == 0)
        {
            animator.SetTrigger("Walking");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }
}
