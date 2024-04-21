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

    private float velocity = 2f;

    private bool isJumpInputDown;

    public override void OnEpisodeBegin()
    {
        gameManager.ResetScore();
        pipeManager.ResetPipes();

        rb.velocity = Vector2.zero;
        transform.localPosition = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(rb.velocity.y); // bird velocity y
        sensor.AddObservation((transform.localPosition.y + 0.66f) / 1.88f); // bird height normalized

        if (pipeManager.frontPipes.Count > 0)
        {
            sensor.AddObservation((pipeManager.frontPipes[0].transform.localPosition.x - transform.localPosition.x) / 1.44f); // pipe distance normalized
        }
        else
        {
            sensor.AddObservation(0f);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int move = actions.DiscreteActions[0];

        if (move == 1)
        {
            rb.Sleep();
            rb.AddForce(Vector2.up * velocity, ForceMode2D.Impulse);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AddReward(-1.0f);
        EndEpisode();
    }
}
