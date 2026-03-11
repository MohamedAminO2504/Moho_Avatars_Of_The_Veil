using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MenuEquipementManager : MonoBehaviour
{
    public Card currentPersonnage;

    public Transform spawnM3D;
    public GameObject m3d;

    public TextMeshProUGUI nom;
    public Image portrait;

    [Header("Caracteristique")]
    public TextMeshProUGUI hpValue;
    public TextMeshProUGUI atkValue;
    public TextMeshProUGUI defValue;
    public TextMeshProUGUI vitValue;
    public TextMeshProUGUI magValue;
    public TextMeshProUGUI espValue;

    [Header("Current Equipement")]
    public Image currentTenue;

    [Header("Choix Item")]
    public Transform contentScrollView;
    public List<GameObject> items;

    [Header("Portrait Equipe")]
    public PortraitItemEquipement equipeMain;
    public List<PortraitItemEquipement> equipes;


    public TenuesItemUI tenuePrefab;
    public Data data;



    public void Init(Card c){
        Destroy(m3d);
        foreach(GameObject o in items){
            Destroy(o);
        }
        currentPersonnage = c;
        nom.text = c.nom;
        portrait.sprite = c.currentTenue.portrait;
        currentTenue.sprite = c.currentTenue.preview;
        hpValue.text = c.cara.hp+"";
        atkValue.text = c.cara.atk+"";
        defValue.text = c.cara.def+"";
        vitValue.text = c.cara.vit+"";
        magValue.text = c.cara.mag+"";
        espValue.text = c.cara.esp+"";
        m3d = Instantiate(c.currentTenue.m3d, spawnM3D);
        m3d.transform.localPosition = Vector3.zero;
        foreach(Tenue t in c.tenues){
            if(data.tenuesFound.Contains(t)){
                TenuesItemUI item = Instantiate(tenuePrefab, contentScrollView);
                        item.Init(t);
                        item.manager = this;
                        items.Add( item.gameObject);
            }
        }
        InitEquipes();
    }

    public void ChangePerso(Card c){
        if(c == currentPersonnage)
            return;
        Init(c);
    }

    public void InitEquipes(){
        int i = 0;
        foreach(PortraitItemEquipement p in equipes){
            p.gameObject.SetActive(false);
        }
        foreach(Card hero in data.equipes){
            if(hero != currentPersonnage){
                equipes[i].manager = this;
                equipes[i].gameObject.SetActive(true);
                equipes[i].Init(hero);
                i++;
            }
        }
        equipeMain.Init(currentPersonnage);
    }

    public void ChangeTenue(Tenue tenue){
        currentPersonnage.currentTenue = tenue;
        Init(currentPersonnage);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init(currentPersonnage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
