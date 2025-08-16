using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static CaptureManager;

namespace PokeDatas
{
    public class GameData : MonoBehaviour
    {
        public enum SceneName
        {
            MAIN,
            BOX,
            SETTINGS,
            POKEDEX,
            SHARE,
            QRSCAN,
            CAPTURE,
        };

        [System.Serializable]
        public class Allie
        {
            public int id;
            public int rarity;
            public Sprite sprite;
            public Sprite spriteShiny;
            public string name;
            public bool canEvolve;
            public int idEvolved;
            public float scale;

            //public string localisation;
            //public float catchRate;

            //Contient les stats et les informations du Pokemon
            public Allie() { }
            //public Allie(int _id, Stats _stats, Sprite _sprite, string _name)
            //{
            //    id = _id;
            //    sprite = _sprite;
            //    name = _name;
            //}
        }
        private class SavePokedex
        {
            public List<GameData.PokedexInfo> savePokedex = new List<GameData.PokedexInfo>();
        }
        [System.Serializable]
        public class AllieIndividual : Allie
        {
            public bool isShiny;
            public Stats stats;
            public int exp;

            public AllieIndividual(Allie allie)
            {
                id = allie.id;
                rarity = allie.rarity;
                sprite = allie.sprite;
                spriteShiny = allie.spriteShiny;
                name = allie.name;
                canEvolve = allie.canEvolve;
                idEvolved = allie.idEvolved;
            }

            public AllieIndividual()
            {

            }
        }

        [System.Serializable]
        public class PokedexInfo
        {
            public bool isPokemonCatched;
            public bool isShinyObtained;
            public int captureLevel;

            public float weight;
            public float height;
            public CaptureManager.ZoneName zoneName;

        }
        //CONTENT OF THE GAMEDATA
        public List<Allie> Allies = new List<Allie>();
        public List<PokedexInfo> Pokedex = new List<PokedexInfo>();
        public List<PokedexInfo> InitPokedex = new List<PokedexInfo>();
        public List<AllieIndividual> PlayerBox = new List<AllieIndividual>();
        public string testSingleton;
        public List<Sprite> backGroundBlurredList = new List<Sprite>();
        public List<Sprite> backGroundList = new List<Sprite>();
        public int mainSelectedAllie;
        public Allie stockedAllie;
        public AllieIndividual stockedIndividualAllie;
        public int shinyRate = 64;
        ////Which button hase been pressed
        public string ButtonPressedName;

        public int ButtonPressedIndex;
        ///QR CODE
        public string QRCodeStr = null;
        float timerSave;

