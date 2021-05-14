using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SealCompletion : MonoBehaviour
{
    private bool hasBeenCompleted;
    private float diluteValue;
    private bool shouldIncrease = true;

    public TextMeshProUGUI uiTextToChange;
    
    // Start is called before the first frame update
    void Start()
    {
        diluteValue = uiTextToChange.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenCompleted)
        {
            DilateText();
        }

    }

    public void CompleteObjective()
    {
        hasBeenCompleted = true;
    }

    private void DilateText()
    { 
        if (diluteValue < 0.7f && shouldIncrease)
        {
            diluteValue += uiTextToChange.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate) * Time.deltaTime;
        }
        else if(diluteValue >= 0.7f || !shouldIncrease)
        {
            shouldIncrease = false;
            diluteValue = uiTextToChange.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate);
            diluteValue -= Time.deltaTime;
        }

        uiTextToChange.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, diluteValue);

        if (diluteValue <= -1f)
        {
            gameObject.SetActive(false);
        } 
    }
        
}
