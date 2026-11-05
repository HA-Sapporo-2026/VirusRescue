using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCollision : MonoBehaviour
{

    // 要救助者のレイヤーをここに入れてください！
    //public LayerMask HumanNeedsHelp;

    //public TagHandle tagHandle;
    public PlayerStatus status;
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Human human = collision.gameObject.GetComponent<Human>();
        if (collision.gameObject.tag == "Human")
        {
            //if (human != null)
            //{
            //    if (human.thereIsWall)
            //        return;
            //}
            //Debug.Log("Human");
            if (human != null)
            {
                //if (human.isHere)
                status.Escort(collision);
            }
        }
        else if (collision.gameObject.tag == "Tent")
        {
            //Debug.Log("Tent");
            status.Escape(collision);
        }
        else if (collision.gameObject.tag == "Heli")
        {
            //Debug.Log("Heli");
            status.Clear = true;
            //status.Escape(collision);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("Probably Enemy");
            status.Damage(collision);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer != HumanNeedsHelp)
    //    {
    //    }
    //}
}
