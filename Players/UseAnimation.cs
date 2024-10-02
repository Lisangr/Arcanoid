using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Struct for storing animation data
[System.Serializable]
public struct AnimationData
{
    public AnimationClip clip;
    public int damage;
}

// Serializable class for editing animation data via inspector
[System.Serializable]
public class AnimationEntry
{
    public string animationName;
    public AnimationData animationData;
}

public class UseAnimation : MonoBehaviour
{
    [SerializeField] private List<AnimationEntry> animationEntries = new List<AnimationEntry>();
    private Dictionary<string, AnimationData> animations = new Dictionary<string, AnimationData>();
    private Animator animator;
    private bool isPlaying = false; // Flag to track animation state

    void Start()
    {
        animator = GetComponent<Animator>();
        InitializeAnimations();
        //AutoAttack();
    }

    private void InitializeAnimations()
    {
        foreach (var entry in animationEntries)
        {
            if (!animations.ContainsKey(entry.animationName))
            {
                animations.Add(entry.animationName, entry.animationData);
            }
        }
    }

    private int currentDamage = 0; // Store the damage of the current animation

    // Method to play a single random animation
    public void AutoAttack()
    {
        if (!isPlaying)
        {
            List<string> animationNames = new List<string>(animations.Keys);
            animationNames.Shuffle(); // Randomize the list
            string randomAnimation = animationNames[0]; // Select the first one from the shuffled list
            PlayAnimation(randomAnimation); // Play the selected animation
        }
    }

    private void PlayAnimation(string animationName)
    {
        if (animations.ContainsKey(animationName))
        {
            AnimationData animationData = animations[animationName];
            animator.Play(animationData.clip.name);
            isPlaying = true;
            currentDamage = animationData.damage; // Store the damage of the current animation
            StartCoroutine(WaitForAnimation(animationData.clip.length));
        }
    }

    // Return the damage of the current animation
    public int GetCurrentAnimationDamage()
    {
        return currentDamage;
    }

    private IEnumerator WaitForAnimation(float duration)
    {
        yield return new WaitForSeconds(duration);
        PlayIdleAnimation(); // Play Idle animation after the current one finishes
        isPlaying = false; // Reset the flag to allow the next animation to play
    }

    // Method to play the Idle animation
    private const string IdleAnimationKey = "Idle"; // Key for the Idle animation
    public void PlayIdleAnimation()
    {
        if (animations.ContainsKey(IdleAnimationKey))
        {
            AnimationData idleAnimation = animations[IdleAnimationKey];
            animator.Play(idleAnimation.clip.name);
        }
    }
}

// Extension for shuffling a list
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}