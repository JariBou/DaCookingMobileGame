using System;
using GraphicsLabor.Scripts.Attributes.Utility;

namespace GraphicsLabor.Scripts.Attributes.LaborerAttributes.InspectedAttributes
{
    public abstract class ShowIfAttributeBase : InspectedAttribute
    {
        public string[] Conditions { get; private set; }
        public ConditionOperator ConditionOperator { get; private set; }
        public bool Inverted { get; protected set; }
        public Enum EnumValue { get; private set; }

        protected ShowIfAttributeBase(string condition)
        {
            ConditionOperator = ConditionOperator.And;
            Conditions = new[] { condition };
        }

        // Allows for showing a field if certains conditions are met (bools are true)
        protected ShowIfAttributeBase(ConditionOperator conditionOperator, params string[] conditions) // params string[] equivalent to *args of python but strongly typed
        {
            ConditionOperator = conditionOperator;
            Conditions = conditions;
        }

        // Allows for showing a field if an enum field has a certain value
        protected ShowIfAttributeBase(string enumName, Enum enumValue)
            : this(enumName) // Like in C++ calls ShowAttributeBase before
        {
            EnumValue = enumValue ?? throw new ArgumentNullException(nameof(enumValue), "This parameter must be an enum value.");
        }
    }
}