        /// /////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                Load();
                _instance.SavePokedexIntoFile();
            }
            else
            {
                Destroy(gameObject);
                Debug.Log("Destroy GameData");
            }
        }
        public void Update()
        {
            timerSave -= Time.deltaTime;
            if (timerSave <= 0)
            {
                timerSave = 5.0f;
                Save();
            }
        }
        private GameData() { }
        private static GameData _instance;
        public static GameData Instance { get { return _instance; } }

        [System.Serializable]
        public class InfoPlayer
        {
            public bool firstTime = true;
            public int starterChoose = 0;
            public string PseudoPlayer;
            public int selectedPictures = 0;
        }
        public InfoPlayer infoplayer = new InfoPlayer();

        //Retourne la box du player
        public List<AllieIndividual> GetAlliesPlayerBox()
        {
            return PlayerBox;
        }

        public Allie GetSpecificAllie(int _id)
        {
            return Allies[_id];
        }
        public AllieIndividual GetAllieBox(int _id)
        {
            return PlayerBox[_id];
        }

        public string getTest()
        {
            return "Test Singleton";
        }
        public void setTest(string _test)
        {
            testSingleton = _test;
        }

        //Change Scene Function
        public void ChangeScene(string _sceneName)
        {

            SceneManager.LoadScene(_sceneName);
        }
        public Allie GetStockedAllie()
        {
            return stockedAllie;
        }
        public void SetStockedAllie(int _id)
        {
            stockedAllie = Allies[_id];
        }
        public PokedexInfo GetPokedexInfo(int _id)
        {
            return Pokedex[_id];
        }
        public AllieIndividual GetStockedIndividualAllie()
        {
            return stockedIndividualAllie;
        }
        public void SetStockedIndividualAllie(AllieIndividual _individual)
        {
            stockedIndividualAllie = _individual;
        }
        public void ChangeScene(int _sceneName)
        {
            SceneManager.LoadScene(_sceneName);
        }

        public void ChangeSceneButton(string _sceneName)
        {
            SceneManager.LoadScene(_sceneName);
            ButtonPressedName = GetComponentInParent<Button>().name;
            Int32.TryParse(GetComponent<Button>().name, out ButtonPressedIndex);
            if (ButtonPressedName != null)
            {
            }
        }
        public int GetSceneNumberInTotal()
        {
            return SceneManager.sceneCountInBuildSettings;
        }

        public void AddCreamon()
        {
            GameData.AllieIndividual newCreamonCaught = new GameData.AllieIndividual(GameData.Instance.Allies.Where(creamon => creamon.id == stockedIndividualAllie.id).Select(creamon => creamon).ToList()[0]);
            newCreamonCaught.isShiny = stockedIndividualAllie.isShiny;
            newCreamonCaught.stats = stockedIndividualAllie.stats;
            Debug.Log("Creamon made " + GameData.Instance.PlayerBox.Count);
            GameData.Instance.PlayerBox.Add(newCreamonCaught);
            GameData.Instance.Pokedex[newCreamonCaught.id - 1].isPokemonCatched = true;
            GameData.Instance.Pokedex[newCreamonCaught.id - 1].captureLevel++;
            stockedIndividualAllie = null;
        }

        private class SaveClass
        {
            public List<GameData.AllieIndividual> saveAllieBox = new List<GameData.AllieIndividual>();
        }

        public void Save()
        {
            SaveBoxToJson();
            SavePokedexIntoFile();
            SaveInfoToJson();
            Debug.Log("Save");

        }

        public static double RoundUp(double input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * multiplier) / multiplier;
        }
        public void Load()
        {
            LoadBoxToJson();
            LoadPokedexFromFile();
            LoadInfoToJson();
            Debug.Log("Load");
            if (GameData.Instance.infoplayer.firstTime)
            {
                for (int i = 0; i < 25; i++)
                {
                    PokedexInfo initAllie = new PokedexInfo();
                    InitPokedex.Add(initAllie);
                    Pokedex[i].weight = UnityEngine.Random.Range(0.0f, 80.0f);
                    Pokedex[i].weight = (float)RoundUp(Pokedex[i].weight, 2);

                    Pokedex[i].height = UnityEngine.Random.Range(1.0f, 3.0f);
                    Pokedex[i].height = (float)RoundUp(Pokedex[i].height, 2);
                    Pokedex[i].isPokemonCatched = true;
                }
                Pokedex[0].zoneName = ZoneName.CREA_FRONT2; ;
                Pokedex[1].zoneName = ZoneName.CREA_FRONT2;
                Pokedex[2].zoneName = ZoneName.CREA_FRONT1;
                Pokedex[3].zoneName = ZoneName.CREA_FRONT1;
                Pokedex[4].zoneName = ZoneName.HALL1;
                Pokedex[5].zoneName = ZoneName.HALL2; ;
                Pokedex[6].zoneName = ZoneName.PIZZA_TRUCK1;
                Pokedex[7].zoneName = ZoneName.CAFETERIA1;
                Pokedex[8].zoneName = ZoneName.CAFETERIA1;
                Pokedex[9].zoneName = ZoneName.HALL1;
                Pokedex[10].zoneName = ZoneName.CREA_FRONT1; ;
                Pokedex[11].zoneName = ZoneName.CAFETERIA1;
                Pokedex[12].zoneName = ZoneName.HALL1;
                Pokedex[13].zoneName = ZoneName.HALL1;
                Pokedex[14].zoneName = ZoneName.PIZZA_TRUCK1;
                Pokedex[15].zoneName = ZoneName.CREA_FRONT1;
                Pokedex[16].zoneName = ZoneName.PIZZA_TRUCK1;
                Pokedex[17].zoneName = ZoneName.HALL1;
                Pokedex[18].zoneName = ZoneName.CAFETERIA1;
                Pokedex[19].zoneName = ZoneName.PIZZA_TRUCK1;
                Pokedex[20].zoneName = ZoneName.CAFETERIA1;
                Pokedex[21].zoneName = ZoneName.CAFETERIA1;
                Pokedex[22].zoneName = ZoneName.SPACE2;
                Pokedex[23].zoneName = ZoneName.SPACE1;
                Pokedex[24].zoneName = ZoneName.SPACE1;
            }
        }

        public void SaveBoxToJson()
        {
            if (!File.Exists(Application.persistentDataPath + "/BoxSaveFile.json"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath + "/BoxSaveFile.json"));
            }

            SaveClass temp = new SaveClass() { saveAllieBox = GameData.Instance.PlayerBox };
            string json = JsonUtility.ToJson(temp, true);
            Debug.Log(json);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/BoxSaveFile.json", json);
        }

        public void LoadBoxToJson()
        {
            if (File.Exists(Application.persistentDataPath + "/BoxSaveFile.json"))
            {
                string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/BoxSaveFile.json");
                Debug.Log(json);
                GameData.Instance.PlayerBox = JsonUtility.FromJson<SaveClass>(json).saveAllieBox;
                Debug.Log(GameData.Instance.PlayerBox);
            }
        }
        public void SavePokedexIntoFile()
        {
            if (!File.Exists(Application.persistentDataPath + "/PokedexSaveFile.json"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath + "/PokedexSaveFile.json"));
            }

            SavePokedex temp = new SavePokedex() { savePokedex = GameData.Instance.Pokedex };
            string json = JsonUtility.ToJson(temp, true);
            Debug.Log(json);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/PokedexSaveFile.json", json);
        }
        public void LoadPokedexFromFile()
        {
            if (File.Exists(Application.persistentDataPath + "/PokedexSaveFile.json"))
            {
                string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/PokedexSaveFile.json");
                Debug.Log(json);
                GameData.Instance.Pokedex = JsonUtility.FromJson<SavePokedex>(json).savePokedex;
                Debug.Log(GameData.Instance.PlayerBox);
            }
        }

        [System.Serializable]
        private class SaveInfoClass
        {
            public InfoPlayer infoplayer = new InfoPlayer();
        }

        public void SaveInfoToJson()
        {
            if (!File.Exists(Application.persistentDataPath + "/InfoSaveFile.json"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath + "/InfoSaveFile.json"));
            }

            Debug.Log("Save");
            SaveInfoClass temp = new SaveInfoClass() { infoplayer = GameData.Instance.infoplayer };
            string json = JsonUtility.ToJson(temp, true);
            Debug.Log(json);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/InfoSaveFile.json", json);
        }

        public void LoadInfoToJson()
        {
            if (File.Exists(Application.persistentDataPath + "/InfoSaveFile.json"))
            {
                Debug.Log("Load");
                string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/InfoSaveFile.json");
                Debug.Log(json);
                GameData.Instance.infoplayer = JsonUtility.FromJson<SaveInfoClass>(json).infoplayer;
                Debug.Log(GameData.Instance.infoplayer);
            }
        }
    }
}