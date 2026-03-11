using UnityEngine;
using System;

public class LookManager : MonoBehaviour
{
    public ScenarioManager scenarioManager;
    public Board board;

    public void Play(Step step){
        if(step.look == LookType.ALL_CAMERA){
            board.AllLookCamera();
        }
        if(step.look == LookType.ALL_CARD){
            board.AllLook(step.actor, false, true);
        }
        if(step.look == LookType.ALL_CARD_WITH_BODY){
            board.AllLook(step.actor, true, true);
        }
        if(step.look == LookType.X_TO_Y){
            try{
                Caractere c1 = board.findCardDisplay(step.actor).caractere;
                        Caractere c2 = board.findCardDisplay(step.actor2).caractere;
                        c1.Look(c2, false, true);
            }catch (Exception) {

             				}

        }
        if(step.look == LookType.NOTHING){
            board.RemoveAllLook();
        }
        scenarioManager.PlayNextStep();
    }

}
