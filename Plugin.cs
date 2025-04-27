using BepInEx;
using GorillaNetworking;
using PlayFab.ClientModels;
using PlayFab;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using GorillaTag.CosmeticSystem;
using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Pun;
using System.Reflection;
using Photon.Realtime;

namespace CosmeticLookup
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        string cosmeticToSearch = "LBAAK.";
        string roomToSearch = "GOLDENTROPHY";
        string outputString = "GUI created by goldentrophy\nPress insert to hide";
        GUIStyle textFieldStyle;
        GUIStyle labelStyle;
        GUIStyle buttonStyle;
        bool isVisible;
        float visibleDelay = 0f;

        void OnGUI()
        {
            if (UnityInput.Current.GetKey(KeyCode.Insert) && Time.time > visibleDelay)
            {
                isVisible = !isVisible;
                visibleDelay = Time.time + 0.2f;
            }

            if (!isVisible)
                return;

            GUI.Box(new Rect(10, 10, 450, 600), "Cosmetic Lookup GUI");

            if (textFieldStyle == null)
            {
                textFieldStyle = GUI.skin.textField;
                textFieldStyle.fontSize = 25;
            }

            if (labelStyle == null)
            {
                labelStyle = GUI.skin.label;
                labelStyle.fontSize = 25;
            }

            if (buttonStyle == null)
            {
                buttonStyle = GUI.skin.button;
                buttonStyle.fontSize = 40;
            }
            
            GUI.Label(new Rect(20, 30, 185, 35), "Room", labelStyle);
            roomToSearch = GUI.TextField(new Rect(260, 30, 185, 35), roomToSearch, textFieldStyle);
            roomToSearch = roomToSearch.ToUpper();

            GUI.Label(new Rect(20, 75, 185, 35), "Cosmetic", labelStyle);
            cosmeticToSearch = GUI.TextField(new Rect(260, 75, 185, 35), cosmeticToSearch, textFieldStyle);
            cosmeticToSearch = cosmeticToSearch.ToUpper();

            if (GUI.Button(new Rect(20, 120, 430, 50), "Search", buttonStyle))
                StartCoroutine(GetPlayersInRoom(roomToSearch));

            GUI.TextField(new Rect(20, 180, 430, 420), outputString);
        }

        public IEnumerator GetPlayersInRoom(string room)
        {
            outputString = "";

            Dictionary<string, string> cosmetics = new Dictionary<string, string> { { "LBAAD.", "ADMINISTRATOR BADGE" }, { "LBAAK.", "MOD STICK" }, { "LBADE.", "FINGER PAINTER BADGE" }, { "LBAGS.", "ILLUSTRATOR BADGE" }, { "LMAPY.", "FOREST GUIDE MOD STICK" } };

            GorillaComputer.instance.UpdateScreen();
            PlayFabClientAPI.GetSharedGroupData(new GetSharedGroupDataRequest
            {
                SharedGroupId = room + "US"
            }, delegate (GetSharedGroupDataResult result)
            {
                outputString += room + " US\n   Users in room: " + result.Data.Count + "\n   Special users found: ";
                int customCosmeticCount = 0;

                if (result.Data.Count > 0)
                {
                    foreach (KeyValuePair<string, SharedGroupDataRecord> data in result.Data)
                    {
                        if (data.Value.Value.Contains(cosmeticToSearch))
                            customCosmeticCount++;

                        foreach (KeyValuePair<string, string> cosmetic in cosmetics)
                        {
                            if (data.Value.Value.Contains(cosmetic.Key))
                                outputString += " <color=green>" + cosmetic.Value + "</color> ";
                        }
                    }
                }
                if (!outputString.Contains("<color=green>"))
                    outputString += "None";
                outputString += "\n   Users with selected cosmetic: " + customCosmeticCount.ToString();
                outputString += "\n\n";
            }, null, null, null);
            yield return new WaitForSeconds(0.1f);

            PlayFabClientAPI.GetSharedGroupData(new GetSharedGroupDataRequest
            {
                SharedGroupId = room + "USW"
            }, delegate (GetSharedGroupDataResult result)
            {
                outputString += room + " USW\n   Users in room: " + result.Data.Count + "\n   Special users found: ";
                int customCosmeticCount = 0;

                if (result.Data.Count > 0)
                {
                    foreach (KeyValuePair<string, SharedGroupDataRecord> data in result.Data)
                    {
                        if (data.Value.Value.Contains(cosmeticToSearch))
                            customCosmeticCount++;

                        foreach (KeyValuePair<string, string> cosmetic in cosmetics)
                        {
                            if (data.Value.Value.Contains(cosmetic.Key))
                                outputString += " <color=green>" + cosmetic.Value + "</color> ";
                        }
                    }
                }
                if (!outputString.Contains("<color=green>"))
                    outputString += "None";
                outputString += "\n   Users with selected cosmetic: " + customCosmeticCount.ToString();
                outputString += "\n\n";
            }, null, null, null);
            yield return new WaitForSeconds(0.1f);

            PlayFabClientAPI.GetSharedGroupData(new GetSharedGroupDataRequest
            {
                SharedGroupId = room + "EU"
            }, delegate (GetSharedGroupDataResult result)
            {
                outputString += room + " EU\n   Users in room: " + result.Data.Count + "\n   Special users found: ";
                int customCosmeticCount = 0;

                if (result.Data.Count > 0)
                {
                    foreach (KeyValuePair<string, SharedGroupDataRecord> data in result.Data)
                    {
                        if (data.Value.Value.Contains(cosmeticToSearch))
                            customCosmeticCount++;

                        foreach (KeyValuePair<string, string> cosmetic in cosmetics)
                        {
                            if (data.Value.Value.Contains(cosmetic.Key))
                                outputString += " <color=green>" + cosmetic.Value + "</color> ";
                        }
                    }
                }
                if (!outputString.Contains("<color=green>"))
                    outputString += "None";
                outputString += "\n   Users with selected cosmetic: " + customCosmeticCount.ToString();
                outputString += "\n\n";
            }, null, null, null);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
