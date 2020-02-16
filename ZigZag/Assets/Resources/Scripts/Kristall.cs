using UnityEngine;

public class Kristall : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            anim.SetTrigger("Add");
            Score.AddScore();
        }
    }

    // удаление кристалла
    void Destroy()
    {
        Destroy(gameObject);
    }
}