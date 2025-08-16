using PokeDatas;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCreamonManager : MonoBehaviour
{
    [SerializeField] public GameData.AllieIndividual allieBox = new GameData.AllieIndividual();
    [SerializeField] private Image spriteCreamon;
    [SerializeField] private TextMeshProUGUI txt_name;
    [SerializeField] private TextMeshProUGUI txt_nature;
    [SerializeField] private TextMeshProUGUI txt_xp;
    [SerializeField] private GameObject evolveButton;
    [SerializeField] private ParticleSystem particuleShiny;
    [SerializeField] private ParticleSystem particuleEvolve;
    [SerializeField] private Image maxed;

    [SerializeField] public int indexInList;
    [SerializeField] private GameObject confirmRelease;
    [SerializeField] private GameObject shinyStars;
    [SerializeField] private GameObject selectedStarCreamon;
    [SerializeField] private GameObject unselectedStarCreamon;

    [SerializeField] private Image rarity;
    [SerializeField] private Sprite[] rarityTab;

    private void Start()
    {
        spriteCreamon.gameObject.SetActive(false);
        rarity.gameObject.SetActive(false);
    }

    public void SetDatasOnScreen()
    {
        if (allieBox != null)
        {
            if (!allieBox.isShiny && allieBox.sprite != null)
            {
                spriteCreamon.sprite = allieBox.sprite;
            }
            else if (allieBox.spriteShiny != null)
            {
                spriteCreamon.sprite = allieBox.spriteShiny;
            }
            spriteCreamon.gameObject.SetActive(true);

            txt_name.text = allieBox.name.ToUpper();
            txt_nature.text = allieBox.stats.natures.ToString().ToUpper();

            shinyStars.SetActive(allieBox.isShiny);

            switch (allieBox.rarity)
            {
                case 1:
                    rarity.sprite = rarityTab[0];
                    break;
                case 2:
                    rarity.sprite = rarityTab[1];
                    break;
                case 3:
                    rarity.sprite = rarityTab[2];
                    break;
                case 4:
                    rarity.sprite = rarityTab[3];
                    break;
                default:
                    break;
            }
            rarity.gameObject.SetActive(true);

            if(allieBox.exp < 100)
            {
                txt_xp.text = allieBox.exp.ToString() + "/100";
                evolveButton.gameObject.SetActive(false);
                maxed.gameObject.SetActive(false);
            }
            else
            {
                if(allieBox.canEvolve)
                {
                    txt_xp.text = null;
                    evolveButton.gameObject.SetActive(true);
                    maxed.gameObject.SetActive(false);
                }
                else
                {
                    txt_xp.text = null;
                    evolveButton.gameObject.SetActive(false);
                    maxed.gameObject.SetActive(true);
                }
            }

            if(GameData.Instance.mainSelectedAllie == indexInList)
            {
                selectedStarCreamon.SetActive(true);
                unselectedStarCreamon.SetActive(false);
            }
            else
            {
                selectedStarCreamon.SetActive(false);
                unselectedStarCreamon.SetActive(true);
            }
        }
        else
        {
            spriteCreamon.gameObject.SetActive(false);
            spriteCreamon.sprite = null;
            shinyStars.gameObject.SetActive(false);
            txt_name.text = null;
            txt_nature.text = null;
            txt_xp.text = null;
            evolveButton.gameObject.SetActive(false);
            maxed.gameObject.SetActive(false);
            rarity.gameObject.SetActive(false);
            selectedStarCreamon.SetActive(false);
            unselectedStarCreamon.SetActive(false);
            rarity.sprite = null;
            Debug.Log("No Datas To Set");
        }
    }

    public void ShowRelease()
    {
        if (allieBox != null && GameData.Instance.mainSelectedAllie != indexInList)
        {
            confirmRelease.SetActive(true);
        }
    }

    public void HideRelease()
    {
        confirmRelease.SetActive(false);
    }

    public void Release()
    {
        if (allieBox != null)
        {
            GameData.Instance.PlayerBox.RemoveAt(indexInList);
            allieBox = null;
            SetDatasOnScreen();
            confirmRelease.SetActive(false);
        }
        else
        {
            Debug.Log("NothingSelected");
        }
    }

    public void Evolve()
    {
        if (allieBox != null && allieBox.canEvolve)
        {
            Debug.Log(allieBox.idEvolved);
            GameData.AllieIndividual newCreamonCaught = new GameData.AllieIndividual(GameData.Instance.Allies.Where(creamon => creamon.id == allieBox.idEvolved).Select(creamon => creamon).ToList()[0]);            
            newCreamonCaught.isShiny = allieBox.isShiny;
            newCreamonCaught.stats = allieBox.stats;
            newCreamonCaught.exp = 0;
            Debug.Log(allieBox.name + " evolved into " + newCreamonCaught.name);

            GameData.Instance.PlayerBox.RemoveAt(indexInList);
            GameData.Instance.PlayerBox.Insert(indexInList, newCreamonCaught);
            allieBox = newCreamonCaught;
            particuleEvolve.Play();
            SetDatasOnScreen();
            confirmRelease.SetActive(false);
            GameData.Instance.Pokedex[allieBox.id].captureLevel++;
        }
        else
        {
            Debug.Log("ThisCantEvolve");
        }
    }

    public void SetAsMain()
    {
        GameData.Instance.mainSelectedAllie = indexInList;
        SetDatasOnScreen();
    }

    public void ShowShinyParticle()
    {
        particuleShiny.Play();
    }
}
