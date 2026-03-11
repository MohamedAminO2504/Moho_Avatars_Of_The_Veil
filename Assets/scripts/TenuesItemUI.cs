using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TenuesItemUI : MonoBehaviour
{
    public Tenue tenue;
    public Image image;
    public TextMeshProUGUI nom;
    public TextMeshProUGUI cara1;
    public TextMeshProUGUI cara2;
    public TextMeshProUGUI competence;
    public MenuEquipementManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init(tenue);
    }

    public void Init(Tenue tenue){
        this.tenue = tenue;
        image.sprite = tenue.preview;
        nom.text = tenue.nom;
        competence.text = tenue.competence.name;
        TextMeshProUGUI toModif = cara1;
        if(tenue.caracteristique.hpMax != 0){
            toModif.text = "HP+"+tenue.caracteristique.hpMax;
            toModif = cara2;
        }
        if(tenue.caracteristique.atk != 0){
            toModif.text = "ATK+"+tenue.caracteristique.atk;
            toModif = cara2;
        }
        if(tenue.caracteristique.def != 0){
            toModif.text = "DEF+"+tenue.caracteristique.def;
            toModif = cara2;
        }
        if(tenue.caracteristique.vit != 0){
            toModif.text = "VIT+"+tenue.caracteristique.vit;
            toModif = cara2;
        }
    }

    public void ChangeTenu(){
        manager.ChangeTenue(tenue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
