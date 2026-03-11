    using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoManager : MonoBehaviour
{
    public Step step;
    public GameObject infoZone;
    public TextMeshProUGUI text;
    public ScenarioManager scenarioManager;

    public void Play(Step step){
       if(step.state == StepState.START){
           ShowInfo(step);
           scenarioManager.PlayNextStep();
       }
       if(step.state == StepState.END){
           CloseInfo();
           scenarioManager.PlayNextStep();
       }
       if(step.state == StepState.WAIT){

       }
    }

    public void ShowInfo(Step s){
       infoZone.gameObject.SetActive(true);
       step = s;
       text.text = step.dialogue;
    }

    public void Next(){
         scenarioManager.PlayNextStep();
    }

    public void CloseInfo(){
        infoZone.gameObject.SetActive(false);
   }

}
