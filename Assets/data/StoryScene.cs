using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
[CreateAssetMenu(fileName = "StoryScene", menuName = "Scenario/StoryScene")]
public class StoryScene : ScriptableObject
{
    public List<Step> steps;
    public bool skip;
    public StoryScene next;
    public List<EventOnClick> eventOnClick;
    public List<Step> init;

}

[System.Serializable]
public class EventOnClick{
     public Card actor;
     public string condition;
     public List<Step> steps;
     public bool played = false;
}

[System.Serializable]
public class Step{
    public StepType type;
    public string location;
    public Card actor;
    public Card actor2;

    [TextArea(3, 8)]
    public string dialogue;
    public TypeCard typeCard;
    public string variable;
    public Caracteristique cara;
    public StoryScene scene;
    public StepState state = StepState.WAIT;
    public LookType look;
    public List<BattleParam> battleParam;
    public BattleType battleType;

    public BattleParam GetAleatoireBattle()
    {
        if (battleParam == null || battleParam.Count == 0)
            return null;

        int total = battleParam.Sum(b => Mathf.Max(0, b.pourcentage));
        if (total <= 0)
            return battleParam[0];
        int roll = Random.Range(0, total);
        int cumul = 0;
        foreach (var battle in battleParam){
            cumul += Mathf.Max(0, battle.pourcentage);
            if (roll < cumul)
                return battle;
        }
        return battleParam[battleParam.Count - 1];
    }
}

[System.Serializable]
public class BattleParam{
    public string nom;
    public int pourcentage;
    public int niveau;
    public List<Card> bestiaire;
}

public enum BattleType{
    RANDOM, EVENT, BOSS, SPECIAL
}

public enum LookType{
    X_TO_Y, NOTHING, ALL_CAMERA, ALL_CARD, ALL_CARD_WITH_BODY
}

public enum StepState{
    START, END, WAIT
}

public enum StepType{
    DIALOGUE, BATTLE, WAIT,
    POSE, CHANGE_TYPE,
    CHANGE_SCENE,
    WAIT_LIEU, PERSO_MOVE,
    ACTIVE_CARD, DESACTIVE_CARD, CHOIX, INFO, LOOK, VARIABLE, DONJON
}