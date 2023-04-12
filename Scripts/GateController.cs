using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{
    private int numEnemies;

    private void Start()
    {
        // Tìm tất cả các object có tag là 'Enemy'
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Đếm số lượng object tìm thấy
        numEnemies = enemies.Length;
        Debug.Log(numEnemies);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        // Kiểm tra xem Player đã chạm vào cổng
        if (other.gameObject.CompareTag("Player"))
        {
            // Kiểm tra số lượng object có tag là 'Enemy'
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            numEnemies = enemies.Length;

            // Nếu số lượng object bằng 0, xử lí
            if (numEnemies == 0)
            {
                SceneManager.LoadScene("BeginLevel2");
            }
        }
    }
}
