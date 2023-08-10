using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="Bag", menuName = "Items/Bag",order =1)]
public class Bag : Item ,IUseable
{
    [SerializeField]
    private GameObject _bagPrefab;

    public BagScript MyBagScrtipt { get; set; }
    public BagButton MyBagButton { get; set; }

    private int _slots;
    public int Slots => _slots;

    public void Initialize(int slots)
    {
        _slots = slots;
    }

    public void SetupScript()
    {
        MyBagScrtipt = Instantiate(_bagPrefab, InventoryScript.Instance.transform).GetComponent<BagScript>();
        MyBagScrtipt.AddSlots(_slots);
    }

    public void Use()
    {
        if (this.Title == "Кот в мешке(не открывать)")
        {
            SoundManager.Instance.MusicSource.Stop();
            SoundManager.Instance.PlaySound(Player.MyInstance.CatMeow);
            SceneManager.LoadScene("LastScene"); }
        if (InventoryScript.Instance.CanAddBag)
        {
            Remove();
            MyBagScrtipt = Instantiate(_bagPrefab, InventoryScript.Instance.transform).GetComponent<BagScript>();
            MyBagScrtipt.AddSlots(_slots);

            if(MyBagButton == null)
            {
                InventoryScript.Instance.AddBag(this);
            }
            else
            {
                InventoryScript.Instance.AddBag(this, MyBagButton);
            }
        }
    }
}