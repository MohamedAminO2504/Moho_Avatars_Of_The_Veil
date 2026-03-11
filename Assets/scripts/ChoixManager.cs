using UnityEngine;

public class ChoixManager : MonoBehaviour
{
    public GameObject zone;
    public ScenarioManager scenarioManager;

    public void Valider(){
        zone.SetActive(false);
        scenarioManager.PlayNextStep();

    }

    public void Annuler(){
        zone.SetActive(false);
    }

    public void ShowChoix(){
        zone.SetActive(true);
    }

}
