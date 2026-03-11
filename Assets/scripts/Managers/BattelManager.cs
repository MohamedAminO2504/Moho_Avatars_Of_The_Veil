using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattelManager : MonoBehaviour
{
    [Header("Data")]
    public string nom;
    public Card battleCard;

    [Header("References")]
    public ScenarioManager scenarioManager;
    public TurnManager turnManager;
    public DonjonManager donjonManager;
    public Board board;
    public GameObject orderZone;
    public List<BattelOrderUnit> uiOrders;

    [Header("Runtime")]
    public List<Caractere> orders = new();
    public List<Caractere> persos = new();
    public Caractere currentTour;

    public BattleZone battleZone;
    private Step step;

    void Start()
    {
        turnManager.battelManager = this;
    }

    // =========================
    // START BATTLE
    // =========================

    public void Play(Step step)
    {
        this.step = step;
        if(step.battleType == BattleType.RANDOM){
            int roll = Random.Range(0, 100);
            if(roll > 40){
                scenarioManager.PlayNextStep();
                return;
            }
        }

        StartCoroutine(StartBattle());
    }

    private IEnumerator StartBattle()
    {
        orderZone.SetActive(true);

        if (scenarioManager.isInDonjon)
        {
            donjonManager.InitBattelDonjon();
            yield return new WaitForSeconds(1f);
        }

        InitBattle();

    }


    private void InitBattle()
    {
        if(scenarioManager.isInDonjon)
            donjonManager.HideChoix();
        orders.Clear();
        persos.Clear();


        battleZone.gameObject.SetActive(true);



        InitEnemies();
        InitHeroes();

        CalculateOrder();
    }

    // =========================
    // SPAWN
    // =========================

    private void InitEnemies()
    {
        int enemyPosition = 0;

        BattleParam battleParam = step.GetAleatoireBattle();
        foreach (Card c in battleParam.bestiaire)
        {
            c.cara.hp = c.cara.hpMax;

            Caractere caractere =
                Instantiate(c.m3d)
                .GetComponent<Caractere>();
            caractere.gameObject.transform.SetParent( battleZone.enemyPositions[enemyPosition]);

            caractere.transform.localPosition = Vector3.zero;
            caractere.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            caractere.transform.localScale *= 30f;

            caractere.cara = c.cara;
            caractere.EnterCombat();

            orders.Add(caractere);
            persos.Add(caractere);

            enemyPosition++;
        }
    }

    private void InitHeroes()
    {
        int persoPosition = 0;

        foreach (CardDisplay h in board.cardOnBoard)
        {
            if (h.card == null) continue;

            if (h.card.type != TypeCard.PERSONNAGE &&
                h.card.type != TypeCard.BESTIAIRE)
                continue;

            var caractere = h.caractere;

            orders.Add(caractere);
            persos.Add(caractere);

            caractere.cara = caractere.card.cara;

            if (h.card.type == TypeCard.PERSONNAGE)
            {
                Transform targetPos = battleZone.persoPositions[persoPosition];
                caractere.battleManager = this;
                caractere.RunToAndCallBack(targetPos, () =>
                {
                    caractere.Look(battleZone.enemyPositions[0], true, false);
                    caractere.EnterCombat();
                    caractere.transform.SetParent(battleZone.persoPositions[persoPosition]);
                    StartCoroutine(TryToStartTurn());
                });

                persoPosition++;
            }
        }
    }

    // =========================
    // TURN SYSTEM
    // =========================

    public void NextTour()
    {
        RemoveDead();

        if (orders.Count == 0)
        {
            CheckEndBattle();
            return;
        }

        RefreshUI();

        currentTour = orders[0];
        turnManager.StartTurn(currentTour.card, currentTour);
    }

    public void EndTour()
    {
        if (orders.Count > 0)
            orders.RemoveAt(0);

        NextTour();
    }

    public void RemoveDead()
    {
        orders.RemoveAll(x => x.card == null || x.card.cara.hp <= 0);
        persos.RemoveAll(x => x.card == null || x.card.cara.hp <= 0);
    }

    // =========================
    // ORDER CALCULATION (ATB style)
    // =========================

    public void CalculateOrder()
    {
        var timeline = orders
            .Select(cd => new TimelineEntry
            {
                display = cd,
                nextActionTime = 0f
            })
            .ToList();

        List<Caractere> result = new();

        for (int i = 0; i < 100; i++)
        {
            timeline.Sort((a, b) =>
                a.nextActionTime.CompareTo(b.nextActionTime));

            var current = timeline[0];
            result.Add(current.display);

            float speed = Mathf.Max(1, current.display.card.cara.vit);
            current.nextActionTime += 100f / speed;
        }

        orders = result;
        RefreshUI();
    }

    // =========================
    // END CONDITIONS
    // =========================

    private void CheckEndBattle()
    {
        if (AllDead(Heros()))
            Perdu();
        else if (AllDead(Bestiaires()))
            StartCoroutine(Gagner());
    }

    public bool AllDead(List<Caractere> list)
    {
        return list.All(cd => cd.card == null || cd.card.cara.hp <= 0);
    }


    public IEnumerator Gagner()
    {
        Debug.Log("Gagner");

        orderZone.SetActive(false);
        turnManager.zone.SetActive(false);
        battleZone.Down();
        yield return new WaitForSeconds(3f);
         if (scenarioManager.isInDonjon){
            Debug.Log("end battle donjon");
            donjonManager.ExitBattle();
            scenarioManager.PlayNextStep();
            yield break;
         }


        foreach (var hero in Heros()){
           StartCoroutine(hero.ExitCombat());
        }


        yield return new WaitForSeconds(1f);

    }

      public IEnumerator TryToStartTurn(){
            foreach (var hero in Heros()){
               if (hero.isRunning)
                yield break;
            }
            yield return new WaitForSeconds(1f);
           battleZone.Eleve();
           yield return new WaitForSeconds(2f);

           NextTour();

        }

    public IEnumerator TryToEndBattle(){
        foreach (var hero in Heros()){
           if (hero.isRunning)
            yield break ;
        }
        board.RemoveAllLook();
        yield return new WaitForSeconds(2f);
        foreach (var hero in Heros()){
           hero.ResetLook();

        }
        battleZone.gameObject.SetActive(false);
        Debug.Log("next step lala ");
        scenarioManager.PlayNextStep();

    }

    public void Perdu()
    {
        Debug.Log("Perdu");
    }

    // =========================
    // HELPERS
    // =========================

    public List<Caractere> Heros()
    {
        return persos
            .Where(p => p.card != null &&
                        p.card.type == TypeCard.PERSONNAGE)
            .ToList();
    }

    public List<Caractere> Bestiaires()
    {
        return persos
            .Where(p => p.card != null &&
                        p.card.type == TypeCard.BESTIAIRE)
            .ToList();
    }

    private void RefreshUI()
    {
        for (int i = 0; i < uiOrders.Count; i++)
        {
            if (i < orders.Count)
                uiOrders[i].Affiche(orders[i]);
            else
                uiOrders[i].Deactivation();
        }
    }

    private class TimelineEntry
    {
        public Caractere display;
        public float nextActionTime;
    }
}