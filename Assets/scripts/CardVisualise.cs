using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardVisualise : MonoBehaviour
{
    public Card card;
    public Transform spotModel3d;
    public Image carteImg;
    public Image portrait;
    public Image combatImg;
    public Image uniformeImg;

    private GameObject obj;

    public void Init(){
        if(card == null){
            return;
        }
        InitCostume();
        carteImg.sprite = card.image;
        portrait.sprite = card.portrait.basic;
        if(obj != null){
            Destroy(obj);
        }
        obj = Instantiate(card.m3d);
        obj.transform.SetParent(spotModel3d);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void InitCostume(){
        Model3D m3d = card.getModel3d(TypeModel3d.COMBAT);
        if(m3d != null){
               combatImg.sprite = m3d.image;
        }
        m3d = card.getModel3d(TypeModel3d.UNIFORME);
        if(m3d != null){
               uniformeImg.sprite = m3d.image;
        }
    }

    public void SetUniforme(){
        card.ChangeM3d(TypeModel3d.UNIFORME);
        Init();
    }

    public void SetCombat(){
        card.ChangeM3d(TypeModel3d.COMBAT);
        Init();
    }

    public void SetCivil(){
        card.ChangeM3d(TypeModel3d.CIVIL);
        Init();
    }

    public void Start(){
        Init();
    }
}
