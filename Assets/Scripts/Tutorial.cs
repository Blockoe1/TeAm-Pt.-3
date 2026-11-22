using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject _overallParent;
    [SerializeField] private Transform _contentParent;
    [SerializeField] private GameObject _firstTutorial;

    void Start()
    {
        _overallParent.SetActive(false);
        if (PlayerManager.Instance.Round == 1)
        {
            StartTutorial(_firstTutorial);
        }
    }

    public void StartTutorial(GameObject contentPrefab)
    {
        _overallParent.SetActive(true);
        Instantiate(contentPrefab, _contentParent);
        GetComponentInChildren<Button>().onClick.AddListener(EndTutorial);
    }

    public void EndTutorial()
    {
        _overallParent.SetActive(false);
        Destroy(_contentParent.GetChild(0).gameObject);
    }
}
