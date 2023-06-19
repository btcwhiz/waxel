using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class GetEventData : MonoBehaviour
{

    [System.Serializable]
    public class EventItem
    {
        public string name;
        public string description;
        public string link;
        public string status;
    }

    public string ApiUrl;
    public Transform eventsParent;
    public GameObject eventItemPrefab;

    protected void Start()
    {
        StartCoroutine(FetchData());
    }

    public IEnumerator FetchData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(ApiUrl))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                EventItem[] eventItemFromJson = JsonConvert.DeserializeObject<EventItem[]>(json);

                foreach(var item in eventItemFromJson)
                {
                    var eventItem = Instantiate(eventItemPrefab);
                    eventItem.transform.SetParent(eventsParent);
                    eventItem.transform.localScale = new Vector3(1, 1, 1);
                    var child = eventItem.gameObject.GetComponent<EventItemStatus>();
                    child.EventName.text = item.name;
                    child.EventDescriptionText.text = item.description;
                    child.EventDescription.gameObject.GetComponent<Button>().onClick.AddListener(delegate { GameObjectHandler.OpenUrl(item.link.ToString()); });
                    child.EventSatus.text = item.status;
                    if(item.status == "Inactive") {
                        child.EventSatus.color = new Color32(222, 41, 22, 255);
                    }
                }

            }
        }
    }
}