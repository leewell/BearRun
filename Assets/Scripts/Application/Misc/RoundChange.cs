using UnityEngine;

public class RoundChange : MonoBehaviour
{
    private GameObject roadNow;
    private GameObject roadNext;
    private GameObject parent;

    private void Start()
    {
        if (parent == null)
        {
            parent = new GameObject();
            parent.transform.position = Vector3.zero;
            parent.name = "Road";
        }
        roadNow = Game.Instance.objectPool.Spawn("Pattern_1", parent.transform);
        roadNext = Game.Instance.objectPool.Spawn("Pattern_2", parent.transform);
        roadNext.transform.position += new Vector3(0, 0, 160);

        AddItem(roadNow);
        AddItem(roadNext);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Road)
        {
            Game.Instance.objectPool.UnSpawn(other.gameObject);
            //创建新的road跑道
            SpawnNewRoad();
        }
    }

    /// <summary>
    /// 创建新的随机跑道
    /// </summary>
    private void SpawnNewRoad()
    {
        int randNum = Random.Range(1, 5);
        roadNow = roadNext;
        roadNext = Game.Instance.objectPool.Spawn("Pattern_" + randNum, parent.transform);
        roadNext.transform.position = roadNow.transform.position + new Vector3(0, 0, 160);

        AddItem(roadNext);
    }

    /// <summary>
    /// 生成障碍物
    /// </summary>
    public void AddItem(GameObject obj)
    {
        var itemChild = obj.transform.Find("Item");
        if (itemChild != null)
        {
            var patternManager = PatternManager.Instance;
            if (patternManager != null && patternManager.patterns != null && patternManager.patterns.Count > 0)
            {
                //随机取一个方案
                var pattern = patternManager.patterns[Random.Range(0, patternManager.patterns.Count)];
                if (pattern != null && pattern.patternItems != null && pattern.patternItems.Count > 0)
                {
                    foreach (var itemList in pattern.patternItems)
                    {
                        GameObject go = Game.Instance.objectPool.Spawn(itemList.name, itemChild);
                        go.transform.parent = itemChild;
                        go.transform.localPosition = itemList.pos;
                    }
                }
            }
        }
    }
}