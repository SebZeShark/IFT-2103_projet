using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class options : MonoBehaviour
{
    InputActionMap k;
    public InputActionAsset settings;

    public Button b_avancer;
    public Button b_reculer;
    public Button b_gauche;
    public Button b_droite;
    public Button b_tirer;

    public Button b_avancer_j2;
    public Button b_reculer_j2;
    public Button b_gauche_j2;
    public Button b_droite_j2;
    public Button b_tirer_j2;

    public Button b_save;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Awake
    void Awake()
    {
        DontDestroyOnLoad(settings);

        overrideBind(); // Applique les changement de contrôles préallables

        // J1
        // Avancer
        InputAction act = settings.FindActionMap("Player1").FindAction("Avancer");
        var bindIndex = act.bindings.IndexOf(x => x.isPartOfComposite && x.name == "positive");
        b_avancer.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);

        // Reculer
        bindIndex = act.bindings.IndexOf(x => x.isPartOfComposite && x.name == "negative");
        b_reculer.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);

        // Tourner Gauche
        act = settings.FindActionMap("Player1").FindAction("Tourner");
        bindIndex = act.bindings.IndexOf(x => x.isPartOfComposite && x.name == "negative");
        b_gauche.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);

        // Tourner Droite
        bindIndex = act.bindings.IndexOf(x => x.isPartOfComposite && x.name == "positive");
        b_droite.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);

        // Tirer
        act = settings.FindActionMap("Player1").FindAction("Fire");
        b_tirer.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(0);


        // J2
        // Avancer
        act = settings.FindActionMap("Player2").FindAction("Avancer");
        bindIndex = act.bindings.IndexOf(x => x.isPartOfComposite && x.name == "positive");
        b_avancer_j2.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);

        // Reculer
        bindIndex = act.bindings.IndexOf(x => x.isPartOfComposite && x.name == "negative");
        b_reculer_j2.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);

        // Tourner Gauche
        act = settings.FindActionMap("Player2").FindAction("Tourner");
        bindIndex = act.bindings.IndexOf(x => x.isPartOfComposite && x.name == "negative");
        b_gauche_j2.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);

        // Tourner Droite
        bindIndex = act.bindings.IndexOf(x => x.isPartOfComposite && x.name == "positive");
        b_droite_j2.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);

        // Tirer
        act = settings.FindActionMap("Player2").FindAction("Fire");
        b_tirer_j2.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onAvancer(int player)
    {
        change("positive", "Avancer", player);
    }
    public void onReculer(int player)
    {
        change("negative", "Avancer", player);
    }
    public void onTournerG(int player)
    {
        change("negative", "Tourner", player);
    }
    public void onTournerD(int player)
    {
        change("positive", "Tourner", player);
    }
    public void onTirer(int player)
    {
        changeTirer(player);
    }
    public void onSave()
    {
        SceneManager.LoadScene("Menu");
    }

    void change(string sensAxe, string action, int player)
    {
        k = settings.FindActionMap("Player" + player.ToString());
        Debug.Log("Player" + player.ToString());

        b_save.interactable = false;

        InputAction act = k.FindAction(action);
        var bindIndex = act.bindings.IndexOf(x => x.isPartOfComposite && x.name == sensAxe);

        var rebind = act.PerformInteractiveRebinding();

        rebind?.Cancel();
        void CleanUp()
        {
            rebind?.Dispose();
            rebind = null;
            finishedRebind();
        }

        void saveChanges()
        {
            if(action == "Avancer")
            {
                if (sensAxe == "positive")
                {
                    if (player == 1)
                        b_avancer.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);
                    if (player == 2)
                        b_avancer_j2.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);
                }
                if(sensAxe == "negative")
                {
                    if (player == 1)
                        b_reculer.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);
                    if (player == 2)
                        b_reculer_j2.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);
                }
            }
            if(action == "Tourner")
            {
                if (sensAxe == "positive")
                {
                    if (player == 1)
                        b_droite.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);
                    if (player == 2)
                        b_droite_j2.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);
                }
                if (sensAxe == "negative")
                {
                    if (player == 1)
                        b_gauche.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);
                    if (player == 2)
                        b_gauche_j2.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(bindIndex);
                }
            }
            PlayerPrefs.SetString("Player" + player + action + sensAxe, act.bindings[bindIndex].overridePath);
        }

        //rebind = act.PerformInteractiveRebinding();

        rebind.WithControlsExcluding("Mouse");
        rebind.WithTargetBinding(bindIndex);
        rebind.WithCancelingThrough("<Keyboard>/escape");
        rebind.OnMatchWaitForAnother(0.1f);
        rebind.Start().OnCancel(x => 
        {
            CleanUp();
        }).OnComplete(x =>
        {
            saveChanges();
            CleanUp();
        });
    }

    void changeTirer(int player)
    {
        k = settings.FindActionMap("Player" + player.ToString());

        b_save.interactable = false;

        InputAction act = k.FindAction("Fire");

        var rebind = act.PerformInteractiveRebinding();

        rebind?.Cancel();
        void CleanUp()
        {
            rebind?.Dispose();
            rebind = null;
            finishedRebind();
        }

        void saveChanges()
        {
            if(player == 1)
                b_tirer.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(0);
            if(player == 2)
                b_tirer_j2.GetComponentInChildren<Text>().text = act.GetBindingDisplayString(0);
            PlayerPrefs.SetString("FirePlayer" + player, act.bindings[0].overridePath);
        }

        rebind = act.PerformInteractiveRebinding();

        rebind.WithControlsExcluding("Mouse");
        rebind.WithCancelingThrough("<Keyboard>/escape");
        rebind.OnMatchWaitForAnother(0.1f);
        rebind.Start().OnCancel(x => CleanUp()).OnComplete(x =>
        {
            saveChanges();
            CleanUp();
        });
    }

    void finishedRebind()
    {
        b_save.interactable = true;
    }

    private void OnDisable()
    {
        settings.Disable();
    }

    void overrideBind()
    {
        string[] players = { "Player1", "Player2" };
        string[] actions = { "Avancer", "Tourner" };
        string[] axes = { "negative", "positive" };

        foreach (string player in players)
        {
            foreach (string action in actions)
            {
                foreach (string axe in axes)
                {
                    string overBind = PlayerPrefs.GetString(player + action + axe);
                    if (overBind != "")
                    {
                        var act = settings.FindActionMap(player).FindAction(action);
                        act.ApplyBindingOverride(act.bindings.IndexOf(b => b.name == axe), overBind);
                    }
                }
            }

            if (PlayerPrefs.GetString("Fire" + player) != "")
                settings.FindActionMap(player).FindAction("Fire").ApplyBindingOverride(PlayerPrefs.GetString("Fire" + player));
        }
    }
}
