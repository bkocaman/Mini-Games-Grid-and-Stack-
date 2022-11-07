using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public SpriteRenderer gridSprite;
    public SpriteRenderer xSprite;

    [HideInInspector]
    public bool isFull;
    private bool isTransitioning = false;

    private int posX;
    private int posY;
    private Vector3 initXScale;

    private void Start()
    {
        initXScale = xSprite.transform.localScale;
    }

    public void FillCell()
    {
        if (isFull || isTransitioning)
            return;


        isFull = true;

        xSprite.transform.DOKill();

        xSprite.transform.localScale = Vector3.zero;
        xSprite.gameObject.SetActive(true);

        xSprite.transform.DOScale(initXScale, 0.2f).SetEase(Ease.OutBack);
    }

    public void ResetGrid()
    {
        StartCoroutine(DelayResetGrid(0.2f));
    }

    private IEnumerator DelayResetGrid(float delay)
    {
        isFull = false;
        isTransitioning = true;

        yield return new WaitForSecondsRealtime(delay);

        xSprite.transform.DOKill();
        xSprite.transform.DOScale(0, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
        {
            xSprite.gameObject.SetActive(false);
            isTransitioning = false;
        });
    }

    public void SetPosition(int x, int y)
    {
        posX = x;
        posY = y;
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int(posX, posY);
    }
}
