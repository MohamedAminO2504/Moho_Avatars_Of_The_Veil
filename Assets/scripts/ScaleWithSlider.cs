using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TransformWithSliders : MonoBehaviour
{
    public Slider scaleSlider;
    public Slider rotationSlider;

    public List<Transform> targets;

    void Start()
    {
        scaleSlider.onValueChanged.AddListener(ChangeScale);
        rotationSlider.onValueChanged.AddListener(ChangeRotation);

        ChangeScale(scaleSlider.value);
        ChangeRotation(rotationSlider.value);
    }

    void ChangeScale(float value)
    {
        foreach (Transform target in targets)
        {
            target.localScale = new Vector3(value, value, value);
        }
    }

    void ChangeRotation(float value)
    {
        foreach (Transform target in targets)
        {
            target.localRotation = Quaternion.Euler(0, value, 0);
        }
    }
}