using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public float maxHealth;

  
    public float currentHealth;
    Ragdoll ragdoll;
    AiAgent agent;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    UIHealthBar healthBar;

    //UIHealthBar healthBar;

    public float blinkIntensity = 10;
    public float blinkDuration = 0.05f;
    float blinkTimer;

    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<AiAgent>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        currentHealth = maxHealth;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
            
        }


    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if(currentHealth <= 0.0f)
        {
            Die(direction);
        }

        blinkTimer = blinkDuration;
    }
   

    public void Die(Vector3 direction)
    {
        AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
        deathState.direction = direction;
        agent.stateMachine.ChangeState(AiStateId.Death);

       // ragdoll.ActivateRagdoll();
       // direction.y = 1;
       // ragdoll.ApplyForce(direction * dieForce);
       // healthBar.gameObject.SetActive(false);
       // Destroy(gameObject, 5);
    }


    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }


}
