using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class FlappyBirdAgent : Agent
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PipeManager pipeManager;
    [SerializeField] private Rigidbody2D rb;

    private float velocity = 2.5f;
    private float rotationSpeed = 10f;

    private bool isJumpInputDown;

    public override void OnEpisodeBegin()
    {
        gameManager.score = 0;
        pipeManager.ClearPipe();

        rb.velocity = Vector2.zero;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int move = actions.DiscreteActions[0];

        if (move == 1)
        {
            rb.velocity = Vector2.up * velocity;
        }

        AddReward(0.1f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> actionSegment = actionsOut.DiscreteActions;

        actionSegment[0] = isJumpInputDown ? 1 : 0;

        isJumpInputDown = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            isJumpInputDown = true;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rb.velocity.y * rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AddReward(-1.0f);
        EndEpisode();
    }
}
