using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DonjonBoard : MonoBehaviour
{

    public List<Case> cases;
    public Case startCase;
    public DonjonManager manager;

    public Case currentCase;

    public List<Caractere> caracteres;
    public List<Vector3> declages;

    public GameObject tuiles;

    public void Init(DonjonManager donjonManager){

        foreach(Case c in cases){
            c.gameObject.SetActive(false);
        }
        startCase.gameObject.SetActive(true);
        currentCase = startCase;
        manager =donjonManager;
        manager.DisplayChoix();
    }

    public void HideForBattel(){
        tuiles.SetActive(false);
        manager.HideChoix();
    }

    public void ShowAfterBattel(){
        tuiles.SetActive(true);
        foreach(CardDisplay h in manager.board.cardOnBoard){
            if(h.card != null && (h.card.type == TypeCard.PERSONNAGE)){
                h.Hide();
            }
        }
        manager.DisplayChoix();
    }


    public void InstanceCaractere(List<Caractere> heros){
         foreach(Caractere hero in heros){
            Caractere caractere = Instantiate(hero.card.m3d).GetComponent<Caractere>();
            caractere.runSpeed = 20f;
            caracteres.Add(caractere);
             caractere.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
             caractere.transform.SetParent(startCase.gameObject.transform);
             caractere.transform.localScale *= 5f;
             Vector3 pos = Vector3.zero;
                pos.y = 0.55f;
        caractere.transform.localPosition = pos;

         }

    }
void OnDestroy()
{
    Debug.Log("DONJON BOARD DESTROYED");
}


    public void GoTo(Case newCase){
        if(manager.scenarioManager.IsBlockStep())
            return;
        Debug.Log(currentCase+" "+newCase);
        currentCase = newCase;
        currentCase.gameObject.SetActive(true);
        int i = 0;
        foreach(Caractere c in caracteres){
            c.RunTo(newCase.gameObject.transform, declages[i]);
            i++;
        }
        manager.scenarioManager.PlayEventOnclick(currentCase.events);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
