using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DonjonManager : MonoBehaviour
{

    public GameObject donjonTouche;
    public Board board;
    public DonjonBoard donjonBoard;
    public ScenarioManager scenarioManager;
    public GameObject btnTop;
    public GameObject btnBot;
    public GameObject btnLeft;
    public GameObject btnRight;
    public Card card;
    public List<Caractere> heros;

    public void Play(Step step){
        if(step.state == StepState.START){
            scenarioManager.isInDonjon = true;
                    board.HidePNJ();
            StartCoroutine(StartDonjon(step));
        }
        if(step.state == StepState.END){
            EndDonjon();
            scenarioManager.isInDonjon = false;
            scenarioManager.ChangeLieu(step.actor);
            scenarioManager.waitManager.EndWait();
        }
        if(step.state == StepState.WAIT){

        }
      scenarioManager.PlayNextStep();
    }



    public IEnumerator StartDonjon(Step step){
        card = step.actor;
        GameObject obj = board.SwtichToDonjon(card);
        donjonBoard = null;
        while(donjonBoard == null){
            donjonBoard = obj.GetComponentInChildren<DonjonBoard>();
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log(donjonBoard);
        donjonBoard.Init(this);
                Debug.Log(donjonBoard);

        InitHeros();
                Debug.Log(donjonBoard);

        donjonTouche.SetActive(true);
                Debug.Log(donjonBoard);

        scenarioManager.PlayEventOnclick(donjonBoard.currentCase.events);

    }

    public void EndDonjon(){
        donjonBoard.HideForBattel();
        InitHerosBattel();
        card.active = false;
        scenarioManager.battleManager.battleZone.gameObject.SetActive(false);

    }


    public void InitBattelDonjon(){
        donjonBoard.HideForBattel();
        InitHerosBattel();
    }

    public void ExitBattle(){
        donjonBoard.ShowAfterBattel();
    }

    public void InitHeros(){
        heros = new List<Caractere>();
        foreach(CardDisplay h in board.cardOnBoard){
            if(h.card != null && (h.card.type == TypeCard.PERSONNAGE)){
                heros.Add(h.caractere);
                h.Hide();
            }
        }
        donjonBoard.InstanceCaractere(heros);
    }

    public void InitHerosBattel(){
        foreach(CardDisplay h in board.cardOnBoard){
            if(h.card != null && (h.card.type == TypeCard.PERSONNAGE)){
                h.Show();
            }
        }
    }

    public void DisplayChoix(){
        btnTop.SetActive(false);
               btnBot.SetActive(false);
               btnLeft.SetActive(false);
               btnRight.SetActive(false);

        if(donjonBoard.currentCase.top != null)
            btnTop.SetActive(true);
        if(donjonBoard.currentCase.bot != null)
            btnBot.SetActive(true);
        if(donjonBoard.currentCase.left != null)
            btnLeft.SetActive(true);
        if(donjonBoard.currentCase.right != null)
           btnRight.SetActive(true);
    }

    public void HideChoix(){
        Debug.Log("arara");
        btnTop.SetActive(false);
            btnBot.SetActive(false);
            btnLeft.SetActive(false);
            btnRight.SetActive(false);

    }

    public void GoDirection(int dir){
        if(dir == 1)
           donjonBoard.GoTo(donjonBoard.currentCase.top);
        if(dir == 2)
           donjonBoard.GoTo(donjonBoard.currentCase.right);
        if(dir == 3)
           donjonBoard.GoTo(donjonBoard.currentCase.bot);
        if(dir == 0)
              donjonBoard.GoTo(donjonBoard.currentCase.left);
        DisplayChoix();
    }
}
