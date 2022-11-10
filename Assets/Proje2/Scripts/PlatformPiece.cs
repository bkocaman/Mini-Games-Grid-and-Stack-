using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPiece : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject leftPos;
    [SerializeField] private GameObject rightPos;
    [SerializeField] private GameObject placedEffect;

    public void SetColor(Color color)
    {
        model.GetComponent<Renderer>().material.color = color;
        var main = placedEffect.GetComponent<ParticleSystem>().main;
        main.startColor = color;
    }

    public void PiecePlaced()
    {
        placedEffect.SetActive(true);
        StartCoroutine(CloseEffectWithDelay(1));
    }

    private IEnumerator CloseEffectWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        placedEffect.SetActive(false);
    }

    public Color GetColor()
    {
        return model.GetComponent<Renderer>().material.color;
    }

    public GameObject GetModel()
    {
        return model;
    }

    public Vector3 GetLeftPos()
    {
        return leftPos.transform.position;
    }

    public Vector3 GetRigtPos()
    {
        return rightPos.transform.position;
    }
}
