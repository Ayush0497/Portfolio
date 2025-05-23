using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelClickIntercept : EventTrigger
{
    protected PrefabPool prefabPool;
    private void Awake()
    {
        prefabPool = GameObject.Find("PrefabPool").GetComponent<PrefabPool>();
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && prefabPool.maxPlayerShips>=1)
        {
            AddTurret(eventData);
            prefabPool.maxPlayerShips--;
        }
        if(eventData.button == PointerEventData.InputButton.Right && prefabPool.maxBloom >= 1)
        {
            AddBloom(eventData);
            prefabPool.maxBloom--;
        }
    }
    public void AddTurret(PointerEventData eventData) 
    {
        Transform player = prefabPool.PlayerShip;
        if (player != null) 
        {
             player.position = Camera.main.ScreenToWorldPoint(eventData.position);
             player.position = new Vector3(player.position.x, player.position.y, 0);
        }
    }

    public void AddBloom(PointerEventData eventData)
    {
        Transform bloom = prefabPool.Bloom;
        if(bloom != null)
        {
            bloom.position = Camera.main.ScreenToWorldPoint(eventData.position);
            bloom.position = new Vector3(bloom.position.x, bloom.position.y, 0);
            StartCoroutine(ScaleBloomOverTime(bloom));
        }
    }

    private IEnumerator ScaleBloomOverTime(Transform bloom)
    {
        float targetScale = 0.5f;
        float duration = 1.0f;

        Vector3 initialScale = bloom.localScale;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            bloom.localScale = Vector3.Lerp(initialScale, new Vector3(targetScale, targetScale, targetScale), t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bloom.localScale = new Vector3(targetScale, targetScale, targetScale);
    }
}
