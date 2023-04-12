using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginLevel : MonoBehaviour
{
    public float delayTime = 3f; // Thời gian trễ trước khi chuyển scene
    public string sceneName; // Tên của scene cần chuyển đến

    private float timeRemaining;

    void Start()
    {
        timeRemaining = delayTime;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
