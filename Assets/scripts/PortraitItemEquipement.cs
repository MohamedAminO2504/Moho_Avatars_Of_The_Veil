using UnityEngine;
using UnityEngine.UI;

public class PortraitItemEquipement : MonoBehaviour
{
    public Card card;
    public Image image;
    public MenuEquipementManager manager;

    public void Init(Card c){
        this.card = c;
        this.image.sprite = c.portrait.head_talk;
    }

    public void ChangePerso(){
        manager.ChangePerso(card);
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
