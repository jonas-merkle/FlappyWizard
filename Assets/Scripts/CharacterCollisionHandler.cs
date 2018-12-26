using System;
using UnityEngine;

public class CharacterCollisionHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        CollisionEventArgs args = new CollisionEventArgs();
        args.Collider = col;
        args.TimeOfCollision = Time.time;
        args.CollisionObjectTag = col.gameObject.tag;
        OnCollisionDetected(args);

        if (col.gameObject.tag == "DeathZone")
        {
            Time.timeScale = 0.0f;
            GameController.Instance.GameOver = true;
            GameController.Instance.GameOverScreen.SetActive(true);

            // calculate high score
            int reachedScore = Convert.ToInt32(GameController.Instance.ScoreText.text);
            int currentHighScore = PlayerPrefs.GetInt("HighScore");
            if (reachedScore > currentHighScore)
            {
                PlayerPrefs.SetInt("HighScore", currentHighScore);
                GameController.Instance.NewHighScore = true;
            }

        }
        else if (col.gameObject.tag == "Item")
        {
            //Debug.Log("Item: " + col.name.Replace("(Clone)", ""));

            string itemType = col.name.Replace("(Clone)", "");

            if ("item_red".Equals(itemType))
            {
                GameController.Instance.Troll = true;
                GameController.Instance.TimeOfEffectStart = Time.time;
            }
            else if ("item_green".Equals(itemType))
            {
                GameController.Instance.Invulnerability = true;
                GameController.Instance.TimeOfEffectStart = Time.time;
            }
            else if ("item_blue".Equals(itemType))
            {
                GameController.Instance.Turbo = true;
                GameController.Instance.TimeOfEffectStart = Time.time;
            }
            else if ("item_silver".Equals(itemType))
            {
                GameController.Instance.DoublePoints = true;
                GameController.Instance.TimeOfEffectStart = Time.time;
            }

            col.gameObject.GetComponent<Rigidbody2D>().position = new Vector2(-1000, 0);
        }
    }

    protected virtual void OnCollisionDetected(CollisionEventArgs e)
    {
        CollisionDetected?.Invoke(this, e);
    }

    public event EventHandler<CollisionEventArgs> CollisionDetected;
}

public class CollisionEventArgs : EventArgs
{
    public Collider2D Collider { get; set; }
    public float TimeOfCollision { get; set; }
    public string CollisionObjectTag { get; set; }
}
