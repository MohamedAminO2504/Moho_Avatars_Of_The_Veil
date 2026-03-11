using UnityEngine;
using UnityEngine.InputSystem;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public SpriteRenderer plan;
    public SpriteRenderer model;
    public GameObject obj;
    public Caractere caractere;
    public Board board;

    public LIG lig;
    public COL col;

    public void Show(){
        Show3dModel(card);
    }

    public void Hide(){
        Destroy(obj);
    }

    public void DisplayCard(Card c){
        this.card = c;
        this.plan.enabled = true;
        this.plan.sprite = card.image;

        if(CanShow3DModel(c))
            Show3dModel(c);
    }

    public bool CanShow3DModel(Card c){

        return c.type != TypeCard.LIEU
        && c.type != TypeCard.DONJON
        && c.type != TypeCard.SPECIAL;
    }

    public void ShowDonjon(){
        Destroy(obj);
        Vector3 pos = Vector3.zero;
        obj = Instantiate(card.donjon);
        obj.transform.SetParent(this.gameObject.transform);
                       obj.transform.localScale =  Vector3.one;
                        obj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        obj.transform.SetParent(this.gameObject.transform);
                    obj.transform.localPosition = pos;
    }
    public GameObject Show3dModel(Card c){
        if(obj != null)
            return null;
         if(c.type != TypeCard.PERSONNAGE && c.where != null && c.where != board.scenarioManager.currentLieu){
            return null;
        }
        if(c.m3d == null && c.active){
            this.model.enabled = true;
            this.model.sprite = card.model;
        }else{
            Vector3 pos = Vector3.zero;
            obj = Instantiate(c.m3d);
            if(c.type == TypeCard.PERSONNAGE || c.type == TypeCard.PNJ){
                obj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                obj.transform.SetParent(this.gameObject.transform);
                caractere = obj.GetComponent<Caractere>();
            }
            if( c.type == TypeCard.PNJ || c.type == TypeCard.BESTIAIRE){
                            obj.transform.SetParent(this.gameObject.transform);
                            obj.transform.localRotation = Quaternion.Euler(90f, 180f, 0f);
                caractere = obj.GetComponent<Caractere>();

            }
            if(c.type == TypeCard.SPECIAL){
                obj.transform.SetParent(this.gameObject.transform);
               obj.transform.localScale =  Vector3.one;
                obj.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

            }else{
                            obj.transform.localScale *= 30f;

            }
            obj.transform.SetParent(this.gameObject.transform);
            obj.transform.localPosition = pos;
            if(caractere != null){
                caractere.cardDisplay = this;
            }
        }
        return obj;
    }

    public void ClearCard(){
        this.card = null;
        this.plan.enabled = false;
        this.model.enabled = false;
        this.plan.sprite = null;
        this.model.sprite = null;
        Destroy(obj);
        this.gameObject.SetActive(false);
    }
}

public enum LIG{
    A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,
}

public enum COL{
    O1,O2,O3,O4,O5,O6,O7,O8,O9
}
