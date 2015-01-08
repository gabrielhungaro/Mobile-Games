using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core;

namespace BateRebate
{
    public class BatePauseState : MonoBehaviour
    {

        private string bgUrl = "Scenes/Pause/pausebg";
        private string closeXUrl = "Scenes/Pause/ClosePause";
        private string returnUrl = "Scenes/Pause/VoltarAoMenu";
        private string soundOnUrl = "Scenes/Pause/SoundOn";
        private string soundOffUrl = "Scenes/Pause/SoundOff";
        private string p1SelectedUrl = "Scenes/Pause/P1Selected";
        private string p2SelectedUrl = "Scenes/Pause/P2Selected";

        private GameObject background;
        private GameObject closeBtn;
        private GameObject returnBtn;
        private GameObject soundOnBtn;
        private GameObject soundOffBtn;
        private GameObject p1SelectedBtn;
        private GameObject p2SelectedBtn;

        private BateController main;
        private BateGameState game;

        public void StartScene(BateGameState m_game)
        {
            main = BateController.Instance;
            game = m_game;

            bgUrl = main.AddPlatformAndQualityToUrl(bgUrl);
            closeXUrl = main.AddPlatformAndQualityToUrl(closeXUrl);
            returnUrl = main.AddPlatformAndQualityToUrl(returnUrl);
            soundOnUrl = main.AddPlatformAndQualityToUrl(soundOnUrl);
            soundOffUrl = main.AddPlatformAndQualityToUrl(soundOffUrl);
            p1SelectedUrl = main.AddPlatformAndQualityToUrl(p1SelectedUrl);
            p2SelectedUrl = main.AddPlatformAndQualityToUrl(p2SelectedUrl);

            background = GameObject.Find("bg");
            closeBtn = GameObject.Find("closeX");
            returnBtn = GameObject.Find("return");
            soundOnBtn = GameObject.Find("soundOn");
            soundOffBtn = GameObject.Find("soundOff");
            p1SelectedBtn = GameObject.Find("p1Selected");
            p2SelectedBtn = GameObject.Find("p2Selected");

            background.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(bgUrl)) as Sprite;
            background.GetComponent<Image>().preserveAspect = true;
            
            closeBtn.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(closeXUrl)) as Sprite;
            closeBtn.GetComponent<Image>().preserveAspect = true;
            closeBtn.GetComponent<Button>().onClick.AddListener(this.Close);
            closeBtn.SetActive(true);
            
            returnBtn.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(returnUrl)) as Sprite;
            returnBtn.GetComponent<Image>().preserveAspect = true;
            returnBtn.GetComponent<Button>().onClick.AddListener(main.GoToMenu);
            returnBtn.SetActive(true);

            soundOnBtn.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(soundOnUrl)) as Sprite;
            soundOnBtn.GetComponent<Image>().preserveAspect = true;
            soundOnBtn.GetComponent<Button>().interactable = (main.IsSounding);
            soundOnBtn.SetActive(main.IsSounding);
            soundOnBtn.GetComponent<Button>().onClick.AddListener(this.PlaySound);
            
            soundOffBtn.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(soundOffUrl)) as Sprite;
            soundOffBtn.GetComponent<Image>().preserveAspect = true;
            soundOffBtn.GetComponent<Button>().interactable = (!main.IsSounding);
            soundOffBtn.SetActive(!main.IsSounding);
            soundOffBtn.GetComponent<Button>().onClick.AddListener(this.MuteSound);

            p1SelectedBtn.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(p1SelectedUrl)) as Sprite;
            p1SelectedBtn.GetComponent<Image>().preserveAspect = true;
            p1SelectedBtn.GetComponent<Button>().interactable = (main.PlayerNumber == 1);
            p1SelectedBtn.SetActive(p1SelectedBtn.GetComponent<Button>().interactable);
            p1SelectedBtn.GetComponent<Button>().onClick.AddListener(this.SinglePlayer);
            
            p2SelectedBtn.GetComponent<Image>().sprite = Instantiate(AFAssetManager.Instance.Load<Sprite>(p2SelectedUrl)) as Sprite;
            p2SelectedBtn.GetComponent<Image>().preserveAspect = true;
            p2SelectedBtn.GetComponent<Button>().interactable = (!p1SelectedBtn.GetComponent<Button>().interactable);
            p2SelectedBtn.SetActive(!p1SelectedBtn.GetComponent<Button>().interactable);
            p2SelectedBtn.GetComponent<Button>().onClick.AddListener(this.MultiPlayer);
        }
        public void Close()
        {
            Debug.Log("CloseBTN!!!");
            game.OnClickUnPause();
        }
        public void PlaySound()
        {
            soundOnBtn.GetComponent<Button>().interactable = false;
            soundOnBtn.SetActive(false);
            main.PlaySound();
            soundOffBtn.GetComponent<Button>().interactable = true;
            soundOffBtn.SetActive(true);
        }
        public void MuteSound()
        {
            soundOffBtn.GetComponent<Button>().interactable = false;
            soundOffBtn.SetActive(false);
            main.MuteSound();
            soundOnBtn.GetComponent<Button>().interactable = true;
            soundOnBtn.SetActive(true);
        }
        public void SinglePlayer()
        {
            p1SelectedBtn.GetComponent<Button>().interactable = false;
            p1SelectedBtn.SetActive(false);
            game.SinglePlayer();
            p2SelectedBtn.GetComponent<Button>().interactable = true;
            p2SelectedBtn.SetActive(true);
        }
        public void MultiPlayer()
        {
            p2SelectedBtn.GetComponent<Button>().interactable = false;
            p2SelectedBtn.SetActive(false);
            game.MultiPlayer();
            p1SelectedBtn.GetComponent<Button>().interactable = true;
            p1SelectedBtn.SetActive(true);
        }
    }
}
