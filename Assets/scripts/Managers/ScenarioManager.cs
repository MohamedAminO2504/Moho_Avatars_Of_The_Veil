using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScenarioManager : MonoBehaviour
{

    public StoryScene currentScene;
    public DialogueManager dialogueManager;
    public PoseManager poseManager;
    public WaitManager waitManager;
    public InfoManager infoManager;
    public BattelManager battleManager;
    public DonjonManager donjonManager;
    public ChoixManager choixManager;
    public LookManager lookManager;
    public VariableManager variableManager;
    public GameManager gameManager;

    public List<Step> steps;
    public Data data;
    public Card currentLieu;
    public bool isInDonjon = false;

    public bool IsBlockStep(){
       return ((steps[0].type == StepType.DIALOGUE && steps[0].state == StepState.WAIT)
        || steps[0].type == StepType.BATTLE);
    }

    public void ChangeLieu(Card lieu){
        if(isInDonjon)
            return;
        if(lieu == currentLieu || steps[0].type == StepType.DIALOGUE || steps[0].type == StepType.BATTLE)
            return;
        currentLieu = lieu;
        poseManager.board.ActiveLieu(lieu);
        if(steps[0].type == StepType.WAIT_LIEU && currentLieu == steps[0].actor ){
            Debug.Log(steps[0].actor.type+ " " + steps[0].actor);
            Debug.Log(steps[0].actor.type+ " " + steps[0].actor);
            if(steps[0].actor.type == TypeCard.DONJON && steps[0].actor){
                isInDonjon = true;
                StartCoroutine(donjonManager.StartDonjon(steps[0]));
            }
              PlayNextStep();
        };
    }

    public void StartDonjon(){
        isInDonjon = true;
        StartCoroutine(donjonManager.StartDonjon(steps[0]));
    }

    public void Start(){
        data.variables = new List<string>();
        dialogueManager.scenarioManager = this;
        poseManager.scenarioManager = this;
        waitManager.scenarioManager = this;
        infoManager.scenarioManager = this;
        battleManager.scenarioManager = this;
        choixManager.scenarioManager = this;

    }

    public void SousQuete(Card c){
        Debug.Log("clique sur "+c);
        if(steps[0].type == StepType.DIALOGUE || steps[0].type == StepType.BATTLE)
                return;
        if(c.type != TypeCard.PERSONNAGE && c.where != null && c.where != currentLieu){
            return;
        }
        Debug.Log("verification sous evenement "+c);
        foreach(EventOnClick eoc in currentScene.eventOnClick){
             if(eoc.condition == "" && eoc.actor == c){
                addSteps(eoc.steps);
                PlayStep();
                return;
             }
             else if(variableManager.ConditionsIsOk(eoc.condition) && eoc.actor == c){
                         Debug.Log("condition ok ");

                 addSteps(eoc.steps);
                 PlayStep();
                 return;
             }
        }
    }

    public void PlayEventOnclick(List<EventOnClick> eventOnClick ){
        foreach(EventOnClick eoc in eventOnClick){
            if(!eoc.played){
                if(eoc.condition == ""){
                    addSteps(eoc.steps);
                    PlayStep();
                    if(!IsStepReapable(eoc.steps))
                        eoc.played = true;
                    return;
                  }
                  else if(eoc.condition != "" && data.variables.Contains(eoc.condition)){
                    addSteps(eoc.steps);
                    if(!IsStepReapable(eoc.steps))
                        eoc.played = true;
                    PlayStep();
                    return;
                }
            }
        }
    }

   public bool IsStepReapable(List<Step> steps){
        foreach(Step step in steps){
            if(step.type == StepType.DIALOGUE){
                return false;
            }
        }
        return true;
   }

    public void InitStep(StoryScene scene){
        foreach(Step step in scene.init){
            if(step.type == StepType.CHANGE_TYPE){
                step.actor.type = step.typeCard;
            }
            if(step.type == StepType.PERSO_MOVE){
                step.actor.where = step.actor2;
            }
        }
    }


    public void PlayScene(StoryScene scene){
        if(scene.skip){
            skip(scene);
            return;
        }
        InitStep(scene);
        this.currentScene = scene;
        Debug.Log("Play scene : "+this.currentScene.name);
        steps = new List<Step>(this.currentScene.steps);
        PlayStep();
    }

    public void skip(StoryScene scene){
        foreach(Step s in scene.steps){
           if(s.type == StepType.POSE ){
                if(s.actor.type != TypeCard.BESTIAIRE)
                    poseManager.board.PlayCard(s.actor, s);
           }
           if(s.type == StepType.CHANGE_SCENE){
               PlayScene(s.scene);
           }
            if(s.type == StepType.PERSO_MOVE){
                s.actor.where =s.actor2;
            }
        }
        PlayScene(scene.next);
    }

    public void addSteps(List<Step> ss){
        steps.InsertRange(0, ss);
    }

    public void PlayStep(){
        if(steps.Count == 0){
           PlayScene(currentScene.next);
           return;
        }
        Step step = steps[0];
        gameManager.Log(step.type+" "+step.actor);
        if(step.type == StepType.DIALOGUE){
            dialogueManager.Play(step);
        }
        if(step.type == StepType.POSE){
            poseManager.PlayPose(step);
        }



        if(step.type == StepType.ACTIVE_CARD){
            step.actor.active = true;

        }
        if(step.type == StepType.DESACTIVE_CARD){
            step.actor.active = false;
        }

        if(step.type == StepType.CHOIX){
            choixManager.ShowChoix();
        }

        if(step.type == StepType.WAIT_LIEU && gameManager.isTest){
                        PlayNextStep();

        }





        if(step.type == StepType.CHANGE_TYPE){
            step.actor.type = step.typeCard;
            poseManager.board.PlayCard(step.actor, step);
            poseManager.board.PlayCard(step.actor, step);
            PlayNextStep();
        }

        if(step.type == StepType.WAIT){
            waitManager.Play(step);
        }
        if(step.type == StepType.PERSO_MOVE){
            step.actor.where = step.actor2;
            PlayNextStep();

        }
        if(step.type == StepType.INFO){
            infoManager.Play(step);
        }
        if(step.type == StepType.LOOK){
            lookManager.Play(step);
        }
        if(step.type == StepType.VARIABLE){
            variableManager.Play(step);
        }
        if(step.type == StepType.BATTLE){
            battleManager.Play(step);
        }

        if(step.type == StepType.CHANGE_SCENE){
            PlayScene(step.scene);
        }

        if(step.type == StepType.DONJON){
            donjonManager.Play(step);

        }
            }

    public void PlayNextStep(){
        steps.RemoveAt(0);
        PlayStep();
    }

    public IEnumerator WaitAndPlayNextStep(float time){
        yield return new WaitForSeconds(time);
        PlayNextStep();
    }


    public Step GetNextStep(){
        if(steps.Count > 1){
            return steps[1];
        }
        return null;
    }
}
