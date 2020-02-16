using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private GameObject PlatformObj;
    [SerializeField] private GameObject KristallObj;

    private static List<Platform> Platforms = new List<Platform>(); // лист всех платформ
    private const int MaxStartInstCount = 17; // количество платформ в стартовой генерации
    private static int instCounter = 0; // счётчик для стартовой генерации
    private const int TailLength = 1; // длинна хвоста
    private static int TailCounter = 0; // счётчик хвоста
    
    private static GameObject LastPlatform; // последняя платформа
    private Animator anim;

    // установка стартовых значений
    public static void Reinit()
    {
        instCounter = 0;
        TailCounter = 0;
        Platforms = new List<Platform>();
    }

    void Start()
    {
        Platforms.Add(this);
        LastPlatform = gameObject;
        name = "Platform";
        anim = GetComponent<Animator>();

        if (instCounter != 0) CreateKristall();

        if (instCounter < MaxStartInstCount)
        {
            CreatePlatform(gameObject);

            instCounter++;
        }
    }

    // создание следующей платформы
    public static void CreateNextPlatform()
    {
        CreatePlatform(LastPlatform);

        if (TailCounter < TailLength)
        {
            TailCounter++;
        }
        else
        {
            Platforms[0].anim.SetTrigger("Fall");
            Platforms.RemoveAt(0);
        }
    }

    // создание платформы
    private static void CreatePlatform(GameObject BasedPlatform)
    {
        Vector3 CreatePos;
        float r;

        // получение позиции создания платформы

        r = Random.Range(0, 2);

        if (r == 0)
        {
            CreatePos = new Vector3(BasedPlatform.transform.position.x - 1, BasedPlatform.transform.position.y, BasedPlatform.transform.position.z);
        }
        else
        {
            CreatePos = new Vector3(BasedPlatform.transform.position.x, BasedPlatform.transform.position.y, BasedPlatform.transform.position.z + 1);
        }

        // если это первая созданная платформа, то игроку заносится направление
        if(instCounter == 0) Player.isRightDim = r == 1;

        // создание платформы

        Instantiate(BasedPlatform.GetComponent<Platform>().PlatformObj, CreatePos, Quaternion.identity);
    }

    // создание кристалла
    private void CreateKristall()
    {
        Vector3 CreatePos;
        float r;

        r = Random.Range(0f, 1f);

        if (r <= 0.2f)
        {
            CreatePos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            Instantiate(GetComponent<Platform>().KristallObj, CreatePos, Quaternion.identity);
        }
    }

    // удаление платформы
    void Destroy()
    {
        Destroy(gameObject);
    }
}