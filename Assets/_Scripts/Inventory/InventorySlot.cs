using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    Item item;
    private PlayerController pc;
    public Image icon;
    public Button removeButton;
    Scene currentScene;
    private Text textPotionVie;
    [SerializeField] private int nbPotions = 3;
    [SerializeField] private PlayerHealth ph;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentScene = SceneManager.GetActiveScene();
        textPotionVie = GameObject.Find("nb").GetComponent<Text>();
        textPotionVie.text = GetNbPotions().ToString();
        ph = pc.GetComponent<PlayerHealth>();
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
       
        if (icon.sprite != null && icon.sprite.name == "potionVie" && currentScene.name != "MainGame(testing)" && nbPotions != 0 && ph.getCurrentHealth() < ph.getMaxVie())
        {
            pc.healthBonus();
            nbPotions--;
            textPotionVie.text = nbPotions.ToString();
        }
        else if (currentScene.name == "MainGame(testing)" || nbPotions == 0)
        {
            if (nbPotions == 0)
            {
                textPotionVie.text = nbPotions.ToString();
            }
            Debug.Log("Tu peux pas te soigner");
        }
    }
    
    public int GetNbPotions()
    {
        return nbPotions;
    }
}