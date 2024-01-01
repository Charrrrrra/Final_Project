using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;
    public AudioSource right_foot_sound;
    public AudioSource left_foot_sound;
    public AudioSource attack_sound;
    public AudioSource fall_back_sound;
    public AudioSource fall_front_sound;
    public AudioSource monster_scream_sound;

    void Awake() {
        if(_instance == null)
            _instance = this;
        else if(_instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void RightFoot() {
        right_foot_sound.Play();
    }

    public void LeftFoot() {
        left_foot_sound.Play();
    }

    public void Attack_NPC() {
        attack_sound.Play();
    }

    public void Fall_Back() {
        fall_back_sound.Play();
    }

    public void Fall_Front() {
        fall_front_sound.Play();
    }

    public void MonsterScream() {
        monster_scream_sound.Play();
    }
}
