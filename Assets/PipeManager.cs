using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject pipe;

    private List<GameObject> pipes = new List<GameObject>();

    private float heightRange = 0.45f;
    private float maxTime = 1.5f;
    private float timer;

    private void Start()
    {
        SpawnPipe();
    }

    private void Update()
    {
        if (timer > maxTime)
        {
            SpawnPipe();
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    private void SpawnPipe()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange));
        GameObject pipeGO = Instantiate(pipe, spawnPos, Quaternion.identity);
        pipeGO.GetComponent<Pipe>().gameManager = gameManager;

        pipes.Add(pipeGO);
        Destroy(pipeGO, 5f);
    }

    public void ClearPipe()
    {
        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
        }

        pipes.Clear();
    }
}
