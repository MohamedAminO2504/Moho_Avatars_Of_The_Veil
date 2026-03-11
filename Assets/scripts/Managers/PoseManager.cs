using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PoseManager : MonoBehaviour
{
    public Step step;
    public Board board;
    public Card card;
    public ScenarioManager scenarioManager;
    public GameManager gameManager;

    public Image cardImage_v;
    public TextMeshProUGUI text_v;

    public void PlayPose(Step s ){
       step = s;
       card = s.actor;
        if(gameManager.isAR && card.type != TypeCard.INFO){
            card.isPending = true;
        }else{
           if(card.type == TypeCard.BESTIAIRE){
            card.cara = s.cara;
           }
            cardImage_v.gameObject.SetActive(true);

            cardImage_v.sprite = card.image;
            card.active=true;
        }
    }

    public void PoseCard(){
        if(gameManager.isAR && card.type != TypeCard.INFO){
            Debug.Log("Pose AR");
            card.isPending = false;
            scenarioManager.PlayNextStep();

        }else{
            Debug.Log("else");
            board.PlayCard(card, step);
            Reset();
            scenarioManager.PlayNextStep();
        }
    }

    public void PoseLieuAR(Card card){
        board.ActiveLieu(card);
    }

    public void Reset(){
        card = null;
        step = null;
        cardImage_v.sprite = null;
        cardImage_v.gameObject.SetActive(false);
    }




}
