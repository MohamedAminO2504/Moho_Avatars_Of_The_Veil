using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Step step;
    public GameObject dialogueZone;
    public Image portrait;
    public TextMeshProUGUI text;
    public string[] lines;
    public int count;
    public ScenarioManager scenarioManager;
    public DialogueRapide dialogueRapidePrefab;
    public Transform contentScroll;

    public void PlayDialogueRapide(Step s){

        DialogueRapide  g = Instantiate(dialogueRapidePrefab, contentScroll);
        Debug.Log(g);
        g.Play(s.dialogue,s.actor.portrait.head_talk);
        Step nextStep = scenarioManager.GetNextStep();

        if(nextStep.type == StepType.DIALOGUE && nextStep.state == StepState.START){
            StartCoroutine(scenarioManager.WaitAndPlayNextStep(2f));
        }
        else
            scenarioManager.PlayNextStep();

    }


    public void Play(Step step){
       if(step.state == StepState.START){
            PlayDialogueRapide(step);
       }
       if(step.state == StepState.END){
       }
       if(step.state == StepState.WAIT){
            PlayDialogue(step);
       }
    }
    public void PlayDialogue(Step s){
        dialogueZone.gameObject.SetActive(true);
        step = s;
        lines = step.dialogue.Split('\n');
        count = 0;
        portrait.sprite = step.actor.portrait.head_talk;
        ShowLine();
    }

    public void ShowLine(){
        if(count >= lines.Length){
            EndDialogue();
            return;
        }
        text.text = lines[count];
        count++;
    }

    public void EndDialogue(){
        dialogueZone.gameObject.SetActive(false);
        scenarioManager.PlayNextStep();
    }

}
