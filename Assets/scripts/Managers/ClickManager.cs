using UnityEngine;
using UnityEngine.InputSystem;

public class ClickManager : MonoBehaviour
{
    public ScenarioManager scenarioManager;
    public Data data;

    void Update(){
        if (Mouse.current.leftButton.wasPressedThisFrame){
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit)){
                CardDisplay card = hit.collider.GetComponentInParent<CardDisplay>();
                if (card != null && card.card != null){
                    Click(card.card);
                }
            }
        }
    }

    public void Click(Card card){
        if(card.type == TypeCard.LIEU || card.type == TypeCard.DONJON){
           scenarioManager.ChangeLieu(card);
        }else{
           scenarioManager.SousQuete(card);
        }
    }
}
