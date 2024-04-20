using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameManager gameManager;
    public PipeManager pipeManager;

    private float speed = 0.65f;

    bool inFront = true;

    private void Start()
    {
        pipeManager.frontPipes.Add(gameObject);
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.localPosition += Vector3.left * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inFront = false;
        pipeManager.frontPipes.RemoveAt(0);
        pipeManager.backPipes.Add(gameObject);
        gameManager.score++;
    }

    private void OnDestroy()
    {
        if (inFront)
        {
            pipeManager.frontPipes.RemoveAt(0);
        }
        else
        {
            pipeManager.backPipes.RemoveAt(0);
        }
    }
}
