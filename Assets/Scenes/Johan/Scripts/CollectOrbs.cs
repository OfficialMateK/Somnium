using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOrbs : MonoBehaviour
{
    private int orbsCollected = 0;
    private GameManager gameManager;

    [SerializeField] private GameObject[] shields;
    [SerializeField] private GameObject[] orbs;
    
    //UI
    private bool uiLocked = false;
    private UIChangeTrigger uiChangeTrigger;

    [SerializeField] private GameObject defaultUI;
    [SerializeField] private GameObject areaUI;
    [SerializeField] private OrbCollectUI orbCollectUI;

    // Start is called before the first frame update
    void Start()
    {
        //lockedDoor = GameObject.Find("LockedDoor").GetComponent<LockedDoor>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiChangeTrigger = GetComponent<UIChangeTrigger>();
    }

    public void PickupOrb()
    {
        //Kollar n�r spelaren plockar upp en orb och spawnar n�sta
        orbsCollected++;
        orbCollectUI.PickupOrb(orbsCollected);

        if(orbsCollected == 1)
        {
            orbs[0].SetActive(true);
        }
        else if(orbsCollected == 2)
        {
            orbs[2].SetActive(true);
        }
        else if(orbsCollected == 3)
        {
            CompleteObjective();
        }
    }

    private void CompleteObjective()
    {
        //Byter tillbaka UI:en och st�nger objectivet
        //lockedDoor.IncreaseObjectivesComplete();
        gameManager.CompleteCollection();
        uiChangeTrigger.triggerUIChange(2);
        gameObject.SetActive(false);
    }

    private void StartObjective()
    {
        //Byter UI och tar fram den f�rsta orben
        defaultUI.SetActive(false);
        areaUI.SetActive(true);

        uiChangeTrigger.SetInsideArea();
        orbs[1].SetActive(true);
    }

    private void LockInPlayer()
    {
        //F�rhindrar att UI:en byter tillbaka och att spelaren l�mnar omr�det
        uiLocked = true;
        shields[0].SetActive(true);
        shields[1].SetActive(true);
    }



    private void OnTriggerEnter(Collider other)
    {
        //Kollar n�r spelaren startar objectivet
        if (other.CompareTag("Player") && !uiLocked)
        {
            LockInPlayer();
            StartObjective();
        }
    }
}
