using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isAR;
    public bool isTest;

    public Card ctest;
    public Data data;
    public Board board;
    public ScenarioManager scenarioManager;
    public StoryScene premiereScene;
    public List<GameObject> objectsToHideInAR;
    public List<GameObject> objectsToShowInAR;
    public TextMeshProUGUI text;

    public List<Caractere> caracteres;

    public void Log(string t){
        text.text += "\n"+t;
    }

    public void StartGame(){
        board.HideAllCards();
        scenarioManager.PlayScene(premiereScene);

    }

    void Start(){
        InitAR();
        DesactiveAllCard();
        if(!isAR)
           StartGame();
    }

    public void DesactiveAllCard(){
        foreach(Card c in data.cards){
            c.active = false;
            c.isPending = false;
        }
    }


    public void FixGround(){

    }

    public void InitAR(){
        foreach(GameObject obj in objectsToHideInAR){
            obj.SetActive(!isAR);
        }
        foreach(GameObject obj in objectsToShowInAR){
                    obj.SetActive(isAR);
                }
    }

    public void PremiereCarte(){
        scenarioManager.PlayScene(premiereScene);
    }
}
