using System;
using System.Collections;
using System.Collections.Generic;
using DataBase.Models;
using Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using WebSocket;

namespace DefaultNamespace
{
    public static class ApiHelper
    {
        private const string IdentityUrl = "";
        private const string ProgressUrl = "";
        private const string PlayerBaseUrl = "";
        private const string MapApiUrl = "";
        private const string WebSocket = "";
         //Enter URLs correctly


        public static IEnumerator LoginRequest(AuthenticateRequest req,
            UnityEvent<TDResponse<AuthenticateResponse>> loginEndEvent)
        {
            var reqWithInfo = new BaseRequest<AuthenticateRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(IdentityUrl + "/api/User/Login"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response =
                    JsonConvert.DeserializeObject<TDResponse<AuthenticateResponse>>(www.downloadHandler.text);
                // Debug.Log(response.Data.Token);
                PlayerPrefs.SetString("token", response.Data.Token);
                loginEndEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator LoginWithDeviceID(
            UnityEvent<TDResponse<AuthenticateResponse>> loginEndEvent)
        {
            var reqWithInfo = new BaseRequest()
            {

                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(IdentityUrl + "/api/User/LoginWithDeviceId"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response =
                    JsonConvert.DeserializeObject<TDResponse<AuthenticateResponse>>(www.downloadHandler.text);
                // Debug.Log(response.Data.Token);
                PlayerPrefs.SetString("token", response.Data.Token);
                response.Data.LoginKind = 1;
                loginEndEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetNextWave(UnityEvent<TDResponse<UserTDInfoDTO>> progressDoneEvent)
        {
            var reqWithInfo = new BaseRequest()
            {

                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(ProgressUrl + "/api/Progress/GetNextWave"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<UserTDInfoDTO>>(www.downloadHandler.text);
                // Debug.Log(response.Message);
                progressDoneEvent.Invoke(response);

            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }
            www.Dispose();
        }

        public static IEnumerator TutorialDone(UnityEvent<TDResponse> progressDoneEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(IdentityUrl + "/api/User/TutorialDone"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                // Debug.Log(response.Message);
                progressDoneEvent.Invoke(response);

            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }
            www.Dispose();
        }


        public static IEnumerator TDResetLevel(UnityEvent<TDResponse> resetLevelEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(ProgressUrl + "/api/Progress/ResetLevel"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                Debug.Log(response.Message);
                resetLevelEvent.Invoke(response);

            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }
            www.Dispose();
        }


        public static IEnumerator GetTutorialWave(UnityEvent<TDResponse<UserTDInfoDTO>> getTutorialWaveEvent)
        {
            Debug.Log("GetTutorialWave");
            var reqWithInfo = new BaseRequest()
            {

                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(ProgressUrl + "/api/Progress/GetTutorialWave"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));

            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {

                var response = JsonConvert.DeserializeObject<TDResponse<UserTDInfoDTO>>(www.downloadHandler.text);

                getTutorialWaveEvent.Invoke(response);

            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }
            www.Dispose();
        }



        public static IEnumerator ProgressRequest(ProgressDto req, UnityEvent<TDResponse> progressDoneEvent)
        {
            var reqWithInfo = new BaseRequest<ProgressDto>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(ProgressUrl + "/api/Progress/AddProgress"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                // Debug.Log(response.Message);
                progressDoneEvent.Invoke(response);

            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }
            www.Dispose();
        }
        public static IEnumerator AddTutorialProgress(ProgressDto req, UnityEvent<TDResponse> addTutorialProgressEvent)
        {
            Debug.Log("AddTutorialProgress worked");
            var reqWithInfo = new BaseRequest<ProgressDto>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(ProgressUrl + "/api/Progress/AddTutorialProgress"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {

                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);

                addTutorialProgressEvent.Invoke(response);

            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }
            www.Dispose();
        }

        public static IEnumerator GetBuildings(
            UnityEvent<TDResponse<List<PlayerBasePlacementDTO>>> playerbaseplacementevent, int? buildingId = null)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/playerbase/getbuildings"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response =
                    JsonConvert.DeserializeObject<TDResponse<List<PlayerBasePlacementDTO>>>(www.downloadHandler.text);
                if (buildingId != null)
                {
                    response.GenericValue = buildingId;
                }
                playerbaseplacementevent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetBuildingTypes(UnityEvent<TDResponse<List<BuildingTypeDTO>>> buildingTypesEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetBuildingTypes"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response =
                    JsonConvert.DeserializeObject<TDResponse<List<BuildingTypeDTO>>>(www.downloadHandler.text);
                buildingTypesEvent.Invoke(response);
                // Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator AddPlayerBaseBuilding(PlayerBaseBuildingRequest req,
            UnityEvent<TDResponse> AddPlayerBaseBuildingRequestEvent)
        {
            var reqWithInfo = new BaseRequest<PlayerBaseBuildingRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/AddPlayerBaseBuilding"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer "
                                                  + PlayerPrefs.GetString("token")
            );
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                AddPlayerBaseBuildingRequestEvent.Invoke(response);
                // Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        #region iskender

        public static IEnumerator GetMapByAreaIds(List<int> req,
            UnityEvent<TDResponse<List<MapInfoDto>>> mapByAreaIdsEvent)
        {
            var reqWithInfo = new BaseRequest<List<int>>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(MapApiUrl + "/api/Map/GetMapByAreaIds"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<List<MapInfoDto>>>(www.downloadHandler.text);
                mapByAreaIdsEvent.Invoke(response);
                // Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetMapItemTypes(UnityEvent<TDResponse<List<MapItemTypeDTO>>> mapItemTypeListEvent)
        {
            var reqWithInfo = new BaseRequest<MapItemDTO>()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(MapApiUrl + "/api/Map/GetMapItemTypes"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response =
                    JsonConvert.DeserializeObject<TDResponse<List<MapItemTypeDTO>>>(www.downloadHandler.text);
                mapItemTypeListEvent.Invoke(response);
                // Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetMoveUserBaser(MapItemDTO req, UnityEvent<TDResponse> moveUserBaserEvent)
        {
            var reqWithInfo = new BaseRequest<MapItemDTO>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(MapApiUrl + "/api/Map/MoveUserBase"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                moveUserBaserEvent.Invoke(response);
                // Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetAddUserBase(bool req, UnityEvent<TDResponse<MapItemDTO>> addUserBaseEvent)
        {
            var reqWithInfo = new BaseRequest<bool>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(MapApiUrl + "/api/Map/AddUserBase"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<MapItemDTO>>(www.downloadHandler.text);
                addUserBaseEvent.Invoke(response);
                // Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        #endregion

        public static IEnumerator MovePlayerBuilding(PlayerBaseBuildingRequest req,
            UnityEvent<TDResponse> MovePlayerBuildingEvent)
        {
            var reqWithInfo = new BaseRequest<PlayerBaseBuildingRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/MovePlayerBuilding"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response =
                    JsonConvert.DeserializeObject<TDResponse<List<PlayerBaseBuildingRequest>>>(www.downloadHandler
                        .text);
                MovePlayerBuildingEvent.Invoke(response);
                // Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetPlayerBaseInfo(UnityEvent<TDResponse<PlayerBaseInfoDTO>> playerBaseInfoEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetPlayerBaseInfo"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<PlayerBaseInfoDTO>>(www.downloadHandler.text);
                playerBaseInfoEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetOtherPlayersBaseInfo(long req, UnityEvent<TDResponse<PlayerBaseInfoDTO>> playerBaseInfoEvent)
        {
            var reqWithInfo = new BaseRequest<long>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetOtherPlayersBaseInfo"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<PlayerBaseInfoDTO>>(www.downloadHandler.text);
                playerBaseInfoEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator ChangeBio(string req, UnityEvent<TDResponse> changeBio)
        {
            var reqWithInfo = new BaseRequest<string>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/ChangeBio"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                changeBio.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator UpdateOrCreatePlayerBaseInfo(PlayerBaseInfoDTO req,
            UnityEvent<TDResponse<PlayerBaseInfoDTO>> playerBaseInfoEvent)
        {
            var reqWithInfo = new BaseRequest<PlayerBaseInfoDTO>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www =
                new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/UpdateOrCreatePlayerBaseInfo"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<PlayerBaseInfoDTO>>(www.downloadHandler.text);
                playerBaseInfoEvent.Invoke(response);
                // Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator ChangeAvatar(int req,
            UnityEvent<TDResponse> changeAvatarResponse)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www =
                new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/ChangeAvatar"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                changeAvatarResponse.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetBaseLevelLeaderBoard(int req,
            UnityEvent<TDResponse<Paging<LeaderBoardItem>>> leaderBoardItemEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www =
                new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetBaseLevelLeaderBoard"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<Paging<LeaderBoardItem>>>(www.downloadHandler.text);
                leaderBoardItemEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetKillTroopLeaderBoard(int req,
            UnityEvent<TDResponse<Paging<LeaderBoardItem>>> leaderBoardItemEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www =
                new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetKillTroopLeaderBoard"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<Paging<LeaderBoardItem>>>(www.downloadHandler.text);
                leaderBoardItemEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetLootedScrapLeaderBoard(int req,
            UnityEvent<TDResponse<Paging<LeaderBoardItem>>> leaderBoardItemEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www =
                new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetLootedScrapLeaderBoard"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<Paging<LeaderBoardItem>>>(www.downloadHandler.text);
                leaderBoardItemEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetLootRunPointLeaderBoard(int req,
            UnityEvent<TDResponse<Paging<LeaderBoardItem>>> leaderBoardItemEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www =
                new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetDefenseKillLeaderBoard"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<Paging<LeaderBoardItem>>>(www.downloadHandler.text);
                leaderBoardItemEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator UpgradeBuildingInfo(int req,
            UnityEvent<TDResponse<BuildingUpgradeTimeDTO>> buildingUpgradeTimeEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/UpgradeBuildingInfo"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {

                // Debug.Log(www.downloadHandler.text);
                var response =
                    JsonConvert.DeserializeObject<TDResponse<BuildingUpgradeTimeDTO>>(www.downloadHandler.text);
                response.Data.clickID = req;
                buildingUpgradeTimeEvent.Invoke(response);

                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator UpgradeBuildingRequest(int req,
            UnityEvent<TDResponse<PlayerBasePlacementDTO>> updateBuildingReqEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www =
                new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/UpgradeBuildingRequest"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response =
                    JsonConvert.DeserializeObject<TDResponse<PlayerBasePlacementDTO>>(www.downloadHandler.text);
                updateBuildingReqEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator UpgradeBuildingDoneRequest(int req,
            UnityEvent<TDResponse<PlayerBasePlacementDTO>> upgradeBuildingDoneReqEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www =
                new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/UpgradeBuildingDoneRequest"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response =
                    JsonConvert.DeserializeObject<TDResponse<PlayerBasePlacementDTO>>(www.downloadHandler.text);
                upgradeBuildingDoneReqEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator CollectBaseResources(BaseRequest req,
            UnityEvent<TDResponse<CollectBaseResponse>> collectBaseResourcesEvent)
        {
            var reqWithInfo = new BaseRequest<BaseRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/CollectBaseResources"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<CollectBaseResponse>>(www.downloadHandler.text);
                collectBaseResourcesEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetResearchTable(BaseRequest req,
            UnityEvent<TDResponse<List<ResearchTableDTO>>> resarchTableEvent)
        {
            var reqWithInfo = new BaseRequest<BaseRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetResarchTable"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response =
                    JsonConvert.DeserializeObject<TDResponse<List<ResearchTableDTO>>>(www.downloadHandler.text);
                resarchTableEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetResearchTableNodeUpgradeNecessariesByNodeId(int req,
            UnityEvent<TDResponse<ResearchNodeUpgradeNecessariesDTO>> ResearchTableNodeUpgradeNecessariesByNodeId)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www =
                new UnityWebRequest(new Uri(PlayerBaseUrl +
                                            "/api/PlayerBase/GetResearchTableNodeUpgradeNecessariesByNodeId"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response =
                    JsonConvert.DeserializeObject<TDResponse<ResearchNodeUpgradeNecessariesDTO>>(www.downloadHandler
                        .text);
                ResearchTableNodeUpgradeNecessariesByNodeId.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator UpgradeResarchNode(int req, UnityEvent<TDResponse> upgradeResarchNodeEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/UpgradeResarchNode"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                upgradeResarchNodeEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator UpgradeResarchNodeDone(int req, UnityEvent<TDResponse> upgradeResarchNodeDoneEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www =
                new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/UpgradeResarchNodeDone"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                upgradeResarchNodeDoneEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetPrisonInfo(UnityEvent<TDResponse<PlayerPrisonDTO>> getprisonInfoEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetPrisonInfo"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<PlayerPrisonDTO>>(www.downloadHandler.text);
                getprisonInfoEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator ExecutePrisoners(int value, UnityEvent<TDResponse<int>> executePrisonersEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = value,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/ExecutePrisoners"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<int>>(www.downloadHandler.text);
                executePrisonersEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator PrisonerTrainingRequest(int value, UnityEvent<TDResponse> prisonerTrainingReqEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = value,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www =
                new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/PrisonerTrainingRequest"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                prisonerTrainingReqEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator PrisonerTrainingDoneRequest(UnityEvent<TDResponse<int>> prisonerTrainingDoneReqEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www =
                new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/PrisonerTrainingDoneRequest"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<int>>(www.downloadHandler.text);
                prisonerTrainingDoneReqEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetHospitalInfo(UnityEvent<TDResponse<PlayerHospitalDTO>> getHospitalInfoEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetHospitalInfo"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<PlayerHospitalDTO>>(www.downloadHandler.text);
                getHospitalInfoEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator HealingRequest(int req, UnityEvent<TDResponse> healingReqEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/HealingRequest"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                healingReqEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetMapByBoundBox(BoundBox Box, UnityEvent<TDResponse<List<MapInfoDto>>> GetMapByBoundBoxEvent)
        {
            var reqWithInfo = new BaseRequest<BoundBox>()
            {
                Data = Box,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(MapApiUrl + "/api/Map/GetMapByBoundBoxV2"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<List<MapInfoDto>>>(www.downloadHandler.text);
                GetMapByBoundBoxEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator TeleportUserBase(TeleportBaseRequest req, UnityEvent<TDResponse> responseEvent)
        {
            var reqWithInfo = new BaseRequest<TeleportBaseRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(MapApiUrl + "/api/Map/TeleportUserBase"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                responseEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetPlayerBaseCoord(UnityEvent<TDResponse<MapInfoDto>> responseEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(MapApiUrl + "/api/Map/GetPlayerBaseCoord"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<MapInfoDto>>(www.downloadHandler.text);
                responseEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator ScoutPlayer(ScoutRequest req, UnityEvent<TDResponse<ScoutDTO>> responseEvent)
        {
            var reqWithInfo = new BaseRequest<ScoutRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/ScoutPlayerV2"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "/");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<ScoutDTO>>(www.downloadHandler.text);
                responseEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator HealingDoneRequest(UnityEvent<TDResponse<int>> healingDoneReqEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/HealingDoneRequest"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<int>>(www.downloadHandler.text);
                healingDoneReqEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetMarket(UnityEvent<TDResponse<MarketDTO>> getMarketEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetMarket"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<MarketDTO>>(www.downloadHandler.text);
                getMarketEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator BuyMarketItem(BuyMarketItemRequest req, UnityEvent<TDResponse> getMarketEvent)
        {
            var reqWithInfo = new BaseRequest<BuyMarketItemRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/BuyMarketItem"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                getMarketEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetInventory(UnityEvent<TDResponse<InventoryDTO>> getInventoryEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetInventory"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.downloadHandler.text);
                var response = JsonConvert.DeserializeObject<TDResponse<InventoryDTO>>(www.downloadHandler.text);
                getInventoryEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator UseItem(UseItemRequest req, UnityEvent<TDResponse> useItemReqEvent)
        {
            var reqWithInfo = new BaseRequest<UseItemRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/UseItem"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                useItemReqEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetPlayerSpeedUpItem(UnityEvent<TDResponse<List<UsableItemDTO>>> playerItemDtoEvent)
        {
            var reqWithInfo = new BaseRequest<UseItemRequest>()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetPlayersSpeedUpItems"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<List<UsableItemDTO>>>(www.downloadHandler.text);
                playerItemDtoEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator SpeedUpHealing(SpeedUpRequest req, UnityEvent<TDResponse<string>> SpeedUpHealingEvent)
        {
            var reqWithInfo = new BaseRequest<SpeedUpRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/SpeedUpHealing"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<string>>(www.downloadHandler.text);
                SpeedUpHealingEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator SpeedUpPrisonerTraining(SpeedUpRequest req, UnityEvent<TDResponse<string>> SpeedUpPrisonerTrainingEvent)
        {
            var reqWithInfo = new BaseRequest<SpeedUpRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/SpeedUpPrisonerTraining"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<string>>(www.downloadHandler.text);
                SpeedUpPrisonerTrainingEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator SpeedUpUpgradeBuilding(SpeedUpRequest req, UnityEvent<TDResponse<string>> speedUpUpgradeBuildingEvent)
        {
            var reqWithInfo = new BaseRequest<SpeedUpRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/SpeedUpUpgradeBuilding"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<string>>(www.downloadHandler.text);
                speedUpUpgradeBuildingEvent.Invoke(response);
                Debug.Log(response.HasError);
                Debug.Log("basarili");
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator CreateGang(CreateGangRequest req, UnityEvent<TDResponse> createGangEvent)
        {
            var reqWithInfo = new BaseRequest<CreateGangRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/CreateGang"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                createGangEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator SendInvitation(long req, UnityEvent<TDResponse> sendInvatationEvent)
        {
            var reqWithInfo = new BaseRequest<long>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/SendGangInvitation"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                sendInvatationEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator SendGangApplication(string req, UnityEvent<TDResponse> sendGangApplicationEvent)
        {
            var reqWithInfo = new BaseRequest<string>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/SendGangApplication"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                sendGangApplicationEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator AcceptGangInvitation(GangInvitationResponse req, UnityEvent<TDResponse> sendInvatationEvent)
        {
            var reqWithInfo = new BaseRequest<GangInvitationResponse>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/AcceptGangInvitation"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                sendInvatationEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator CollectLootRunByNewsId(string req, UnityEvent<TDResponse> sendInvatationEvent)
        {
            var reqWithInfo = new BaseRequest<string>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/News/CollectLootRunByNewsId"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                sendInvatationEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetGangInfo(long? req, UnityEvent<TDResponse<GangInfo>> getGangInfoEvent)
        {
            var reqWithInfo = new BaseRequest<long?>()
            {
                Data = req,
                Info = GetInfo()
            };
            Debug.Log("^^^^^^^^^^^^Req" + req);
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/GetGangInfo"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<GangInfo>>(www.downloadHandler.text);
                getGangInfoEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetEnemyGangInfo(long? req, UnityEvent<TDResponse<GangInfo>> getGangInfoEvent)
        {
            var reqWithInfo = new BaseRequest<long?>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/GetGangInfo"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<GangInfo>>(www.downloadHandler.text);
                getGangInfoEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetGangMembers(string req, UnityEvent<TDResponse<List<GangMemberInfo>>> sendInvatationEvent)
        {
            var reqWithInfo = new BaseRequest<string>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/GetGangMembers"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<List<GangMemberInfo>>>(www.downloadHandler.text);
                sendInvatationEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetEnemyGangMembers(string req, UnityEvent<TDResponse<List<GangMemberInfo>>> sendInvatationEvent)
        {
            var reqWithInfo = new BaseRequest<string>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/GetGangMembers"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<List<GangMemberInfo>>>(www.downloadHandler.text);
                sendInvatationEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator ChangeUsername(string req, UnityEvent<TDResponse<string>> changeName)
        {
            var reqWithInfo = new BaseRequest<string>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(IdentityUrl + "/api/User/ChangeUsername"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<string>>(www.downloadHandler.text);
                changeName.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetTDWaveRewardsByWaveId(int req, UnityEvent<TDResponse<List<PlayerItemDTO>>> rewardEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetTDWaveRewardsByWaveId"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<List<PlayerItemDTO>>>(www.downloadHandler.text);
                rewardEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetTDWaveRewardsDoneByWaveId(int req, UnityEvent<TDResponse> rewardDoneEvent)
        {
            var reqWithInfo = new BaseRequest<int>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetTDWaveRewardsDoneByWaveId"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<List<PlayerItemDTO>>>(www.downloadHandler.text);
                rewardDoneEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator AttackPlayer(AttackRequest req, UnityEvent<TDResponse<AttackInfoDTO>> attackEvent)
        {
            var reqWithInfo = new BaseRequest<AttackRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/AttackPlayer"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<AttackInfoDTO>>(www.downloadHandler.text);
                attackEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GangEditDTO(GangEditDTO req, UnityEvent<TDResponse> GangEditEvent)
        {
            var reqWithInfo = new BaseRequest<GangEditDTO>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/EditGang"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                GangEditEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }


        public static IEnumerator GetFirstTimeTutorial(string req, UnityEvent<TDResponse<int?>> getFirstTimeTutorialEvent)
        {
            var reqWithInfo = new BaseRequest<string>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetFirstTimeTutorial"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<int?>>(www.downloadHandler.text);
                getFirstTimeTutorialEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator FirstTimeTutorialDone(string req, UnityEvent<TDResponse> firstTimeTutorialDoneEvent)
        {
            var reqWithInfo = new BaseRequest<string>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/FirstTimeTutorialDone"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                firstTimeTutorialDoneEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetGangMemberType(UnityEvent<TDResponse<List<MemberTypeDTO>>> GetGangMemberTypeEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/GetGangMemberTypes"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<List<MemberTypeDTO>>>(www.downloadHandler.text);
                GetGangMemberTypeEvent.Invoke(response);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator SetGangMemberType(List<MemberTypeDTO> req, UnityEvent<TDResponse> SetGangMemberTypeEvent)
        {
            var reqWithInfo = new BaseRequest<List<MemberTypeDTO>>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/SetGangMemberType"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                SetGangMemberTypeEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator SetMemberTypePoolRequest(List<SetMemberTypePoolRequest> req, UnityEvent<TDResponse> SetGangMemberTypeEvent)
        {
            var reqWithInfo = new BaseRequest<List<SetMemberTypePoolRequest>>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/SetMemberTypePool"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                SetGangMemberTypeEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetGangs(Int32 req, UnityEvent<TDResponse<Paging<GangInfo>>> getGangsEvent)
        {
            var reqWithInfo = new BaseRequest<Int32>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/GetGangs"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<Paging<GangInfo>>>(www.downloadHandler.text);
                getGangsEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetGangApplications(Int32 req, UnityEvent<TDResponse<Paging<GangApplicationDTO>>> GetGangApplicationsEvent)
        {
            var reqWithInfo = new BaseRequest<Int32>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/GetGangApplications"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<Paging<GangApplicationDTO>>>(www.downloadHandler.text);
                GetGangApplicationsEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GangInvitationResponse(GangInvitationResponse req, UnityEvent<TDResponse> invitationResponse)
        {
            var reqWithInfo = new BaseRequest<GangInvitationResponse>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/GetGangApplications"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                invitationResponse.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator ApplicationAccept(ApplicationAcceptRequest req, UnityEvent<TDResponse> invitationResponse)
        {
            var reqWithInfo = new BaseRequest<ApplicationAcceptRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/AcceptGangApplication"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                invitationResponse.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator ChangeGangMemberType(List<ChangeGangMemberTypeRequest> req, UnityEvent<TDResponse> ChangeMemberTypeRes)
        {
            var reqWithInfo = new BaseRequest<List<ChangeGangMemberTypeRequest>>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/ChangeGangMemberType"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                ChangeMemberTypeRes.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator KickMember(long req, UnityEvent<TDResponse> ChangeMemberTypeRes)
        {
            var reqWithInfo = new BaseRequest<long>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/KickMember"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                ChangeMemberTypeRes.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator DestroyGang(UnityEvent<TDResponse> DestroyGangEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(WebSocket + "/api/Gang/DestroyGang"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                DestroyGangEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator CreateRally(CreateRallyRequest request, UnityEvent<TDResponse> CreateRallyEvent)
        {
            var reqWithInfo = new BaseRequest<CreateRallyRequest>()
            {
                Data = request,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/CreateRally"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                CreateRallyEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator JoinRally(JoinRallyRequest request, UnityEvent<TDResponse> JoinRallyEvent)
        {
            var reqWithInfo = new BaseRequest<JoinRallyRequest>()
            {
                Data = request,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/JoinRally"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                JoinRallyEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetRallyList(UnityEvent<TDResponse<List<RallyDTO>>> GetRallyListEvent)
        {
            var reqWithInfo = new BaseRequest()
            {

                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetRallyList"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<List<RallyDTO>>>(www.downloadHandler.text);
                GetRallyListEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator SendSupportUnit(SupportUnitRequest request, UnityEvent<TDResponse> SendSupportEvent)
        {
            var reqWithInfo = new BaseRequest<SupportUnitRequest>
            {
                Data = request,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/SendSupportUnit"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                SendSupportEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator GetSupportUnitBackById(long request, UnityEvent<TDResponse> GetSupportUnitBackByIdEvent)
        {
            var reqWithInfo = new BaseRequest<long>
            {
                Data = request,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetSupportUnitBackById"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                GetSupportUnitBackByIdEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator GetScoutInfo(UnityEvent<TDResponse<PlayerScoutDTO>> playerScoutEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetScoutInfo"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<PlayerScoutDTO>>(www.downloadHandler.text);
                playerScoutEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator SoyTrainingRequest(Int32 req, UnityEvent<TDResponse> spyTrainingEvent)
        {
            var reqWithInfo = new BaseRequest<Int32>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/SpyTrainingRequest"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                spyTrainingEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator SpyTrainingDoneRequest(UnityEvent<TDResponse<Int32>> spyTrainingDoneEvent)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/SpyTrainingDoneRequest"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<Int32>>(www.downloadHandler.text);
                spyTrainingDoneEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator KickRallyPart(long req, UnityEvent<TDResponse> kickRallyPartEvent)
        {
            var reqWithInfo = new BaseRequest<long>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/KickRallyPart"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                kickRallyPartEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        public static IEnumerator AbortRally(long req, UnityEvent<TDResponse> abortRallyEvent)
        {
            var reqWithInfo = new BaseRequest<long>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/AbortRally"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                abortRallyEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
        
        public static IEnumerator GetTutorialQuestList(UnityEvent<TDResponse<List<PlayerTutorialQuestDTO>>> getTutorialQuestListEvent)
        {
            var reqWithInfo = new BaseRequest()
            {

                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/GetTutorialQuestList"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse<List<PlayerTutorialQuestDTO>>>(www.downloadHandler.text);
                getTutorialQuestListEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }
       
        public static IEnumerator DoneTutorialQuestById(DoneTutorialQuestRequest req, UnityEvent<TDResponse> doneTutorialQuestByIdEvent)
        {
            var reqWithInfo = new BaseRequest<DoneTutorialQuestRequest>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/PlayerBase/DoneTutorialQuestById"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "*/*");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                response.GenericValue = req ? 1 : 0;
                doneTutorialQuestByIdEvent.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator DistrubutePoolScraps(UnityEvent<TDResponse> distrubute)
        {
            var reqWithInfo = new BaseRequest()
            {
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/Gang/DistrubutePoolScraps"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "/");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                distrubute.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        public static IEnumerator DonatePoolScrap(Int32 req, UnityEvent<TDResponse> donatePool)
        {
            var reqWithInfo = new BaseRequest<Int32>()
            {
                Data = req,
                Info = GetInfo()
            };
            var serializedReq = JsonConvert.SerializeObject(reqWithInfo);
            Debug.Log(serializedReq + "serialize");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(serializedReq);
            UnityWebRequest www = new UnityWebRequest(new Uri(PlayerBaseUrl + "/api/Gang/DonatePoolScrap"));
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "/");
            www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            www.disposeUploadHandlerOnDispose = true;
            www.disposeDownloadHandlerOnDispose = true;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<TDResponse>(www.downloadHandler.text);
                donatePool.Invoke(response);
                Debug.Log(response.Message);
            }
            else
            {
                Debug.Log("Error While Sending: " + www.error);
            }

            www.Dispose();
        }

        private static InfoDto GetInfo()
        {
            return new InfoDto()
            {
                AppVersion = Application.version,
                DeviceId = SystemInfo.deviceUniqueIdentifier,
                DeviceModel = SystemInfo.deviceModel,
                DeviceType = SystemInfo.deviceName + "(" + SystemInfo.deviceType + ")",
                OsVersion = SystemInfo.operatingSystem
            };
        }
    }
}