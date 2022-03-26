using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishRaceScreen : MonoBehaviour
{
    string url = "https://t05-jrosa.vigion.pt/API/Objects/UserCommonUpdate.php";

    [Header("Finish")]
    public GameObject Finish;

    [Header("Stars")]
    public Image Star1;
    public Image Star2;
    public Image Star3;

    [Header("Times")]
    public Text TimeLap1;
    public Text TimeLap2;
    public Text TimeLap3;

    [Header("Money")]
    public Text MoneyGained;
    public Text MoneyShadow;
    int RepGained;

    [Header("Buttons")]
    public Button ButtonContinue;
    public Button ButtonRetry;

    [Header("Colors")]
    public Color startColor, endColor;

    bool isLapTimeUpdatable = false;
    bool isMoneyUpdatable = false;
    bool updateStars = false;

    int nStars = 0;
    private int lapToUpdate = 0;

    float secs;
    float milisecs;
    float min;

    float mili;
    float sec;
    float minu;

    string mil;
    string se;
    string mi;

    Text TimeLapToUpdate;

    // Start is called before the first frame update
    void Awake()
    {
        //esconde o menu e associa um evento ao eventController ativo 
        gameObject.SetActive(false);
        ButtonContinue.interactable = false;
        ButtonRetry.interactable = false;
        EventController.current.onRaceFinish += RaceEnd;
    }

    // Update is called once per frame
    void Update()
    {
        //faz com que a sombra do texto que contem dinheiro seja igual ao dinheiro em si
        MoneyShadow.text = MoneyGained.text;
    }

    private void FixedUpdate()
    {

        //verifica se pode dar update aos tempos do menu final
        if (isLapTimeUpdatable)
        {

            #region Updates nos tempos

            //verifica qual é a volta que deve atualizar
            switch (lapToUpdate)
            {
                case 0:
                    TimeLapToUpdate = TimeLap1;
                    break;
                case 1:

                    //verifica se o tempo é bom o suficiente para receber uma estrela
                    if (min < 1 && secs < 59f && TimeLapToUpdate != TimeLap2)
                    {
                        nStars += 1;
                        Debug.LogWarning("Star added : " + nStars);
                    }

                    //associa a volta para atualizar ao objeto certo
                    TimeLapToUpdate = TimeLap2;
                    break;
                case 2:
                    if (min < 1 && secs < 59f && TimeLapToUpdate != TimeLap3)
                    {
                        nStars += 1;
                        Debug.LogWarning("Star added : " + nStars);
                    }
                    TimeLapToUpdate = TimeLap3;
                    break;
                default:

                    //muda as variaveis de forma a que se possa começar a atualizar as outras informações
                    lapToUpdate = 0;
                    isLapTimeUpdatable = false;
                    isMoneyUpdatable = true;
                    updateStars = true;
                    
                    break;
            }
            //verifica se o tempo nas voltas pode ser atualizado 
            if (isLapTimeUpdatable)
            {
                //virifica se os milisegundos apresentados são iguais aos milisegundos guardados
                if (mili < milisecs)
                {
                    //caso não adiciona um milisegundo ao tempo mostrado
                    mili += 1;

                    //fromatam os milisegundos
                    mil = mili.ToString("00");

                    //apresentam os milisegundos no menu
                    TimeLapToUpdate.text = $"00 : 00 : {mil}";
                }
                else if (sec < secs) //virifica se os segundos apresentados são iguais aos segundos guardados
                {
                    sec += 1;
                    se = sec.ToString("00");
                    TimeLapToUpdate.text = $"00 : {se} : {mil}";
                }
                else if (minu < min) //virifica se os segundos apresentados são iguais aos segundos guardados
                {
                    minu += 1;
                    mi = minu.ToString("00");
                    TimeLapToUpdate.text = $"{mi} : {se} : {mil}";
                }
                else //caso nenhum dos ifs seja verdade significa que o tempo da volta já foi atualizado e pode-se atualizar a proxima volta
                {
                    lapToUpdate += 1;

                    mili = 00;
                    sec = 00;
                    minu = 00;

                    //divide o tempo por cada fração para poder demonstrar de forma dinamica
                    milisecs = float.Parse(Data.Track.lapTimes[lapToUpdate].Split(':')[2]);
                    secs = float.Parse(Data.Track.lapTimes[lapToUpdate].Split(':')[1]);
                    min = float.Parse(Data.Track.lapTimes[lapToUpdate].Split(':')[0]);
                }
            }
            #endregion
        }
        else if (isMoneyUpdatable)  //verifica se pode atualizar o dinheiro e as estrelas que o jogador ganhou
        {
            if (updateStars) //verifica se pode atualizar as estrelas 
            {
                Debug.LogWarning("Number of stars : " + nStars);

                switch (nStars) //dependendo do numero de estrelas ganho muda o valor de dinheiro ganho e anima um numero diferente de estrelas
                {
                    case 0:
                        //muda os valores ganhos
                        MoneyGained.text = "+15";
                        RepGained = 10;
                        break;
                    case 1:
                        //muda os valores ganhos
                        MoneyGained.text = "+50";
                        RepGained = 50;

                        //anima a estrela que ganhou 
                        Star1.gameObject.LeanScale(new Vector3(1.3f, 1.3f), 0.2f).setOnComplete(() => { Star1.color = Color.Lerp(startColor, endColor, 0.3f); Star1.gameObject.LeanScale(new Vector3(1f, 1f), 0.2f); });
                        break;
                    case 2:
                        //muda os valores ganhos
                        MoneyGained.text = "+100";
                        RepGained = 200;

                        //anima as estrelas que ganhou
                        Star1.gameObject.LeanScale(new Vector3(1.3f, 1.3f), 0.2f).setOnComplete(() => { 

                            Star1.color = Color.Lerp(startColor, endColor, 0.3f); Star1.gameObject.LeanScale(new Vector3(1f, 1f), 0.2f);

                            Star2.gameObject.LeanScale(new Vector3(1.3f, 1.3f), 0.2f).setOnComplete(() => {

                                Star2.color = Color.Lerp(startColor, endColor, 1f); Star2.gameObject.LeanScale(new Vector3(1f, 1f), 0.2f);

                            });

                        });

                        break;
                    case 3:
                        //muda os valores ganhos
                        MoneyGained.text = "+150";
                        RepGained = 350;

                        //anima as estrelas que ganhou
                        Star1.gameObject.LeanScale(new Vector3(1.3f, 1.3f), 0.2f).setOnComplete(() => { 

                            Star1.color = Color.Lerp(startColor, endColor, 1f); Star1.gameObject.LeanScale(new Vector3(1f, 1f), 0.2f);

                            Star2.gameObject.LeanScale(new Vector3(1.3f, 1.3f), 0.2f).setOnComplete(() => { 

                                Star2.color = Color.Lerp(startColor, endColor, 1f); Star2.gameObject.LeanScale(new Vector3(1f, 1f), 0.2f);

                                Star3.gameObject.LeanScale(new Vector3(1.3f, 1.3f), 0.2f).setOnComplete(() => {

                                    Star3.color = Color.Lerp(startColor, endColor, 1f); Star3.gameObject.LeanScale(new Vector3(1f, 1f), 0.2f);
                                });
                            });
                        });
                        
                        break;
                    default:
                        break;
                }

                //indica que as estrelas já foram atualizadas
                updateStars = false;
            }

            //caso as estrelas já tenham cido atualizadas começa as animações das recompensas
            if (!updateStars)
            {
                //remove os caracteres extra para ser facil converter o dinheiro ganho
                string money = MoneyGained.text.Replace("+", "");

                //vai diminuindo o valor do dinheiro ganho que é demonstrado no menu até 0
                if (nStars > 0 && int.Parse(money) != 0)
                {
                    Data.Player.Money += 1;
                    MoneyGained.text = "+" + (int.Parse(money) - 1).ToString();
                }

                //anima a recompença de nivel
                if (RepGained > 0 )
                {
                    //muda a velocidade da animação dependendo de quanto recebeu
                    switch (nStars)
                    {
                        case 1:
                            Data.Player.Reputation += 1;
                            RepGained -= 1;
                            break;

                        case 2:
                            Data.Player.Reputation += 4;
                            RepGained -= 4;
                            break;

                        case 3:
                            Data.Player.Reputation += 7;
                            RepGained -= 7;
                            break;

                        default:
                            break;
                    }

                }

                //verifica se ambas as recompensas ja foram atribuidas
                if (RepGained <= 0 && int.Parse(money) <= 0)
                {
                    isMoneyUpdatable = false;
                    isLapTimeUpdatable = false;
                    UpdateCar();
                }
            }

        }
    }

    public void RetryButtonClick()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void ContinueButton()
    {
        SceneManager.LoadSceneAsync("GarageLobby", LoadSceneMode.Single);
    }

    void RaceEnd()
    {
        mili = 00;
        sec = 00;
        minu = 00;

        milisecs = float.Parse(Data.Track.lapTimes[lapToUpdate].Split(':')[2]);
        secs = float.Parse(Data.Track.lapTimes[lapToUpdate].Split(':')[1]);
        min = float.Parse(Data.Track.lapTimes[lapToUpdate].Split(':')[0]);

        if (min < 1 && secs < 59f)
        {
            nStars = 1;
            Debug.LogWarning("Star added : " + nStars);
        }

        gameObject.SetActive(true);

        Finish.LeanAlpha(1f, 0.3f);
        Finish.LeanScale(new Vector3(0.344f, 0.344f, 0.344f), 1f);
        isLapTimeUpdatable = true;
    }


    public void UpdateCar()
    {
        Debug.Log("Starting Corutine");
        StartCoroutine(UpdateCarSelected());
    }

    IEnumerator UpdateCarSelected()
    {
        Debug.Log("Making The web form");
        WWWForm form = new WWWForm();
        form.AddField("ID", Data.Player.ID);
        form.AddField("UserName", Data.Player.UserName);
        form.AddField("Money", Data.Player.Money);
        form.AddField("Reputation", Data.Player.Reputation);
        form.AddField("UserCarIDSelected", Data.Player.UserCarIDSelected);

        //cria um web request que vai ligar a api e enviar a informação
        UnityWebRequest ww = UnityWebRequest.Post(url, form);

        //inicia o web request
        yield return ww.SendWebRequest();
        Debug.Log(ww.downloadHandler.text);

        //caso haja erros mostra no debug log
        if (ww.error != null)
        {
            Debug.Log(ww.downloadHandler.text);
            Debug.LogError(ww.error);
        }
        else
        {
            if (ww.isDone)
            {
                Debug.Log("feito");
                Debug.Log(ww.downloadHandler.text);
                if (ww.downloadHandler.text.Contains("sucesso"))
                {
                    Debug.Log(ww.downloadHandler.text);
                    Debug.Log("feito");
                    //TODO : Pop up a confirmar que funcionou
                }
            }
        }
        ww.Dispose();

        //ativa os butões
        ButtonContinue.interactable = true;
        ButtonRetry.interactable = true;
    }

}
