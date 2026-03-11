using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaitManager : MonoBehaviour
{
    public GameObject btnContinue;
    public ScenarioManager scenarioManager;

    public void Play(Step step){
       if(step.state == StepState.START){
            Wait(step);
       }
       if(step.state == StepState.END){
            EndWait();
       }
       if(step.state == StepState.WAIT){
           StartCoroutine(WaitTime(step));
       }
    }

    IEnumerator WaitTime(Step step){
            float timer = float.Parse(step.variable);
            yield return new WaitForSeconds(timer);
            scenarioManager.PlayNextStep();
    }

    public void Wait(Step s){
        //btnContinue.SetActive(true);
    }

    public void EndWait(){
        btnContinue.SetActive(false);
        scenarioManager.PlayNextStep();
        scenarioManager.PlayNextStep();
    }
}
