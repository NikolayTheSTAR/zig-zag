using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed = 3;

    public static bool isRightDim = true; // истинно, когда шар катится вправо
    public static bool isLife; // истинно, если шар катится
    public static bool isDie = false; // истинно, если шар упал
    private int SumActivePlatform; // сумма платформ, которых касается игрок
    private float Step; // расстояние, на которое шар сдвигается за кадр

    void Start ()
    {
        isLife = false;
        SumActivePlatform = 1;
    }

    void Update()
    {
        if (isLife)
        {
            Step = Time.deltaTime * Speed;

            if (isRightDim)
            {
                // перемещение вправо
                transform.Translate(0, 0, Step);
            }
            else
            {
                // перемещение вверх
                transform.Translate(-Step, 0, 0);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (isLife & col.tag == "Platform")
        {
            SumActivePlatform++;
            Platform.CreateNextPlatform();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (isLife & col.tag == "Platform")
        {
            SumActivePlatform--;

            if (SumActivePlatform <= 0)
            {
                Die();
            }
        }
    }
    
    // поражение
    private void Die()
    {
        isLife = false;
        isDie = true;
        GetComponent<Animator>().SetTrigger("Loss");
    }

    // перезапуск игры
    private void Reload()
    {
        Reinit();
        Platform.Reinit();
        Score.Reinit();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // установка стартовых значений (для перезапуска)
    void Reinit()
    {
        isDie = false;
        isLife = false;
    }
}