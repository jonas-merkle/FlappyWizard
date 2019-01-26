using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class CollisionTest
    {
        // tests the player collision with a red item
        [UnityTest]
        public IEnumerator RedItemCollisionTest()
        {
            
            // gen a new player
            var player =  new GameObject("player");
            player.AddComponent<Rigidbody2D>();
            player.AddComponent<BoxCollider2D>();
            player.AddComponent<CharacterCollisionHandler>();

            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().position = Vector2.zero;
            player.GetComponent<Rigidbody2D>().simulated = true;
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<BoxCollider2D>().size = new Vector2(10, 10);

            // gen a new game instance
            var game = new GameObject();
            game.AddComponent<GameControl>();

            GameControl.Instance.PausedScreen = new GameObject();
            GameControl.Instance.GameOverScreen = new GameObject();
            GameControl.Instance.GamePlayScreen = new GameObject();
            GameControl.Instance.ScoreText = new GameObject().AddComponent<Text>();
            GameControl.Instance.ItemText = new GameObject().AddComponent<Text>();
            GameControl.Instance.ScoreAtGameOverText = new GameObject().AddComponent<Text>();

            // gen new item 
            var item = new GameObject();
            item.tag = "Item";
            item.name = "item_red(Clone)";

            item.AddComponent<Rigidbody2D>();
            item.GetComponent<Rigidbody2D>().gravityScale = 0;
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            item.GetComponent<Rigidbody2D>().position = Vector2.zero;
            item.GetComponent<Rigidbody2D>().simulated = true;
            item.AddComponent<BoxCollider2D>();
            item.GetComponent<BoxCollider2D>().size = new Vector2(10, 10);

            yield return null;

            Assert.IsTrue(GameControl.Instance.Troll);
        }

        // tests the player collision with a blue item
        [UnityTest]
        public IEnumerator BlueItemCollisionTest()
        {

            // gen a new player
            var player = new GameObject("player");
            player.AddComponent<Rigidbody2D>();
            player.AddComponent<BoxCollider2D>();
            player.AddComponent<CharacterCollisionHandler>();

            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().position = Vector2.zero;
            player.GetComponent<Rigidbody2D>().simulated = true;
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<BoxCollider2D>().size = new Vector2(10, 10);

            // gen a new game instance
            var game = new GameObject();
            game.AddComponent<GameControl>();

            GameControl.Instance.PausedScreen = new GameObject();
            GameControl.Instance.GameOverScreen = new GameObject();
            GameControl.Instance.GamePlayScreen = new GameObject();
            GameControl.Instance.ScoreText = new GameObject().AddComponent<Text>();
            GameControl.Instance.ItemText = new GameObject().AddComponent<Text>();
            GameControl.Instance.ScoreAtGameOverText = new GameObject().AddComponent<Text>();

            // gen new item 
            var item = new GameObject();
            item.tag = "Item";
            item.name = "item_blue(Clone)";

            item.AddComponent<Rigidbody2D>();
            item.GetComponent<Rigidbody2D>().gravityScale = 0;
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            item.GetComponent<Rigidbody2D>().position = Vector2.zero;
            item.GetComponent<Rigidbody2D>().simulated = true;
            item.AddComponent<BoxCollider2D>();
            item.GetComponent<BoxCollider2D>().size = new Vector2(10, 10);

            yield return null;

            Assert.IsTrue(GameControl.Instance.Turbo);
        }

        // tests the player collision with a green item
        [UnityTest]
        public IEnumerator GreenItemCollisionTest()
        {

            // gen a new player
            var player = new GameObject("player");
            player.AddComponent<Rigidbody2D>();
            player.AddComponent<BoxCollider2D>();
            player.AddComponent<CharacterCollisionHandler>();

            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().position = Vector2.zero;
            player.GetComponent<Rigidbody2D>().simulated = true;
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<BoxCollider2D>().size = new Vector2(10, 10);

            // gen a new game instance
            var game = new GameObject();
            game.AddComponent<GameControl>();

            GameControl.Instance.PausedScreen = new GameObject();
            GameControl.Instance.GameOverScreen = new GameObject();
            GameControl.Instance.GamePlayScreen = new GameObject();
            GameControl.Instance.ScoreText = new GameObject().AddComponent<Text>();
            GameControl.Instance.ItemText = new GameObject().AddComponent<Text>();
            GameControl.Instance.ScoreAtGameOverText = new GameObject().AddComponent<Text>();

            // gen new item 
            var item = new GameObject();
            item.tag = "Item";
            item.name = "item_green(Clone)";

            item.AddComponent<Rigidbody2D>();
            item.GetComponent<Rigidbody2D>().gravityScale = 0;
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            item.GetComponent<Rigidbody2D>().position = Vector2.zero;
            item.GetComponent<Rigidbody2D>().simulated = true;
            item.AddComponent<BoxCollider2D>();
            item.GetComponent<BoxCollider2D>().size = new Vector2(10, 10);

            yield return null;

            Assert.IsTrue(GameControl.Instance.Invulnerability);
        }

        // tests the player collision with a silver item
        [UnityTest]
        public IEnumerator SilverItemCollisionTest()
        {

            // gen a new player
            var player = new GameObject("player");
            player.AddComponent<Rigidbody2D>();
            player.AddComponent<BoxCollider2D>();
            player.AddComponent<CharacterCollisionHandler>();

            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().position = Vector2.zero;
            player.GetComponent<Rigidbody2D>().simulated = true;
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<BoxCollider2D>().size = new Vector2(10, 10);

            // gen a new game instance
            var game = new GameObject();
            game.AddComponent<GameControl>();

            GameControl.Instance.PausedScreen = new GameObject();
            GameControl.Instance.GameOverScreen = new GameObject();
            GameControl.Instance.GamePlayScreen = new GameObject();
            GameControl.Instance.ScoreText = new GameObject().AddComponent<Text>();
            GameControl.Instance.ItemText = new GameObject().AddComponent<Text>();
            GameControl.Instance.ScoreAtGameOverText = new GameObject().AddComponent<Text>();

            // gen new item 
            var item = new GameObject();
            item.tag = "Item";
            item.name = "item_silver(Clone)";

            item.AddComponent<Rigidbody2D>();
            item.GetComponent<Rigidbody2D>().gravityScale = 0;
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            item.GetComponent<Rigidbody2D>().position = Vector2.zero;
            item.GetComponent<Rigidbody2D>().simulated = true;
            item.AddComponent<BoxCollider2D>();
            item.GetComponent<BoxCollider2D>().size = new Vector2(10, 10);

            yield return null;

            Assert.IsTrue(GameControl.Instance.DoublePoints);
        }

        // tests the player collision with an obstacle
        [UnityTest]
        public IEnumerator ObstacleCollisionTest()
        {

            // gen a new player
            var player = new GameObject("player");
            player.AddComponent<Rigidbody2D>();
            player.AddComponent<BoxCollider2D>();
            player.AddComponent<CharacterCollisionHandler>();

            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().position = Vector2.zero;
            player.GetComponent<Rigidbody2D>().simulated = true;
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<BoxCollider2D>().size = new Vector2(10, 10);

            // gen a new game instance
            var game = new GameObject();
            game.AddComponent<GameControl>();

            GameControl.Instance.PausedScreen = new GameObject();
            GameControl.Instance.GameOverScreen = new GameObject();
            GameControl.Instance.GamePlayScreen = new GameObject();
            GameControl.Instance.ScoreText = new GameObject().AddComponent<Text>();
            GameControl.Instance.ItemText = new GameObject().AddComponent<Text>();
            GameControl.Instance.ScoreAtGameOverText = new GameObject().AddComponent<Text>();

            // gen new item 
            var item = new GameObject();
            item.tag = "DeathZone";

            item.AddComponent<Rigidbody2D>();
            item.GetComponent<Rigidbody2D>().gravityScale = 0;
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            item.GetComponent<Rigidbody2D>().position = Vector2.zero;
            item.GetComponent<Rigidbody2D>().simulated = true;
            item.AddComponent<BoxCollider2D>();
            item.GetComponent<BoxCollider2D>().size = new Vector2(10, 10);

            yield return null;

            Assert.IsTrue(GameControl.Instance.GameOver);
        }
    }
}
