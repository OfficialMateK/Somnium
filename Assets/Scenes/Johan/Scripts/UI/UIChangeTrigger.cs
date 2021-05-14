using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChangeTrigger : MonoBehaviour
{
    public GameObject allObjectivesUI;
    public GameObject areaUI;
    public GameObject objective1Text;
    public GameObject objective2Text;
    public GameObject objective3Text;

    private bool insideArea = false;

    private void ChangeUI()
    {
        if (insideArea)
        {
            //När spelaren går in i arean - byt UI till den för objectivet
            areaUI.SetActive(true);
            allObjectivesUI.SetActive(false);
        }
        else
        {
            //När spelaren går ut ur arean eller klarat objektivet - byt UI till default
            areaUI.SetActive(false);
            allObjectivesUI.SetActive(true);
        }
        
    }

    public void triggerUIChange(int objectiveNumber)
    {
        insideArea = !insideArea;
        ChangeUI();

        switch (objectiveNumber)
        {
            case 1:
                objective1Text.GetComponent<SealCompletion>().CompleteObjective();
                break;
            case 2:
                objective2Text.GetComponent<SealCompletion>().CompleteObjective();
                break;
            case 3:
                objective3Text.GetComponent<SealCompletion>().CompleteObjective();
                break;
            default:
                break;
        }

           
    }


}
