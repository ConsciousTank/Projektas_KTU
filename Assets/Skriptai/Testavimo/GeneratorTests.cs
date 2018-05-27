using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using System.Collections;

public class GeneratorTests {


	[UnityTest]
	public IEnumerator Adds_Room() {
        int x = 0;
        int y = 0;
        int rotation = 0;
        int id = 0;
        var generator = new GameObject().AddComponent<Generator>();
        generator.SetStartingEndingPoints(1, 1, 1, 1);
        yield return null;
        Generator.AddRoom(x, y, rotation, id);
        Assert.AreEqual(false, Generator.isEmpty(x, y));
        Assert.AreEqual(rotation, Generator.checkRoomRotation(x,y));
        Assert.AreEqual(id, Generator.checkRoomId(x, y));
        
    }

    [UnityTest]
    public IEnumerator Checks_If_Room_Is_Empty()
    {
        int x = 0;
        int y = 0;
        var generator = new GameObject().AddComponent<Generator>();
        generator.SetStartingEndingPoints(1, 1, 1, 1);
        yield return null;
        Generator.RemoveRoom(x, y);
        Assert.AreEqual(true, Generator.isEmpty(x, y));
    }

    [UnityTest]
    public IEnumerator Checks_If_Can_Add_In_Player_Room()
    {
        int x = 1;
        int y = 1;
        int rotation = 0;
        int id = 0;
        var generator = new GameObject().AddComponent<Generator>();
        generator.SetStartingEndingPoints(1, 1, 1, 1);
        yield return null;
        var add = Generator.AddRoom(x, y, rotation, id);
        Assert.AreEqual(false, add);
    }

    [TearDown]
    public void AfterEveryTest()
    {
        var generator = new GameObject().AddComponent<Generator>();
        Generator.ClearRooms();
    }
}
