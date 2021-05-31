using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{

    public AiStateId initialState;
    public AiAgentConfig config;

    public AiStateMachine stateMachine;
    public NavMeshAgent navMeshAgent;
   
    public Ragdoll ragdoll;
    public SkinnedMeshRenderer mesh;
    public UIHealthBar ui;
    public Transform playerTransform;




    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        ui = GetComponentInChildren<UIHealthBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.ChangeState(initialState);


    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
