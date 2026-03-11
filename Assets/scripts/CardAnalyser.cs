using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardAnalyser : MonoBehaviour
{
    public Card card;
    public GameObject md3;

    private Coroutine timerCoroutine;
    public Image img;
    public PoseManager poseManager;


    public float duration = 3f;
    void Start(){
        poseManager = FindFirstObjectByType<PoseManager>();
        if(md3 != null)
        md3.SetActive(false);
    }

    public void StartTimer()
    {
        if(!card.isPending)
            return;

        if(!card.active){
            StopTimer();
            img.sprite = card.image;
            img.fillAmount = 0f;
            timerCoroutine = StartCoroutine(CardTimer());
        }

    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }else{
            // md3.SetActive(false);
        }
        img.fillAmount = 0f;
        //img.gameObject.SetActive(false);
    }

    private IEnumerator CardTimer()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            img.fillAmount = elapsed / duration;

            yield return null;
        }

        img.fillAmount = 1f;
         card.active = true;

         timerCoroutine = null;
        yield return new WaitForSeconds(1f);
        if(card.type == TypeCard.PERSONNAGE || card.type == TypeCard.PNJ)
           md3.SetActive(true);
        else if(card.type == TypeCard.LIEU)
            poseManager.PoseLieuAR(card);
        yield return new WaitForSeconds(2f);

        poseManager.PoseCard();
        img.fillAmount = 0f;
    }
}