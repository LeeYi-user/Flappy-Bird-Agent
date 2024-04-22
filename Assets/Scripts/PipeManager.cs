using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject pipePrefab;

    public List<GameObject> frontPipes = new List<GameObject>();
    public List<GameObject> backPipes = new List<GameObject>();

    private float heightRange = 0.3f;
    private float maxTime = 1.5f;
    private float timer;

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            SpawnPipe();
            timer = maxTime;
        }
    }

    private void SpawnPipe()
    {
        Vector3 spawnPos = transform.position + new Vector3(0f, Random.Range(-heightRange, heightRange), 0f);
        GameObject pipe = Instantiate(pipePrefab, spawnPos, Quaternion.identity, transform);
        pipe.GetComponent<Pipe>().gameManager = gameManager;
        pipe.GetComponent<Pipe>().pipeManager = this;
        frontPipes.Add(pipe);
    }

    public void ResetPipes()
    {
        foreach (GameObject pipe in frontPipes)
        {
            Destroy(pipe);
        }

        frontPipes.Clear();

        foreach (GameObject pipe in backPipes)
        {
            Destroy(pipe);
        }

        backPipes.Clear();

        timer = 0f;
    }
}
