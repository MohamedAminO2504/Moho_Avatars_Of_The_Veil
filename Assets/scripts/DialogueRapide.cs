using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueRapide : MonoBehaviour
{
    public Image portrait;
    public TextMeshProUGUI text;

    public void Play(string t, Sprite i){
        text.text = t;
        portrait.sprite = i;
        Destroy(gameObject, 5f);

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
