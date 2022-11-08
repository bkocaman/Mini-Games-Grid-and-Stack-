using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text scoreText;
    public TMP_InputField sizeInput;

    private int score = 0;

    public delegate void OnRebuildButtonClick(int count);
    public static event OnRebuildButtonClick onRebuildButtonClick;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int _addition)
    {
        score += _addition;
        scoreText.text = "Score: " + score;
    }

    public void onClickRebuildButton()
    {
        if (int.TryParse(sizeInput.text, out int size))
        {
            if (size > 1)
            {
                if (size > 50)
                    onRebuildButtonClick(50);
                else
                    onRebuildButtonClick(size);

                score = 0;
                scoreText.text = "Score: " + score;

            }
        }
        sizeInput.text = "";

    }
}

