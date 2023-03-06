using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopBarUI : MonoBehaviour
{
    public TMP_Text PlayerNameText;
    public TMP_Text MaterialTypeText;
    public TMP_Text MaterialWeightText;
    public TMP_Text SwappingModeTexts;
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
    public GameObject MaterialContainer;
    public GameObject BehaviorContainer;
    public GameObject UI_Bar;
    public GameObject SwappingContainer;
    public GameObject PlayerContainer;
    public GameObject UI_Outline;
    public GameObject SwappingModeText;
    public bool UI_Toggle;
    private bool UI_Toggle2;
    private PlayerSwapProperty player_swap_script;
    private float cooldown;
    private float marked_time;
    private float marked_time2;
    


    public GameObject FanContainer;
    public GameObject MoveContainer;
    // Start is called before the first frame update
    void Start()
    {
        UI_Toggle = false;
        UI_Toggle2 = false;
        UI_Outline.SetActive(false);
        cooldown = .25f;
        
        marked_time = Time.time;
        marked_time2 = Time.time;
        PlayerNameText.text = "RED";
        PlayerNameText.color = new Color(255, 0, 0);
        player_swap_script = player_game_object.GetComponent<PlayerSwapProperty>();
        MaterialTypeText.text = "NONE";
        MaterialWeightText.text = "NONE";
        MaterialThresholdText.text = "NONE";

        MaterialContainer.SetActive(false);
        BehaviorContainer.SetActive(false);
        SwappingContainer.SetActive(false);
        PlayerContainer.SetActive(false);
        SwappingModeText.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Fire4") > .5 && Time.time > marked_time + cooldown )
        {
            marked_time = Time.time;
            if (UI_Toggle)
            {
                DeactivateUI();
              
                UI_Toggle = false;
            }
            else
            {
                ReactivateUI();
                
                UI_Toggle = true;
            }
        }

        if (Input.GetAxisRaw("Fire3") > .5 && Time.time > marked_time2 + cooldown)
        {
            marked_time2 = Time.time;
            if (UI_Toggle2)
            {
                SwappingContainer.SetActive(false);
                UI_Toggle2 = false;
            }
            else
            {
                SwappingContainer.SetActive(true);
                UI_Toggle2 = true;
            }
        }

        if (player_swap_script.swap_AI)
        {
            SwappingModeTexts.text = "Behavior";
            SwappingModeTexts.color = new Color(0, 0, 255);
        }
        else
        {
            SwappingModeTexts.text = "Material";
            SwappingModeTexts.color = new Color(255, 0, 0);
        }

        if (UI_Toggle)
            {
            
            if (player_swap_script.swapObject)
            {
                //UI_Outline.SetActive(true);
                GameObject hit_game_object = player_swap_script.swapObject;
                MaterialHolder material_info = hit_game_object.GetComponent<MaterialHolder>();
                MaterialTypeText.text = material_info.propertyList[0].materialName;
                MaterialWeightText.text = material_info.propertyList[0].mass.ToString() + " lb";
                MaterialThresholdText.text = material_info.propertyList[0].destructionThreshold.ToString() + " lb";

                Basic_AI object_AI = player_swap_script.swapObject.GetComponent<Basic_AI>();
                if (object_AI)
                {
                    if (object_AI.AI_type == 0)
                    {
                        BehaviorTypeText.text = "STATIONARY";
                        FanContainer.SetActive(false);
                        MoveContainer.SetActive(false);
                    }
                    if (object_AI.AI_type == 1)
                    {
                        BehaviorTypeText.text = "MOVING";
                        MoveSpeedText.text = " " + object_AI.speed.ToString();
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
                        }
                        else
                        {
                            FanDirectionText.text = "UP";
                        }

                        if (object_AI.fan_speed <= 60)
                        {
                            FanForceText.text = "3 lb";
                        }
                        else if (object_AI.fan_speed > 60)
                        {
                            FanForceText.text = "10 lb";
                        }

                        FanRangeText.text = object_AI.fan_range.ToString();
                    }
                }
            }
            else
            {
                //UI_Outline.SetActive(false);
                FanContainer.SetActive(false);
                MoveContainer.SetActive(false);
                MaterialTypeText.text = "NONE";
                MaterialWeightText.text = "NONE";
                MaterialThresholdText.text = "NONE";
                BehaviorTypeText.text = "NONE";
            }

            if (player_swap_script.swap_AI)
            {
                MaterialContainer.SetActive(false);
                if (player_swap_script.swapObject)
                {
                    BehaviorContainer.SetActive(true);
                    

                }
                else
                {
                    BehaviorContainer.SetActive(false);
                 
                }

            }
            else
            {
                BehaviorContainer.SetActive(false);
                if (player_swap_script.swapObject)
                {
                    MaterialContainer.SetActive(true);
                    
                }
                else
                {
                    MaterialContainer.SetActive(false);
                }
            }
        }



    void ReactivateUI()
       {
            MaterialContainer.SetActive(true);
            BehaviorContainer.SetActive(true);
            //SwappingContainer.SetActive(true);
            //SwappingModeText.SetActive(true);
        }

    void DeactivateUI()
        {
            MaterialContainer.SetActive(false);
            BehaviorContainer.SetActive(false);
            //SwappingContainer.SetActive(false);
            //SwappingModeText.SetActive(false);
        }
        
    }
}
