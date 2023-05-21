using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    private static List<Character> _charactersScr;
    [SerializeField] private List<Text> leaderBoardTexts;


    private void Awake()
    {
        _charactersScr = new List<Character>();
        InvokeRepeating(nameof(ChangeLeaderBoard), 1f, 1f);
    }

    private void ChangeLeaderBoard()
    {
        var charRanksScr = new List<Character>();
        for (var i = 0; i < _charactersScr.Count; i++)
        {
            var mostFurtherCharScr = MostFurtherCharDifferentFromArray(charRanksScr);
            charRanksScr.Add(mostFurtherCharScr);
        }

        SetLeaderBoardTexts(charRanksScr);
    }

    private Character MostFurtherCharDifferentFromArray(ICollection<Character> charRanksScr)
    {
        Character mostFurtherCharScr = null;
        var maxDistanceX = float.MinValue;
        foreach (var charScr in LeaderBoard._charactersScr.Where(charScr =>
            charScr.transform.position.x > maxDistanceX && !charRanksScr.Contains(charScr)))
        {
            mostFurtherCharScr = charScr;
            maxDistanceX = charScr.transform.position.x;
        }

        return mostFurtherCharScr;
    }

    private void SetLeaderBoardTexts(List<Character> charRanksScr)
    {
        for (var index = 0; index < leaderBoardTexts.Count; index++)
        {
            leaderBoardTexts[index].text = charRanksScr[index].userName;
        }
    }


    public static void AddChar(Character charScr)
    {
        _charactersScr ??= new List<Character>();
        _charactersScr.Add(charScr);
    }

    public static void RemoveChar(Character charScr)
    {
        _charactersScr.Remove(charScr);
    }
}