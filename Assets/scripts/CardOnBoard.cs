using UnityEngine;
using UnityEngine.UI;

public class CardOnBoard : MonoBehaviour
{

    public Card card;

    public Image img;

    public void ChangeCard(Card c){
        this.card = c;
        img.enabled = true;
    }

    public void ClearCard(){
        this.card = null;
        img.enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
