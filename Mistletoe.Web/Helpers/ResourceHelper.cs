// <copyright file="ResourceHelper.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web.Helpers
{
    using Mistletoe.Web.Properties;

    /// <summary>
    ///     Resource helper
    /// </summary>
    public class ResourceHelper
    {
        /// <summary>
        ///     Resource value
        /// </summary>
        /// <param name="resourceName">Resource name</param>
        /// <returns>Value if it exists, else resource name</returns>
        public static string ResourceValue(string resourceName)
        {
            string result = resourceName;

            var resourceValue = Resources.ResourceManager.GetString(resourceName);
            if (string.IsNullOrEmpty(resourceValue) == false)
            {
                result = resourceValue;
            }

            return result;
        }
    }
}