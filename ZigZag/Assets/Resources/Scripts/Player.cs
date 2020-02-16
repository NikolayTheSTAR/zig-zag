using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed = 3;

    public static bool isRightDim = true; // истинно, когда шар катится вправо
    public static bool isLife;
    public static bool isDie = false;
    private int SumActiveCube;
    private float Step; // расстояние, на которое шар сдвигается за кадр

    void Start ()
    {
        isLife = false;
        SumActiveCube = 1;
    }

    void Reinit()
    {
        isDie = false;
        isLife = false;
    }

    void Update()
    {
        if (isLife)
        {
            Step = Time.deltaTime * Speed;

            if (isRightDim)
            {
                transform.Translate(0, 0, Step);
            }
            else
            {
                transform.Translate(-Step, 0, 0);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (isLife & col.tag == "Platform")
        {
            SumActiveCube++;

            Platform.CreateNextPlatform();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (isLife & col.tag == "Platform")
        {
            SumActiveCube--;

            if (SumActiveCube <= 0)
            {
                Die();
            }
        }
    }

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
}