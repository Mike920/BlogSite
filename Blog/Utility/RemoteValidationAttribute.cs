using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blog.Utility
{
    
        /// <summary>
        /// Extends RemoteAttribute class functionality so the validation performs also on server side
        /// </summary>
        public class RemoteValidationAttribute : RemoteAttribute
        {
            /// <summary>
            /// List of all Controllers on MVC Application
            /// </summary>
            /// <returns></returns>
            private static List<Type> GetControllerList()
            {
                return Assembly.GetCallingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(Controller))).ToList();
            }

            /// <summary>
            /// Constructor of base class.
            /// </summary>
            protected RemoteValidationAttribute()
            {
            }

            /// <summary>
            /// Constructor of base class.
            /// </summary>
            public RemoteValidationAttribute(string routeName)
                : base(routeName)
            {
            }

            /// <summary>
            /// Constructor of base class.
            /// </summary>
            public RemoteValidationAttribute(string action, string controller)
                : base(action, controller)
            {
            }

            /// <summary>
            /// Constructor of base class.
            /// </summary>
            public RemoteValidationAttribute(string action, string controller, string areaName)
                : base(action, controller, areaName)
            {
            }

            /// <summary>
            /// Overridden IsValid function
            /// </summary>
            /// <param name="value"></param>
            /// <param name="validationContext"></param>
            /// <returns></returns>
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                string[] additionalFields = this.AdditionalFields.Split(',');

                List<object> propValues = new List<object>();
                propValues.Add(value);
                foreach (string additionalField in additionalFields)
                {
                    PropertyInfo prop = validationContext.ObjectType.GetProperty(additionalField);
                    if (prop != null)
                    {
                        object propValue = prop.GetValue(validationContext.ObjectInstance, null);
                        propValues.Add(propValue);
                    }
                }

            // Find the controller passed in constructor
            var controller = GetControllerList().FirstOrDefault(x => x.Name == string.Format("{0}Controller", this.RouteData["controller"]));
                if (controller == null)
                {
                    // Default behavior of IsValid when no controller is found.
                    return ValidationResult.Success;
                }

                // Find the Method passed in constructor
                var mi = controller.GetMethod(this.RouteData["action"].ToString());
                if (mi == null)
                {
                    // Default behavior of IsValid when action not found
                    return ValidationResult.Success;
                }

                // Create instance of the controller to be able to call non static validation method
                var instance = Activator.CreateInstance(controller);

                // invoke the method on the controller with value
                var result = (JsonResult)mi.Invoke(instance, propValues.ToArray());

                // Return success or the error message string from CustomRemoteAttribute
                return result.Data.ToString() == "True" ? ValidationResult.Success : new ValidationResult(result.Data.ToString());
            }
        }
    
}
