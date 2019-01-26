using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class ObjectHandlerTests
    {
        [UnityTest]
        public IEnumerator OnScreenControlTest()
        {
            // gen a new game instance
            var game = new GameObject();
            game.AddComponent<CharacterCollisionHandler>();
            game.AddComponent<GameControl>();

            GameControl.Instance.PausedScreen = new GameObject();
            GameControl.Instance.GameOverScreen = new GameObject();
            GameControl.Instance.GamePlayScreen = new GameObject();
            GameControl.Instance.ScoreText = new GameObject().AddComponent<Text>();
            GameControl.Instance.ItemText = new GameObject().AddComponent<Text>();
            GameControl.Instance.ScoreAtGameOverText = new GameObject().AddComponent<Text>();

            game.AddComponent<GameObjectPoolHandler>();

            // setting up the game object pool handler
            game.GetComponent<GameObjectPoolHandler>().GameObjectPrefab = Resources.Load<GameObject>("Tests/TestObject");

            yield return null;

            var spawnPos =  new Vector2(10, 0);
            var spawnedObj = game.GetComponent<GameObjectPoolHandler>().SpawnNextObject(spawnPos);

            yield return null;

            Assert.AreEqual(spawnPos, spawnedObj.GetComponent<Rigidbody2D>().position);

            yield return null;

            spawnedObj.GetComponent<Rigidbody2D>().position = new Vector2(
                game.GetComponent<GameObjectPoolHandler>().EndOfScreenX * 2, 0);

            yield return new WaitForSeconds(1);

            Assert.AreEqual(game.GetComponent<GameObjectPoolHandler>().PoolPos, 
                spawnedObj.GetComponent<Rigidbody2D>().position);
        }
    }
}
