using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    private static List<Transform> _charTransforms;
    public List<Text> playerRankTexts;


    private void Start()
    {
        Invoke(nameof(ChangeLeaderBoard), 1f);
    }

    private void ChangeLeaderBoard()
    {
        var playerRanks = new List<Transform>();
        for (var i = 0; i < playerRankTexts.Count; i++)
        {
            var mostFurtherPlayer = MostFurtherPlayerDifferentFromArray(playerRanks);
            playerRanks.Add(mostFurtherPlayer);
            playerRankTexts[i].text = (i + 1) + " . " + mostFurtherPlayer.GetComponent<Character>().userName;
        }
    }

    private Transform MostFurtherPlayerDifferentFromArray(ICollection<Transform> charTransforms)
    {
        Transform mostFurtherCharTrans = null;
        var maxDistanceX = float.MinValue;
        foreach (var charTrans in LeaderBoard._charTransforms.Where(charTrans =>
            charTrans.position.x > maxDistanceX && !charTransforms.Contains(charTrans)))
        {
            mostFurtherCharTrans = charTrans;
            maxDistanceX = charTrans.position.x;
        }

        return mostFurtherCharTrans;
    }


    public static void AddPlayer(Transform charTrans)
    {
        _charTransforms ??= new List<Transform>();
        _charTransforms.Add(charTrans);
    }

    public static void RemovePlayer(Transform playerTrans)
    {
        _charTransforms.Remove(playerTrans);
    }
}