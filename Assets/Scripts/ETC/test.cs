using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ConsoleLogger.Log(DBManager.Instance.GetItemData(1000).Name);
        GameObject obj = UIManager.Instance.ShowPopup<InventoryPopup>().gameObject;
        Inventory inventory = obj.GetComponent<Inventory>();
        inventory.GetItem(1000);
    }
}
