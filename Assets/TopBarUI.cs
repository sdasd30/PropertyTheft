using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopBarUI : MonoBehaviour
{
    public TMP_Text PlayerNameText;
    public TMP_Text SwappingModeText;
    public TMP_Text MaterialTypeText;
    public TMP_Text MaterialWeightText;
    public TMP_Text MaterialThresholdText;
    public TMP_Text BehaviorTypeText;
    public TMP_Text FanDirectionText;
    public TMP_Text FanForceText;
    public TMP_Text FanRangeText;
    public TMP_Text MoveSpeedText;
    public TMP_Text MoveFallAvoidanceText;
    public GameObject player_game_object;
    public GameObject MaterialOutline;
    public GameObject BehaviorOutline;
    private PlayerSwapProperty player_swap_script;
    


    public GameObject FanContainer;
    public GameObject MoveContainer;
    // Start is called before the first frame update
    void Start()
    {
        PlayerNameText.text = "RED";
        PlayerNameText.color = new Color(255, 0, 0);
        player_swap_script = player_game_object.GetComponent<PlayerSwapProperty>();
        MaterialTypeText.text = "NONE";
        MaterialWeightText.text = "NONE";
        MaterialThresholdText.text = "NONE";
        BehaviorOutline.SetActive(false);
        MaterialOutline.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        if (player_swap_script.swapObject)
        {
            //Debug.Log("here");
            GameObject hit_game_object = player_swap_script.swapObject;
            MaterialHolder material_info = hit_game_object.GetComponent<MaterialHolder>();
            MaterialTypeText.text = material_info.propertyList[0].materialName;
            MaterialWeightText.text = material_info.propertyList[0].mass.ToString();
            MaterialThresholdText.text = material_info.propertyList[0].destructionThreshold.ToString();

            Basic_AI object_AI = player_swap_script.swapObject.GetComponent<Basic_AI>();
            if (object_AI)
            {
                if (object_AI.AI_type == 0)
                {
                    BehaviorTypeText.text = "NONE";
                    FanContainer.SetActive(false);
                    MoveContainer.SetActive(false);
                }
                if (object_AI.AI_type == 1)
                {
                    BehaviorTypeText.text = "MOVE BACK AND FORTH";
                    MoveSpeedText.text = object_AI.AI_1_speed.ToString();
                    MoveFallAvoidanceText.text = "FALSE";
                    MoveContainer.SetActive(true);
                    FanContainer.SetActive(false);
                }
                if (object_AI.AI_type == 7)
                {
                    BehaviorTypeText.text = "FAN";
                    FanContainer.SetActive(true);
                    MoveContainer.SetActive(false);
                    if (object_AI.fan_direction == 1)
                    {
                        FanDirectionText.text = "RIGHT";
                    }
                    else if (object_AI.fan_direction == 2)
                    {
                        FanDirectionText.text = "DOWN";
                    }
                    else if (object_AI.fan_direction == 3)
                    {
                        FanDirectionText.text = "LEFT";
                    } else
                    { 
                        FanDirectionText.text = "UP";
                    }

                    if (object_AI.fan_speed == 60)
                    {
                        FanForceText.text = "WOOD, PLASTIC";
                    } else
                    {
                        FanForceText.text = "WOOD, PLASTIC, STEEL";
                    }

                    FanRangeText.text = object_AI.fan_range.ToString();
                }
            }
        } else
        {
            FanContainer.SetActive(false);
            MoveContainer.SetActive(false);
            MaterialTypeText.text = "NONE";
            MaterialWeightText.text = "NONE";
            MaterialThresholdText.text = "NONE";
            BehaviorTypeText.text = "NONE";


        }
        if (player_swap_script.swap_AI)
        {
            SwappingModeText.text = "Behavior";
            SwappingModeText.color = new Color(0, 0, 255);
            MaterialOutline.SetActive(false);
            BehaviorOutline.SetActive(true);
        } else
        {
            SwappingModeText.text = "Material";
            SwappingModeText.color = new Color(255, 0, 0);
            MaterialOutline.SetActive(true);
            BehaviorOutline.SetActive(false);
        }
        
    }
}
