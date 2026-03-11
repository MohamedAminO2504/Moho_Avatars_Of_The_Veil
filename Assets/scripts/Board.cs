using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public CardOnBoard card;
    public CardDisplay cardDisplay;

    public List<CardDisplay> cardOnBoard;

    public Transform lookNeutral;
    public ScenarioManager scenarioManager;
    public GameManager gameManager;
    public GameObject lieu;
    public Transform spawnLieu;

    public Transform spawnLieuAR;

    void Start(){
        if(gameManager.isAR)
            spawnLieu = spawnLieuAR;
        foreach(CardDisplay c in cardOnBoard){
            c.board = this;
        }
    }

    public void HidePNJ(){
         foreach(CardDisplay cd  in cardOnBoard){
            if(cd.card != null && cd.card.type == TypeCard.PNJ){
                cd.Hide();
            }
         }
    }
    public CardDisplay findCardDisplay(Card c){
         foreach(CardDisplay cd  in cardOnBoard){
            if(cd.card == c){
               return cd;
            }
         }
         return null;
    }

    public void RemoveAllLook(){
       foreach(CardDisplay cd  in cardOnBoard){
            if(cd.card != null && cd.caractere != null){
                cd.caractere.Look(lookNeutral, true, false);
                cd.caractere.useHead = false;
            }
         }
    }

       public void AllLook(Card actor, bool body, bool head){
           CardDisplay target = findCardDisplay(actor);
           foreach(CardDisplay cd  in cardOnBoard){
                if(cd.card != null && cd.caractere != null && target.card != cd.card){
                    cd.caractere.Look(target.caractere, body, head);
                }
             }
        }

    public void AllLookCamera(){
         foreach(CardDisplay cd  in cardOnBoard){
            if(cd.card != null && cd.caractere != null){
                cd.caractere.Look(Camera.main.transform, true, true);
                cd.caractere.useHead = true;
            }
         }
         foreach(Caractere caractere  in gameManager.caracteres){
             if(caractere.card != null && caractere.card.active == true){
                 caractere.Look(Camera.main.transform, true, true);
                 caractere.useHead = true;
             }
          }
    }

    public GameObject  SwtichToDonjon(Card donjon){
         if(this.lieu != null){
            Destroy(this.lieu);
        }
        return Instantiate(donjon.donjon,spawnLieu);

    }



    public void ActiveLieu(Card lieu){
        if(this.lieu != null){
            Destroy(this.lieu);
        }
        Debug.Log("instantiate lieu "+lieu);
        this.lieu = Instantiate(lieu.m3d,spawnLieu);

        foreach(CardDisplay cd  in cardOnBoard){

            if(cd.card != null && cd.card.where != null && cd.card.type == TypeCard.PNJ){
                if(cd.card.where == lieu){
                    cd.Show();
                }else{
                    cd.Hide();
                }

            }

        }
    }

    public void PlayCard(Card c, Step step)
    {
        if(gameManager.isAR){
           // PlayCardAR(c);
            return;
        }
        if(c.type == TypeCard.INFO)
            return;
        CardDisplay cd = FindCDifCartExist(c);

        if(cd == null){

         cd = findCardPlacement(step);

                    if(cd == null)
                        return;

            cd.gameObject.SetActive(true);
            cd.DisplayCard(c);
            if(c.type == TypeCard.BESTIAIRE)
                cd.card.cara = step.cara;

        }else{
            cd.ClearCard();

        }
    }

    public CardDisplay findCardPlacement(Step s){
        if(s.location == "")
            return null;
        string lig = s.location.Split("-")[0];
        string col = s.location.Split("-")[1];
        foreach( CardDisplay cd in cardOnBoard){
            if(cd.col+"" == "O"+col && cd.lig+"" == lig){
                return cd;
            }
        }
        return null;
    }

    public CardDisplay FindCDifCartExist(Card c){
        foreach( CardDisplay cd in cardOnBoard){
            if(cd.card == c){
                return cd;
            }
        }
        return null;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideAllCards(){
        foreach(CardDisplay c in cardOnBoard){
            c.gameObject.SetActive(false);
        }
    }


}
