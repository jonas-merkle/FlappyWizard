using UnityEngine;

public class CharacterCollisionHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DeathZone")
        {
            Time.timeScale = 0.0f;
            GameController.Instance.GameOver = true;
            GameController.Instance.GameOverScreen.SetActive(true);
        }
        else if (col.gameObject.tag == "Item")
        {
            //Debug.Log("Item: " + col.name.Replace("(Clone)", ""));

            string itemType = col.name.Replace("(Clone)", "");

            if ("item_red".Equals(itemType))
            {

            }
            else if ("item_green".Equals(itemType))
            {

            }
            else if ("item_blue".Equals(itemType))
            {

            }
            else if ("item_silver".Equals(itemType))
            {

            }
        }
    }
}
