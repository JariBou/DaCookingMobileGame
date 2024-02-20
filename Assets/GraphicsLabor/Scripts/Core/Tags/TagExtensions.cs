using UnityEngine;

namespace GraphicsLabor.Scripts.Core.Tags
{
    public static class TagExtensions
    {

        /// <summary>
        /// Used to check if a MonoBehaviour only has all passed tags
        /// </summary>
        /// <param name="self">The MonoBehaviour to check</param>
        /// <param name="tags">Tags to test for</param>
        /// <returns></returns>
        /// <exception cref="MissingComponentException">Whenever the checked MonoBehaviour doesn't have a component implementing ITagHolder</exception>
        public static bool HasExactTags(this MonoBehaviour self, LaborTags tags)
        {
            ITagHolder component = self.GetComponent<ITagHolder>();
            if (component == null)
            {
                throw new MissingComponentException($"{nameof(self)} is missing required ITagHolder component");
            }

            return component.GetLaborTags() == tags;
        }

        /// <summary>
        /// Used to check if a MonoBehaviour at least has all passed tags
        /// </summary>
        /// <param name="self">The MonoBehaviour to check</param>
        /// <param name="tags">Tags to test for</param>
        /// <returns></returns>
        /// <exception cref="MissingComponentException">Whenever the checked MonoBehaviour doesn't have a component implementing ITagHolder</exception>
        public static bool HasTags(this MonoBehaviour self, LaborTags tags)
        {
            ITagHolder component = self.GetComponent<ITagHolder>();
            if (component == null)
            {
                throw new MissingComponentException($"{nameof(self)} is missing required ITagHolder component");
            } 

            int result = (int)component.GetLaborTags() & (int)tags;

            return result == (int)tags;
        }

        /// <summary>
        /// Used to check if a MonoBehaviour at least has one of passed tags
        /// </summary>
        /// <param name="self">The MonoBehaviour to check</param>
        /// <param name="tags">Tags to test for</param>
        /// <returns></returns>
        /// <exception cref="MissingComponentException">Whenever the checked MonoBehaviour doesn't have a component implementing ITagHolder</exception>
        public static bool HasOneOfTags(this MonoBehaviour self, LaborTags tags)
        {
            ITagHolder component = self.GetComponent<ITagHolder>();
            if (component == null)
            {
                throw new MissingComponentException($"{nameof(self)} is missing required ITagHolder component");
            } 

            int result = (int)component.GetLaborTags() & (int)tags;

            return result > 0;
        }

    }
}