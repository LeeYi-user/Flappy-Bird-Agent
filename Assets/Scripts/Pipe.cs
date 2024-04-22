using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameManager gameManager;
    public PipeManager pipeManager;

    private float speed = 0.65f;

    private void FixedUpdate()
    {
        transform.localPosition += Vector3.left * speed * Time.deltaTime;

        if (transform.localPosition.x < -2.29f)
        {
            Destroy(gameObject);
            pipeManager.backPipes.RemoveAt(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.AddScore();
        pipeManager.frontPipes.RemoveAt(0);
        pipeManager.backPipes.Add(gameObject);
    }
}
