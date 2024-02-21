using System;

namespace GraphicsLabor.Scripts.Attributes.LaborerAttributes.DrawerAttributes
{

    public enum MessageLevel
    {
        None,
        Info,
        Warning,
        Error
    }
    
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ShowMessageAttribute : DrawerAttribute
    {

        public string Message { get; private set; }
        public MessageLevel MessageType { get; private set; }

        /// <summary>
        /// Draws a box with text for the user above the property
        /// </summary>
        /// <param name="message">The message for the user</param>
        /// <param name="messageType">The level of the message (Info, Warning...)</param>
        public ShowMessageAttribute(string message, MessageLevel messageType = MessageLevel.None)
        {
            Message = message;
            MessageType = messageType;
        }
    }
    
 

}