using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TurnManager : MonoBehaviour
{

    public GameObject zone;
    public GameObject zoneChoixCible;

    public Image portrait;
    public TextMeshProUGUI  nom;

    public Card card;
    public int competenceChoisie = 0;
    public int cibleChoisie = 0;

    public BattelManager battelManager;

    public List<Image> cibleImage;

    public List<Caractere> ciblePossible;
    public Caractere caractere;

    public void StartTurn(Card c, Caractere cara){
    Debug.Log("c'est le tour de "+c.name+" qui est "+c.type);
        caractere = cara;
        if(c.type == TypeCard.PERSONNAGE){
            card = c;
            zone.SetActive(true);
            portrait.sprite = card.portrait.head_battle;
            nom.text = c.nom;
        }else{
            card = c;
            ChoixAleatoire();
            ChoixCibleAllie();
            cibleChoisie = Random.Range(1, ciblePossible.Count+1);
            StartCoroutine(LanceCompetence());
        }

    }



    public void ChoixAleatoire(){
        competenceChoisie = 1;
    }

    public void ChoixCibleAllie(){
        ciblePossible = new List<Caractere>();

        foreach(Caractere hero in battelManager.Heros()){
            if(hero.card != null && hero.card.cara.hp > 0){
                ciblePossible.Add(hero);
            }
        }
    }

    public void Competence(int comp){
        competenceChoisie = comp;
        ChoixCible();
    }

    public void Cible(int cible){
        cibleChoisie = cible;
        zoneChoixCible.gameObject.SetActive(false);
        zone.SetActive(false);
        StartCoroutine(LanceCompetence());
    }

    IEnumerator LanceCompetence(){
        if(GetCompetence() != null && GetCompetence().m3d !=null){
            yield return new WaitForSeconds(0.5f); // fin FX
            caractere.DoAttack();
            yield return new WaitForSeconds(0.5f); // fin FX
            Debug.Log(card.name+" lance "+GetCompetence().name+" sur "+GetCible().card.name);
             GameObject obj = Instantiate(GetCompetence().m3d);
             Vector3 pos = Vector3.zero + GetCompetence().decalage;


             if(GetCompetence().cible == TypeCible.SELF){
                   obj.transform.SetParent(battelManager.currentTour.gameObject.transform );
                            obj.transform.localPosition = pos;

                //obj.transform.position = battelManager.currentTour.gameObject.transform.position + GetCompetence().decalage;
             }else{
               // obj.transform.position = GetCible().gameObject.transform.position + GetCompetence().decalage;
             obj.transform.SetParent(GetCible().gameObject.transform );
                            if(GetCible().card.type == TypeCard.BESTIAIRE)
                                pos.y *= -1;
                            else
                               obj.transform.Rotate(0f, 180f, 0f);

                            obj.transform.localPosition = pos;

             }
             Destroy(obj, 4f);
        }else{
            caractere.DoAttack();
        }
            yield return new WaitForSeconds(1f); // fin FX
        int damage = CalculateDamage( battelManager.currentTour.card.cara.atk, GetCible().card.cara.def, GetCompetence().power);
        AppliqueDamage(damage, GetCible());
        if(GetCible().cara.hp <= 0){
              GetCible().Die();
            yield return new WaitForSeconds(2.5f);
            battelManager.orders.RemoveAll(p => p == GetCible());
            battelManager.persos.RemoveAll(p => p == GetCible());
            CibleIsDead(GetCible());
            GetCible().IsDead();
             if(battelManager.AllDead(battelManager.Bestiaires())){
                 yield return new WaitForSeconds(1f);
                 StartCoroutine(battelManager.Gagner());
                 yield break;

             } if(battelManager.AllDead(battelManager.Heros())){
                  yield return new WaitForSeconds(1f);
                 battelManager.Perdu();
                 yield break;
             }

        }else{
            GetCible().TakeHit();
             yield return new WaitForSeconds(1f);


        }
        battelManager.EndTour();
    }

    public void CibleIsDead(Caractere cd){
        battelManager.RemoveDead();
    }

    public void AppliqueDamage(int damage, Caractere cd){
        cd.cara.hp -= damage;
        Debug.Log(damage+ " damage");
    }

    public int CalculateDamage(int atk, int def, int power)
    {
        float randomFactor = Random.Range(0.9f, 1.1f);

        float baseDamage = atk * power; // 7 * 10 = 70
        float defenseFactor = 1f / (1f + def * 0.25f);

        float damage = baseDamage * defenseFactor * randomFactor;

        return Mathf.Max(1, Mathf.RoundToInt(damage));
    }


    public Spell GetCompetence(){
        if(competenceChoisie == 1){
            return card.spells.spell1;
        }else if(competenceChoisie == 2){
            return card.spells.spell2;
          }else if(competenceChoisie == 3){
            return card.spells.spell3;
         }
         return null;
    }

    public Caractere GetCible(){
        return ciblePossible[cibleChoisie-1];
    }

    public void ChoixCible(){
        zoneChoixCible.gameObject.SetActive(true);
        foreach(Image img in cibleImage){
            img.gameObject.SetActive(false);
        }
        int k = 0;
        ciblePossible = new List<Caractere>();
        foreach(Caractere cible in battelManager.Bestiaires()){
                if(cible.card != null && cible.cara.hp > 0){
                    cibleImage[k].gameObject.SetActive(true);
                    cibleImage[k].sprite = cible.card.image;
                    k++;
                    ciblePossible.Add(cible);
                }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
