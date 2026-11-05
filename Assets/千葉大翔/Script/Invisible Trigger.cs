using UnityEngine;
using UnityEngine.Tilemaps;

public class InvisibleTrigger : MonoBehaviour
{
    public TilemapRenderer renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<TilemapRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.enabled = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Human human = collision.gameObject.GetComponent<Human>();
        //if (human != null)
        //{
        //    //human.humanManager.PositionSearch(human.Num);
        //}
    }
}
