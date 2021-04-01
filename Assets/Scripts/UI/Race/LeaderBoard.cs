using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    private static List<Transform> _playerTransforms;
    public List<Text> _playerRankTexts;

    private void OnEnable()
    {
        _playerTransforms = new List<Transform>();
    }

    private void Update()
    {
        ChangeLeaderBoard();
    }

    void ChangeLeaderBoard()
    {
        List<Transform> _playerRanks = new List<Transform>();
        for (int i = 0; i < _playerRankTexts.Count; i++)
        {
            Transform _mostFurtherPlayer = MostFurtherPlayerDifferentFromArray(_playerRanks);
            _playerRanks.Add(_mostFurtherPlayer);
            _playerRankTexts[i].text = (i + 1) + " . " + _mostFurtherPlayer.GetComponent<Character>().userName;
        }
    }

    Transform MostFurtherPlayerDifferentFromArray(List<Transform> _playerRanks)
    {
        Transform _mostFurtherPlayer = null;
        float maxDistanceX = float.MinValue;
        foreach (Transform _playerTransform in _playerTransforms)
        {
            if (_playerTransform.position.x > maxDistanceX && !_playerRanks.Contains(_playerTransform))
            {
                _mostFurtherPlayer = _playerTransform;
                maxDistanceX = _playerTransform.position.x;
            }
        }
        return _mostFurtherPlayer;
    }


    public static void AddPlayer(Transform _playerTrans)
    {
        _playerTransforms.Add(_playerTrans);
    }

    public static void RemovePlayer(Transform _playerTrans)
    {
        _playerTransforms.Remove(_playerTrans);
    }

}
