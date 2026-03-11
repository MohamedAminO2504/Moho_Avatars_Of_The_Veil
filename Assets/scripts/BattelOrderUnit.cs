using UnityEngine;
using UnityEngine.UI;

public class BattelOrderUnit : MonoBehaviour
{

    public Image portrait;
    public Image hp;

    public void Affiche(Caractere c){
        portrait.sprite = c.card.portrait.head_battle;
        hp.fillAmount = (float)c.cara.hp / c.cara.hpMax;
    }

    public void Deactivation(){
        this.gameObject.SetActive(false);
    }
}
