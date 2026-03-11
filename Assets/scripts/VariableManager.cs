using UnityEngine;

public class VariableManager : MonoBehaviour
{
    public ScenarioManager scenarioManager;
    public Data data;

    public void Play(Step step){
       if(step.state == StepState.START){
           data.variables.Add(step.variable);
       }
       if(step.state == StepState.END){
           data.variables.Remove(step.variable);
       }
       if(step.state == StepState.WAIT){

       }
       scenarioManager.PlayNextStep();
    }

    public bool ConditionsIsOk(string cond){
        if(cond == "")
            return true;
        string[] conditions = cond.Split(' ');
        Debug.Log("Les conditions a remplir sont "+cond);
        Debug.Log("conditions presente "+data.GetAllVariable());
        foreach(string condition in conditions){
            if(!data.variables.Contains(condition)){
                return false;
            }
        }
        return true;
    }
}
