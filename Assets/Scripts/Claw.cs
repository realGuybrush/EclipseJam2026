using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Claw : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    private float spawnWait, spawnDelta, speed, minSpeed;

    [SerializeField]
    private List<Device> devices;

    [SerializeField]
    private Vector3 startPos, endPos, max, min;

    private Vector3 followPos;

    private bool waiting, following, continuing;

    private Device newDevice;

    private void Update()
    {
        if (!waiting)
            Spawn();
        else if (following)
        {
            Follow();
            if(!continuing)
                TryDrop();
        }
    }

    private void Spawn()
    {
        waiting = true;
        following = true;
        continuing = false;
        transform.position = startPos;
        followPos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }

    private void Follow()
    {
        var velocity = followPos - transform.position;
        if (velocity.magnitude < minSpeed)
        {
            following = false;
            if (continuing)
                StartCoroutine("WaitForNextDrop", spawnWait + Random.Range(-spawnDelta, spawnDelta));
            return;
        }
        if (velocity.magnitude > speed)
            velocity = velocity.normalized * speed;
        if (velocity.magnitude < minSpeed)
            velocity = velocity.normalized * minSpeed;
        rigidBody.linearVelocity = velocity;
    }
    
    private void TryDrop()
    {
        if ((followPos - transform.position).magnitude < minSpeed)
        {
            following = false;
            Drop();
        }
    }

    private void Drop()
    {
        animator.SetTrigger("Open");
        Instantiate(devices[Random.Range(0, devices.Count)], transform.position, transform.rotation).SetFlight();
        StartCoroutine("WaitASec");
    }

    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(1);
        ContinueMovement();
    }

    private void ContinueMovement()
    {
        continuing = true;
        followPos = endPos;
        following = true;
    }

    private IEnumerator WaitForNextDrop(float time)
    {
        yield return new WaitForSeconds(time);
        waiting = false;
    }
}
