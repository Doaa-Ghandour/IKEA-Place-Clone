using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DataHandler : MonoBehaviour
{
    private GameObject furniture;

    [SerializeField] private ButtonManager buttonPrefab;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private List<Item> items;
    [SerializeField] private string label;

    private int currentId = 0;

    private static DataHandler instance;

    public static DataHandler Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataHandler>();
            }
            return instance;
        }
    }

    private async void Start()
    {
        items = new List<Item>();
        await Get(label);
        //LoadItems();
        CreateButtons();
    }

    /*private void LoadItems()
    {
        var itemsObj = Resources.LoadAll("Items", typeof(Item));
        foreach (var item in itemsObj)
        {
            items.Add(item as Item);
        }
    }*/

    private void CreateButtons()
    {
        foreach (Item i in items)
        {
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            b.ItemId = currentId;
            b.ButtonTexture = i.itemImage;
            currentId++;
        }
    }

    public void SetFurniture(int id)
    {
        furniture = items[id].itemPrefab;
    }

    public GameObject GetFurniture()
    {
        return furniture;
    }

    public async Task Get(string label)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
        foreach (var location in locations)
        {
            var obj = await Addressables.LoadAssetAsync<Item>(location).Task;
            items.Add(obj);
        }
    }

}
