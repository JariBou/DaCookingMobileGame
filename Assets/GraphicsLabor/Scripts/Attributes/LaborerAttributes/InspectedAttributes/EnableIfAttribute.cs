using System;
using GraphicsLabor.Scripts.Attributes.Utility;

namespace GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EnableIfAttribute : EnableIfAttributeBase
    {
        /// <summary>
        /// Enable field or property if condition is met
        /// </summary>
        /// <param name="condition">A name of a bool field/prop or a method that returns a bool with no arguments</param>
        public EnableIfAttribute(string condition)
            : base(condition)
        {
            Inverted = false;
        }

        /// <summary>
        /// Enable field or property if conditions are met using the conditionOperator
        /// </summary>
        /// <param name="conditionOperator">how to process conditions: AND or OR</param>
        /// <param name="conditions">Names of a bool field/prop or a method that returns a bool with no arguments</param>
        public EnableIfAttribute(ConditionOperator conditionOperator, params string[] conditions)
            : base(conditionOperator, conditions)
        {
            Inverted = false;
        }
        
        /// <summary>
        /// Enable field or property if conditions are all met
        /// </summary>
        /// <param name="conditions">Names of a bool field/prop or a method that returns a bool with no arguments</param>
        public EnableIfAttribute(params string[] conditions) 
                : base(ConditionOperator.And, conditions)
            {
                Inverted = false;
            }

        /// <summary>
        /// Enable field or property if enum value is found
        /// </summary>
        /// <param name="enumName">The name of the field holding the enum</param>
        /// <param name="enumValue">The enum value to check for</param>
        public EnableIfAttribute(string enumName, object enumValue)
            : base(enumName, enumValue as Enum)
        {
            Inverted = false;
        }
        }
}