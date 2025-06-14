using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptAbleObject
{
    [CreateAssetMenu(fileName = "AnimTimeGlobalConfig", menuName = "ScriptableObjects/AnimTimeGlobalConfig")]
    [GlobalConfig("Resources/GlobalConfig/AnimTimeGlobalConfig")]
    public class AnimTimeGlobalConfig : GlobalConfig<AnimTimeGlobalConfig>
    {
        public List<AnimTime> catAnimTimes;
        public List<AnimTime> sharkAnimTimes;
        
        public float GetAnimTime(AnimType animType, Exam exam)
        {
            switch (exam)
            {
                case Exam.Cat:
                    return catAnimTimes.Find(x => x.animType == animType)?.animTimeValue ?? 0f;
                case Exam.Shark:
                    return sharkAnimTimes.Find(x => x.animType == animType)?.animTimeValue ?? 0f;
                default:
                    return 0f;
            }
        }
    }

    public enum Exam
    {
        Cat,
        Shark
    }

    [System.Serializable]
    public class AnimTime
    {
        public AnimType animType;
        public float animTimeValue;
    }
}
