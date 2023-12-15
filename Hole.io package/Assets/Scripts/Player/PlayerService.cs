using Holeio.essentials;
using Holeio.Player;
using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    public CameraFollow Playercamera;
    [SerializeField]
    private Vector3 StartPos;
    [SerializeField]
    private int[] scoresForUpgrade;
    [SerializeField]
    private PlayerView player;
    [SerializeField]
    private int currentLevel=0;
    [SerializeField]
    private FixedJoystick playerInput;

    public void CheckForUpgrade(int currentScore)
    {
        if (currentLevel <= scoresForUpgrade.Length - 1) 
        {
            if (currentScore >= scoresForUpgrade[currentLevel])
            {
                player.IncreaseSize();
                currentLevel++;
            }
        }
    }
    public Transform playerTransform()
    {
        return player.transform;
    }

    internal void SetPlayer(PlayerView playerView)
    {
        player = playerView;
        Playercamera.setPlayerTransform(player.transform);
        currentLevel = 0;
        if(playerInput != null)
        {
            player.SetJoystick(playerInput);
        }
    }

    public void SetPlayerJoystick(FixedJoystick playerJoystick)
    {
        playerInput=playerJoystick;
        if (playerInput != null)
        {
            player.SetJoystick(playerInput);
        }
    }
}
