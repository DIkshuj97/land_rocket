using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{
    [SerializeField] GameObject loseLabel;
    [SerializeField] GameObject winlabel;
    [SerializeField] float waitToLoad = 4f;

    // Start is called before the first frame update
    void Start()
    {
        loseLabel.SetActive(false);
        winlabel.SetActive(false);
    }
    public void WinCondition()
    {
        StartCoroutine(HandleWinCondition());
    }

    IEnumerator HandleWinCondition()
    {
        winlabel.SetActive(true);
        yield return new WaitForSeconds(waitToLoad);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
    }
}
