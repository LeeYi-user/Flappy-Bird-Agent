using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject pipe;

    public List<GameObject> frontPipes = new List<GameObject>();
    public List<GameObject> backPipes = new List<GameObject>();

    private float heightRange = 0.3f;
    private float maxTime = 1.5f;
    private float timer;

    private void Start()
    {
        SpawnPipe();
    }

    private void Update()
    {
        if (timer >= maxTime)
        {
            SpawnPipe();
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    private void SpawnPipe()
    {
        Vector3 spawnPos = transform.position + new Vector3(0f, Random.Range(-heightRange, heightRange), 0f);
        GameObject pipeGO = Instantiate(pipe, spawnPos, Quaternion.identity);
        pipeGO.GetComponent<Pipe>().gameManager = gameManager;
        pipeGO.GetComponent<Pipe>().pipeManager = this;
    }

    public void ClearPipe()
    {
        foreach (GameObject pipe in frontPipes)
        {
            Destroy(pipe);
        }

        foreach (GameObject pipe in backPipes)
        {
            Destroy(pipe);
        }
    }
}
