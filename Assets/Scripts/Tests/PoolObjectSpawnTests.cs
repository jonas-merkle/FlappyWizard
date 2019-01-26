using NUnit.Framework;
using UnityEngine;
using UnityEngine.WSA;

namespace Tests
{
    public class PoolObjectSpawnTests
    {
        [Test]
        public void SpawnDummyObjectTest()
        {
            Vector2 testPos = new Vector2(5.7f, 2.2f);
            PoolObject obj = new PoolObject(Resources.Load<GameObject>("Tests/TestObject"), testPos);

            Assert.AreEqual(testPos, obj.GameObject.GetComponent<Rigidbody2D>().position);
        }

        [Test]
        public void SpawnDummyObjectPoolTest()
        {
            int size = 3333;
            Vector2 poolPos = new Vector2(23, 4.7f);
            Pool objPool = new Pool(Resources.Load<GameObject>("Tests/TestObject"), size, poolPos);

            Assert.AreEqual(size, objPool.PoolObjects.Length);

            foreach (var obj in objPool.PoolObjects)
            {
                Assert.AreEqual(poolPos, obj.GameObject.GetComponent<Rigidbody2D>().position);
            }
        }
    }
}
